using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Data
{
    public interface IEventData<T>
    {
        /// <summary>
        /// Data of Event
        /// </summary>
        public IEventData<T> Data { get; }
    }
}
