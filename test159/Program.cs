using System;
using System.Net.Sockets;
namespace ServerGame

{
    class Test
    {
        static void Main(string[] args)
        {

            Program.Data.Message MyMessage = new Program.Data.Message("Hello I'am Here, How Are You...?");
            Program.Room.Room room = new Program.Room.Room();
            Event.MessageSender msgSender = new Event.MessageSender();

            msgSender.MessageSent += room.OnMessageSent;
            msgSender.OnMessageSent(MyMessage);
            Console.WriteLine(msgSender.EventName);
        }
    }
}
