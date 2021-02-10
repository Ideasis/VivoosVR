using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Photon.Voice
{
	public class RawCodec
	{
		public class Encoder<T> : IEncoderDirect<T[]>
		{
			public string Error { get; private set; }

			public Action<ArraySegment<byte>, FrameFlags> Output { set; get; }

			private static readonly ArraySegment<byte> EmptyBuffer = new ArraySegment<byte>(new byte[] { });

			public ArraySegment<byte> DequeueOutput(out FrameFlags flags)
			{
                flags = 0;
                return EmptyBuffer;
			}

            public VoiceInfo Info { get; }

			public void EndOfStream()
			{
			}

			public void Dispose()
			{				
			}			

			public void Input(T[] buf)
			{
				if (Error != null)
				{
					return;
				}
				if (Output == null)
				{
					Error = "RawCodec.Encoder: Output action is not set";
					return;
				}
				if (buf == null)
				{
					return;
				}
				if (buf.Length == 0)
				{
					return;
				}
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream stream = new MemoryStream();
				bf.Serialize(stream, buf);
				Output(new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length), 0);
			}
		}

		public class Decoder<T> : IDecoder
		{
			public string Error { get; private set; }

			public Decoder(Action<FrameOut<T>> output)
			{
				this.output = output;
			}

			public void Open(VoiceInfo info)
			{
			}
			
			private Type outType = (new T[1])[0].GetType();
			
			public void Input(byte[] buf, FrameFlags flags)
			{
				if (buf == null)
				{
					return;
				}
				if (buf.Length == 0)
				{
					return;
				}
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream stream = new MemoryStream(buf);
				var obj = bf.Deserialize(stream);
				if (obj.GetType() != outType)
				{
					var objFloat = obj as float[];
					if (objFloat != null)
					{
						var objShort = new short[objFloat.Length];
						AudioUtil.Convert(objFloat, objShort, objFloat.Length);
						output(new FrameOut<T>((T[])(object)objShort, false));
					}
					else
					{
						var objShort = obj as short[];
						if (objShort != null)
						{
							objFloat = new float[objShort.Length];
							AudioUtil.Convert(objShort, objFloat, objShort.Length);
							output(new FrameOut<T>((T[])(object)objFloat, false));
						}
					}
				}
				else
				{
					output(new FrameOut<T>((T[])obj, false));
				}
			}
			public void Dispose()
			{
			}

			private Action<FrameOut<T>> output;
		}
	}
}
