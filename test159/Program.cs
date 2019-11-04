using System;
using System.Net.Sockets;
using System.Reflection;
using System.Collections.Generic;
using ServerGame.Program.EventStore;


namespace ServerGame

{
    class Test
    {
        static void Main(string[] args)
        {

            Program.Data.Message MyMessage = new Program.Data.Message("Hello I'am Here, How Are You...?");
            Program.Room.Room room = new Program.Room.Room();
            Program.Event.MessageSender msgSender = new Program.Event.MessageSender();

            msgSender.MessageSent += room.OnMessageSent;
            msgSender.OnMessageSent(MyMessage);
            
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            

            EventStore.PrinAllClass(EventStore.GetAllClasses("ServerGame.Program.Event"));
            
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            Console.WriteLine(".......................................................");
            //EventStore e = new EventStore();
            //e.Parser(MyMessage);
            //Type T = Type.GetType("ServerGame.Program.Event");
            //Type aa = Assembly.GetExecutingAssembly().GetType();
            //var s = typeof(T);
        }
    }
}

