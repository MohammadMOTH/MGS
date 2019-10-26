using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.MEF
{
   public abstract class MEFAbstract : Interface.MEF.IMEF
    {
        private List<Type> _types = new List<Type>();

        public MEFAbstract()
        {
            throw new System.NotImplementedException();
        }

        ~MEFAbstract()
        {
            throw new System.NotImplementedException();
        }
    }
}
