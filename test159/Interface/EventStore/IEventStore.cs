using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.EventStore
{
    public interface IEventStore
    {
        /// <summary>
        /// List Of All Stored Events
        /// </summary>
        public static List<Ievnet> StoredEvent { get; set; }

        /// <summary>
        /// Method To Parse Inputed Data Parameter Then Fire The Right Event Based On Data
        /// </summary>
        /// <param name="data">Which it Get From Connection To Check Which Event Should Raise</param>
        public void Parser(IData data);

    }
}
