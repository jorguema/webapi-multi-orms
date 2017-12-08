using Application.Services;
using Application.Services.Interfaces;
using Autofac;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Ioc
{
    public static class AutofacService
    {
        private static ContainerBuilder _container = null;

        public static ContainerBuilder GetContainer() {
            return _container ?? RegisterServices();
        }

        public static ContainerBuilder RegisterServices()
        {
            var builder = new ContainerBuilder();

            //services
            builder.RegisterType<ContactService>().As<IContactService>();

            //repositories
            builder.RegisterType<ContactRepository>().As<IContactRepository>();
            
            return builder;
        }
        
    }
}
