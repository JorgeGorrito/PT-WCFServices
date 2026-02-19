using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using PruebaTecnica.BDO.Repositories;
using PruebaTecnica.BDO.UseCases;
using PruebaTecnica.Business.Contracts;
using PruebaTecnica.Business.Logic;
using PruebaTecnica.Business.Mappings;
using PruebaTecnica.Business.Services;
using PruebaTecnica.DataAccess.Repositories;
using System;

namespace PruebaTecnica.Business
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AutoMapperConfig.Configure();

            var builder = new ContainerBuilder();

            builder.RegisterType<UserLogic>()
                   .As<IUserUseCases>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                   .As<IUserRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterInstance(AutoMapperConfig.Mapper)
                   .As<IMapper>()
                   .SingleInstance();

            builder.RegisterType<UserService>()
                   .Named<object>(typeof(UserService).FullName)
                   .InstancePerDependency();

            builder.RegisterType<UserService>()
                   .InstancePerDependency();

            var container = builder.Build();

            AutofacServiceHostFactory.Container = container;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}