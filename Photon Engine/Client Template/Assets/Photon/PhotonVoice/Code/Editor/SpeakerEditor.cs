namespace Photon.Voice.Unity.Editor
{
    using UnityEngine;
    using UnityEditor;
    using Unity;

    [CustomEditor(typeof(Speaker))]
    public class SpeakerEditor : Editor
    {
        private Speaker speaker;

        private SerializedProperty playDelayMsSp;
        private SerializedProperty playbackOnlyWhenEnabledSp;

        #region AnimationCurve

        private AudioSource audioSource;
        private FFTWindow window = FFTWindow.Hanning;
        private float[] samples = new float[512];
        private AnimationCurve curve;

        private void DrawAnimationCurve()
        {
            this.audioSource.GetSpectrumData(this.samples, 0, this.window);
            this.curve = new AnimationCurve();
            for (var i = 0; i < this.samples.Length; i++)
            {
                this.curve.AddKey(1.0f / this.samples.Length * i, this.samples[i] * 100);
            }
            EditorGUILayout.CurveField(this.curve, Color.green, new Rect(0, 0, 1.0f, 0.1f), GUILayout.Height(64));
        }

        #endregion

        private void OnEnable()
        {
            this.speaker = this.target as Speaker;
            this.audioSource = this.speaker.GetComponent<AudioSource>();
            this.playDelayMsSp = this.serializedObject.FindProperty("playDelayMs");
            this.playbackOnlyWhenEnabledSp = this.serializedObject.FindProperty("playbackOnlyWhenEnabled");
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.UpdateIfRequiredOrScript();
            VoiceLogger.ExposeLogLevel(this.serializedObject, this.speaker);

            EditorGUI.BeginChangeCheck();

            if (PhotonVoiceEditorUtils.IsInTheSceneInPlayMode(this.speaker.gameObject))
            {
                this.speaker.PlayDelayMs = EditorGUILayout.IntField(new GUIContent("Playback Delay (ms)",
                    "Remote audio stream playback delay to compensate packets latency variations. Try 100 - 200 if sound is choppy. Default is 200ms"), this.speaker.PlayDelayMs);
                this.speaker.PlaybackOnlyWhenEnabled = EditorGUILayout.Toggle(new GUIContent("Playback Only When Enabled", "If true, component will work only when enabled and active in hierarchy."),
                    this.speaker.PlaybackOnlyWhenEnabled);
            }
            else
            {
                EditorGUILayout.PropertyField(this.playDelayMsSp, new GUIContent("Playback Delay (ms)", "Remote audio stream playback delay to compensate packets latency variations. Try 100 - 200 if sound is choppy. Default is 200ms"));
                EditorGUILayout.PropertyField(this.playbackOnlyWhenEnabledSp, new GUIContent("Playback Only When Enabled", "If true, component will work only when enabled and active in hierarchy."));
            }

            if (EditorGUI.EndChangeCheck())
            {
                this.serializedObject.ApplyModifiedProperties();
            }

            if (this.speaker.IsPlaying)
            {
                EditorGUILayout.LabelField(string.Format("Current Buffer Lag: {0}", this.speaker.Lag));
                this.DrawAnimationCurve();
            }
        }
    }
}