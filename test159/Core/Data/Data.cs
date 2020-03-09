using ServerGame.Interface.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Data
{
    [Serializable]
    public  class Data : IData
    {
        
        public object DataS { get; protected set; }



        public byte PramterName { get; protected set; }


        public Data(object TypeData, byte PramterName)
        {
            this.DataS = TypeData;
            this.PramterName = PramterName;

        }
        public void DataSChange (object Datas)
        {
            this.DataS = Datas;

        }

    }
   

}
