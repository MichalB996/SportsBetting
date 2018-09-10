﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using BUKMACHER_INFRASTRUCTURE.Commands;

namespace BUKMACHER_INFRASTRUCTURE.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
        }
    }
}