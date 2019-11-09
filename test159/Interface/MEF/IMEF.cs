using ServerGame.Interface.Room;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ServerGame.Interface.MEF
{
    interface IMEF
    {/*
       private static CompositionContainer  _container { get; set; }

        public static void LoadDllFiles(object classs)
        {
            Assembly.LoadFile(@"c:\qw");
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                   

                        if (type.GetInterface("IRoom") != null)
                        {
                        IRoom Room = Activator.CreateInstance(type) as IRoom;
                        Room.Test();

                        }


                }
            }
        }

        public static void LoadDllFiles()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IMEF).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(@"C:\Users\mohmm\Extensions"));       ///TODO: edit here the path 
            _container = new CompositionContainer(catalog);
        }


        public static void ComposeParts(object myclass)
        {
            try
            {
                _container.ComposeParts(myclass);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

        }
        */

    }
}
