using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Connctions
{
    [Serializable]
    public  class PackSendData 
    {
        public ushort NameOFEvent { get; protected set; }
        public List<ServerGame.Core.Data.Data> AllData { get; protected set; }
        
        public PackSendData(ushort  NameOFEvent, List<ServerGame.Core.Data.Data> AllData)
        {
            this.NameOFEvent = NameOFEvent;
            this.AllData = AllData;
        }

        public void AddData(ServerGame.Core.Data.Data Data)
        {
            this.AllData.Add(Data);
        }
        public void AddData(ServerGame.Core.Data.Data[] Datas)
        {
            foreach (var Data in Datas)
            {
                this.AllData.Add(Data);
            }
            
        }


    }

   public enum DataType : byte
    {
        String = 0,
        Voice = 1,
        Float = 2,
 


    }
}
