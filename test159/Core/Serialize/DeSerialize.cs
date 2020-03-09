using ServerGame.Core.Connctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Serialize
{
    public class DeSerialize : ServerGame.Interface.Serialize.DeSerialize
    {
        public  PackSendData Decoder(byte[] bytes)
        {
            return decoder(bytes, true);
        }
        public static PackSendData Deserialize(byte[] bytes)
        {
            return decoder(bytes, true);
        }

        private static object converttypeby(byte idtype , byte[]data, bool utf)
        {
            switch (idtype)
            {

                case 1:
                    return BitConverter.ToBoolean   (data, 0);
                case 2:
                    return BitConverter.ToChar      (data, 0);
                case 3:
                    return BitConverter.ToDouble    (data, 0);
                case 4:
                    return BitConverter.ToInt16     (data, 0);
                case 5:
                    return BitConverter.ToInt32     (data, 0);
                case 6:
                    return BitConverter.ToInt64     (data, 0);
                case 7:
                    return BitConverter.ToSingle    (data, 0);
                case 8:
                    return BitConverter.ToUInt16    (data, 0);
                case 9:
                    return BitConverter.ToUInt32    (data, 0);
                case 10:
                    return BitConverter.ToUInt64    (data, 0);
                case 11:
                    return Encoding.ASCII.GetString(data);
                case 12:
                    return Encoding.UTF8.GetString(data);
                case 13:
                    return data;
                case 14:
                    return data[0];

                default:
                    return null;
            }


        }
        private  static PackSendData decoder(byte[] bytes ,  bool utf)
        {

           
            var data = new List<Data.Data>();
   

            var pack = new PackSendData(BitConverter.ToUInt16(new byte[] { bytes[0], bytes[1] }, 0) , data);
            byte contalldata = bytes[2];
            List<byte> databytes = new List<byte>();
            int start = 0;
            int end = 0;
            int startLoong = 5;
            int endLoong = 6;

            for (int gi = 0;gi < contalldata; gi++)
            {
                byte type = bytes[startLoong-2];
                byte PramterName = bytes[startLoong-1 ];
                start = endLoong +1;
                end = start + BitConverter.ToUInt16(new byte[] { bytes[startLoong], bytes[endLoong] }, 0) -1;
                

                for (int i = start; i <= end; i++)
                {
                    databytes.Add(bytes[i]);
                }

                pack.AddData( new Data.Data( converttypeby(type, databytes.ToArray(), utf) , PramterName ) );
                startLoong = end+3;
                endLoong = startLoong + 1;
                databytes.Clear();
            }

            return pack;

        }
    }
}
