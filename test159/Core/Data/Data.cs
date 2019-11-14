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

        public int PramterNameHash { get; protected set; }

        public string PramterName { get; protected set; }


        public Data(object TypeData, string PramterName)
        {
     
            this.DataS = TypeData;
            this.PramterNameHash = PramterName.GetHashCode();
            this.PramterName = PramterName;

        }


    }
}
