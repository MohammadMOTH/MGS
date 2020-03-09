using ServerGame.Core.Connctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServerGame.Core.Serialize
{
    public class Serialize : ServerGame.Interface.Serialize.Serialize
    {
        
        public static byte[] serialize(PackSendData PackSendData)
        {
            return encoder(PackSendData, true);
        }

        public byte[] Encoder(PackSendData PackSendData)
        {
            return encoder( PackSendData,true);
        }

        private static byte[] encoder(PackSendData PackSendData, bool utf = false)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(PackSendData.NameOFEvent));
            bytes.Add((byte)PackSendData.AllData.Count);

            for (int i = 0; i < PackSendData.AllData.Count; i++)
            {
                Type t = PackSendData.AllData[i].DataS.GetType();
                byte type = 0;

                if (t == typeof(bool))
                {
                    type = 1;
                    var b = BitConverter.GetBytes(((bool)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);

                }

                else if (t == typeof(char))
                {
                    type = 2;
                    var b = (BitConverter.GetBytes(((char)PackSendData.AllData[i].DataS)));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(Double))
                {
                    type = 3;
                    var b = (BitConverter.GetBytes(((Double)PackSendData.AllData[i].DataS)));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(Int16))
                {
                    type = 4;
                    var b = BitConverter.GetBytes(((Int16)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(Int32))
                { type = 5;
                    var b = BitConverter.GetBytes(((Int32)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);

                }
                else if (t == typeof(Int64))
                { type = 6; 
                    var b = BitConverter.GetBytes(((Int64)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(Single))
                { type = 7; 
                    var b = BitConverter.GetBytes(((Single)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(UInt16))
                { type = 8;
                    var b = BitConverter.GetBytes(((UInt16)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(UInt32))
                {
                    type = 9;
                    var b = BitConverter.GetBytes(((UInt32)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(UInt64))
                { type =10;
                    var b = BitConverter.GetBytes(((UInt64)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(string) && !utf)
                { type = 11; 
                    var b = Encoding.ASCII.GetBytes(((string)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(string) && utf)
                { type = 12;
                    var b = Encoding.UTF8.GetBytes(((string)PackSendData.AllData[i].DataS));
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(byte[]))
                {
                    type = 13;
                    var b =(byte[])PackSendData.AllData[i].DataS;
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)b.Length));
                    bytes.AddRange(b);
                }
                else if (t == typeof(byte))
                {
                    type = 14;
                    var b = (byte)PackSendData.AllData[i].DataS;
                    bytes.Add(type);
                    bytes.Add(PackSendData.AllData[i].PramterName);
                    bytes.AddRange(BitConverter.GetBytes((Int16)1));
                    bytes.Add(b);
                }




            }

            return bytes.ToArray();
        }
    }
}
