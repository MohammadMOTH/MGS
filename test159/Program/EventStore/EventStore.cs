using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Core.EventStore;
using System.Linq;
using System.Reflection;
using ServerGame.Program.Event;
using ServerGame.Interface.Data;


namespace ServerGame.Program.EventStore
{
    class EventStore : EventStoreAbstract
    {
        
        public static IEnumerable<Type> GetAllClasses(string nameSpace) 
        { 
            Assembly asm = Assembly.GetExecutingAssembly();
            if (!String.IsNullOrWhiteSpace(nameSpace))
                return asm.GetTypes().Where(x => x.Namespace == nameSpace).Select(x => x);
            else return asm.GetTypes().Select(x => x);
        }
        
        public static void PrinAllClass(IEnumerable<Type> list)
        {
            foreach(Type t in list)
            {
                //var myObject = typeof(t);
                //Console.WriteLine(t.GetType().GetMethod("OnMessageSending").Name);
                //var myProp = myclass.get
                //t.GetType().GetProperty()
                
                    //var myclass = t.GetProperty("Data").PropertyType.Name;
                    if(t.GetProperty("Data") != null)
                {
                    var myclass = t.GetProperty("Data").PropertyType.Name;
                    Console.WriteLine(myclass);
                }
                else Console.WriteLine("Bad");
                    
                 
                //var myclass = t.GetProperty("Data").PropertyType.Name;

                //var myclass = typeof(t.Name);
            }
        }
        

        public override void Parser(ICustomeData data, IEnumerable<Type> list)
        {
            foreach (Type currentClass in list)
            {
                if (currentClass.GetProperty("Data") != null)
                {
                    var DataClass = currentClass.GetProperty("Data").PropertyType.Name;
                    var DataConnection = data.GetType().Name;
                    if(DataClass == DataConnection)
                    {
                        
                    }
                    Console.WriteLine(DataClass);
                    Console.WriteLine(DataConnection);
                }
                else Console.WriteLine("Bad");
            }
        }
    }
}
