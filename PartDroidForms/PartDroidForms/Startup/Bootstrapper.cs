using Autofac;
using System;
using Prism.Events;
using System.Collections.Generic;
using System.Text;


namespace PartDroidForms
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            
            
            

            return builder.Build();
        }
    }
}
