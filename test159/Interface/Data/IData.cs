using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame
{

    public  interface IData
    {
        object DataS { get; }
        int PramterNameHash { get;  }
        string PramterName { get;  }



    }
}
