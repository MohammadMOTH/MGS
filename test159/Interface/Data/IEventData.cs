using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Data
{
    public interface IEventData<T>: ICustomeData
    {
        /// <summary>
        /// Data of Event
        /// </summary>
        public T Data { get; set; }

    }
}
