﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace RestWrapHost
{
    public static class DependancyBuilder
    {
        public static IContainer Container()
        {
            var builder = new ContainerBuilder();

            return builder.Build();
        }
    }
}
