using System;
using System.Net.Sockets;
namespace ServerGame

{
    class Test
    {
        
        static void Main(string[] args)
        {
            string MyMessage = "Hello I'am Here, How Are You...?";
            Program.Room.Room room = new Program.Room.Room();
            Event.MessageSender msgSender = new Event.MessageSender();

            msgSender.MessageSent += room.OnMessageSent;
            msgSender.Send(MyMessage);
            
        }
    }
}
