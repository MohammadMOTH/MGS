using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Event;
using ServerGame.Program.Data;
using ServerGame.Interface.Data;

namespace ServerGame.Program.Event
{
    class PositionSender : Ievnet<Position>
    {
        
        public string EventName { get; set; }

        public ICustomeData Data { get; set; }

        public event EventHandler<Position> Send;

        public void OnSending(object sender, Position arg)
        {

        }
    }
}
