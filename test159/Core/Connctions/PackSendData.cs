using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Connctions
{
    [Serializable]
    public  class PackSendData 
    {
        public  string NameOFEvent { get; protected set; }
        public List<ServerGame.Core.Data.Data> AllData { get; protected set; }
        
        public PackSendData(string NameOFEvent, List<ServerGame.Core.Data.Data> AllData)
        {
            this.NameOFEvent = NameOFEvent;
            this.AllData = AllData;
        }

        public void AddData(ServerGame.Core.Data.Data Data)
        {
            this.AllData.Add(Data);
        }


    }
}
