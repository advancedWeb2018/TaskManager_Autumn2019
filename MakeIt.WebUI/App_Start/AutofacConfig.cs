﻿using Autofac;
using Autofac.Integration.Mvc;
using MakeIt.BLL.Identity;
using MakeIt.BLL.IdentityConfig;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.AutoMapper;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MakeIt.WebUI.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            // We get a container instance
            var builder = new ContainerBuilder();

            // Register the controller in the current assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register AutoMapper Module
            builder.RegisterModule(new MapperAutofacModule()); 

            string[] assemblyScanerPattern = new[] { @"MakeIt.*.dll" };

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // Begin setup of autofac >>

            // Scan for assemblies containing autofac modules in the bin folder
            var assemblies = new List<Assembly>();
            assemblies.AddRange(
                Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "MakeIt.*.dll", SearchOption.AllDirectories)
                         .Where(filename => assemblyScanerPattern.Any(pattern => Regex.IsMatch(filename, pattern)))
                         .Select(Assembly.LoadFrom)
                );

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
            }

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.RegisterType<MakeItContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => new UserStore(c.Resolve<MakeItContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => Startup.DataProtectionProvider).InstancePerRequest();
    
            // Create a new container with the dependencies defined above
            var container = builder.Build();

            // Setting dependency mapper
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}