using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Core.EventStore;
using System.Linq;
using System.Reflection;
using ServerGame.Program.Event;
using ServerGame.Interface.Event;


namespace ServerGame.Program.EventStore
{
    class EventStore : EventStoreAbstract
    {
        
        public static IEnumerable<object> GetAllClasses(string nameSpace) 
        { 
            Assembly asm = Assembly.GetExecutingAssembly();
            if (!String.IsNullOrWhiteSpace(nameSpace))
                return asm.GetTypes().Where(x => x.Namespace == nameSpace).Select(x => x);
            else return asm.GetTypes().Select(x => x);
        }
        
        public static void PrinAllClass(IEnumerable<object> list)
        {
            foreach(object t in list)
            {
                //var myObject = typeof(t);
                Console.WriteLine(t);
            }
        }
        

        public override void Parser(IData data)
        {
            //foreach (string c in GetAllClasses("ServerGame.Program.Event"))
            //{
            //    if (data == )
            //    Console.WriteLine("Hello From Parser");
            //    Console.WriteLine(c);
            //}
            
        }
    }
}
