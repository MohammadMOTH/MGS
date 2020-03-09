using ServerGame.Core.Connctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Serialize
{
    interface DeSerialize
    {
        PackSendData Decoder(byte[] bytes);
        

    }
}
