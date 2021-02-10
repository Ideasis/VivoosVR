using UnityEngine;
using System.Collections.Generic;

namespace Photon.Voice.Unity
{
    // Plays back input audio via Unity AudioSource
    // May consume audio packets in thread other than Unity's main thread
    public class UnityAudioOut : IAudioOut<float>
    {
        private int frameSamples;
        private int frameSize;
        private int bufferSamples;

        private int clipWriteSamplePos;

        private int playSamplePosPrev;
        private int sourceTimeSamplesPrev;
        private int playLoopCount;

        private readonly AudioSource source;
        private int channels;
        private bool started;
        private bool flushed = true;

        const int MAX_DELAY_MS = 3000;
        const int RESAMPLE_RAMP_END_MS = 2000;

        private int targetDelaySamples;
        private int upperTargetDelaySamples;       // correct if higher: gradually move to target via input frames resampling
        private int lowerTargetDelaySamples;       // correct if lower: set to targetit (and produce a gap in playback), it could be gradual move too but we don't have enough space for this because we set target as low as possible
        private int maxDelaySamples;               // set delay to this value if delay is higher
        private int resampleRampEndDelaySamples;   // the more delay deviates from target, the higher resampling factor: after this value the factor remains constant

        private readonly ILogger logger;
        private readonly string logPrefix;
        private readonly bool debugInfo;

        float[] zeroFrame;
        float[] resampledFrame;
        public UnityAudioOut(AudioSource audioSource, ILogger logger, string logPrefix, bool debugInfo)
        {            
            this.source = audioSource;
            this.logger = logger;
            this.logPrefix = logPrefix;
            this.debugInfo = debugInfo;
        }
        public int Lag { get { return this.clipWriteSamplePos - (this.started ? this.playLoopCount * this.bufferSamples + this.source.timeSamples : 0); } }

        public bool IsPlaying
        {
            get { return started && !this.flushed; }
        }

        public void Start(int frequency, int channels, int frameSamples, int playDelayMs)
        {
            //frequency = (int)(frequency * 1.2); // underrun test
            //frequency = (int)(frequency / 1.2); // overrun test

            this.channels = channels;
            this.bufferSamples = 2 * MAX_DELAY_MS * frequency / 1000; // make sure we have enough soace
            this.frameSamples = frameSamples;
            this.frameSize = frameSamples * channels;

            this.source.loop = true;
            // using streaming clip leads to too long delays
            this.source.clip = AudioClip.Create("UnityAudioOut", bufferSamples, channels, frequency, false);
            this.started = true;

            // add 1 frame samples to make sure that we have something to play when delay set to 0
            int playDelaySamples = playDelayMs * frequency / 1000 + frameSamples;
            this.lowerTargetDelaySamples = playDelaySamples;
            this.targetDelaySamples = playDelaySamples + playDelaySamples / 2;
            this.upperTargetDelaySamples = 2 * playDelaySamples;

            this.maxDelaySamples = MAX_DELAY_MS * frequency / 1000;
            this.resampleRampEndDelaySamples = RESAMPLE_RAMP_END_MS * frequency / 1000;

            this.clipWriteSamplePos = this.targetDelaySamples;

            if (this.framePool.Info != this.frameSize)
            {
                this.framePool.Init(this.frameSize);
            }

            this.zeroFrame = new float[this.frameSize];
            this.resampledFrame = new float[this.frameSize];

            this.source.Play();

            this.logger.LogWarning("{0} UnityAudioOut Start: overrun bs={1} ch={2} f={3} ltds={4} tds={5} utds={6} mds={7}, rreds={8}", this.logPrefix, bufferSamples, channels, frequency, lowerTargetDelaySamples, targetDelaySamples, upperTargetDelaySamples, maxDelaySamples, resampleRampEndDelaySamples);
        }

        Queue<float[]> frameQueue = new Queue<float[]>();
        public const int FRAME_POOL_CAPACITY = 50;
        PrimitiveArrayPool<float> framePool = new PrimitiveArrayPool<float>(FRAME_POOL_CAPACITY, "UnityAudioOut");
        bool catchingUp = false;

        // should be called in Update thread
        public void Service()
        {
            if (this.started)
            {
                // cache source.timeSamples
                int sourceTimeSamples = source.timeSamples;                
                // loop detection (pcmsetpositioncallback not called when clip loops)
                if (sourceTimeSamples < sourceTimeSamplesPrev)
                {
                    playLoopCount++;
                }
                sourceTimeSamplesPrev = sourceTimeSamples;


                var playSamplePos = this.playLoopCount * this.bufferSamples + sourceTimeSamples;

                var lagSamples = this.clipWriteSamplePos - playSamplePos;
                if (!this.flushed)
                {                    
                    if (lagSamples > maxDelaySamples)
                    {
                        if (this.debugInfo)
                        {
                            this.logger.LogWarning("{0} UnityAudioOut overrun {1} {2} {3} {4} {5} {6}", this.logPrefix, lowerTargetDelaySamples, upperTargetDelaySamples, lagSamples, playSamplePos, this.clipWriteSamplePos, playSamplePos + targetDelaySamples);
                        }
                        this.clipWriteSamplePos = playSamplePos + maxDelaySamples;
                        lagSamples = maxDelaySamples;
                    }
                    else if (lagSamples < lowerTargetDelaySamples)
                    {
                        if (this.debugInfo)
                        {
                            this.logger.LogWarning("{0} UnityAudioOut underrun {1} {2} {3} {4} {5} {6}", this.logPrefix, lowerTargetDelaySamples, upperTargetDelaySamples, lagSamples, playSamplePos, this.clipWriteSamplePos, playSamplePos + targetDelaySamples);
                        }
                        this.clipWriteSamplePos = playSamplePos + targetDelaySamples;
                        lagSamples = targetDelaySamples;
                    }
                }

                lock (this.frameQueue)
                {
                    while (frameQueue.Count > 0)
                    {
                        var frame = frameQueue.Dequeue();
                        if (frame == null) // flush signalled
                        {
                            this.flushed = true;
                            if (catchingUp)
                            {
                                catchingUp = false;
                                this.logger.LogWarning("{0} UnityAudioOut stream sync reset {1} {2} {3} {4} {5} {6}", this.logPrefix, lowerTargetDelaySamples, upperTargetDelaySamples, lagSamples, playSamplePos, this.clipWriteSamplePos, playSamplePos + targetDelaySamples);
                            }
                            return;
                        }
                        else
                        {
                            if (this.flushed)
                            {
                                this.clipWriteSamplePos = playSamplePos + targetDelaySamples;
                                lagSamples = targetDelaySamples;
                                this.flushed = false;
                            }                            
                        }

                        if (lagSamples > upperTargetDelaySamples && !catchingUp)
                        {
                            catchingUp = true;
                            this.logger.LogWarning("{0} UnityAudioOut stream sync started {1} {2} {3} {4} {5} {6}", this.logPrefix, lowerTargetDelaySamples, upperTargetDelaySamples, lagSamples, playSamplePos, this.clipWriteSamplePos, playSamplePos + targetDelaySamples);
                        }

                        if (lagSamples <= targetDelaySamples && catchingUp)
                        {
                            catchingUp = false;
                            this.logger.LogWarning("{0} UnityAudioOut stream sync finished {1} {2} {3} {4} {5} {6}", this.logPrefix, lowerTargetDelaySamples, upperTargetDelaySamples, lagSamples, playSamplePos, this.clipWriteSamplePos, playSamplePos + targetDelaySamples);
                        }

                        if (catchingUp)
                        {
                            const int STEPS = 3;
                            int resampledLen = frame.Length;
                            int k = STEPS * (lagSamples - targetDelaySamples) / (resampleRampEndDelaySamples - targetDelaySamples);
                            if (k >= STEPS) k = STEPS - 1;
                            if (k >= 0)
                            {
                                resampledLen = frame.Length * (100 - 5*(k + 1)) / 100;                                
                            }
                            
                            AudioUtil.Resample(frame, resampledFrame, resampledLen, channels);

                            // zero not used part of the buffer because SetData applies entire frame
                            // if this frame is the last, grabage may be played back
                            for (int i = resampledLen; i < resampledFrame.Length;i++)
                            {
                                resampledFrame[i] = 0;
                            }
                            this.source.clip.SetData(resampledFrame, this.clipWriteSamplePos % this.bufferSamples);
                            this.clipWriteSamplePos += resampledLen / this.channels;
                            lagSamples -= resampledLen / this.channels;
                        }
                        else
                        {
                            this.source.clip.SetData(frame, this.clipWriteSamplePos % this.bufferSamples);
                            this.clipWriteSamplePos += frame.Length / this.channels;
                        }
                        framePool.Release(frame);
                    }
                }

                // clear played back buffer segment
                var clearStart = this.playSamplePosPrev;
                var clearMin = playSamplePos - this.bufferSamples;
                if (clearStart < clearMin)
                {
                    clearStart = clearMin;
                }
                // round up
                var framesToClear = (playSamplePos - clearStart - 1) / this.frameSamples + 1;
                for (var offset = playSamplePos - framesToClear * this.frameSamples; offset < playSamplePos; offset += this.frameSamples)
                {
                    var o = offset % this.bufferSamples;
                    if (o < 0) o += this.bufferSamples;
                    this.source.clip.SetData(this.zeroFrame, o);
                }
                this.playSamplePosPrev = playSamplePos;

            }
        }
        // may be called on any thread
        public void Push(float[] frame)
        {
            if (!this.started)
            {
                return;
            }

            if (frame.Length == 0)
            {
                return;
            }

            if (frame.Length != this.frameSize)
            {
                logger.LogError("{0} UnityAudioOut audio frames are not of  size: {1} != {2}", this.logPrefix, frame.Length, this.frameSize);
                return;
            }

            float[] b = framePool.AcquireOrCreate();
            System.Buffer.BlockCopy(frame, 0, b, 0, frame.Length * sizeof(float));
            lock (this.frameQueue)
            {
                this.frameQueue.Enqueue(b);
            }
        }

        public void Flush()
        {
            lock (this.frameQueue)
            {
                this.frameQueue.Enqueue(null);
            }
        }

        public void Stop()
        {
            this.started = false;
            if (this.source != null)
            {
                this.source.clip = null;
            }
        }
    }
}