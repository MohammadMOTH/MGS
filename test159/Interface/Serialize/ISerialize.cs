using ServerGame.Core.Connctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Serialize
{
    interface Serialize
    {

        byte[] Encoder(PackSendData PackSendData);



    }
}
