﻿using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpListener
{
    class IocModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {

            Bind<ILog>().ToMethod(ctx =>
            {
                Type type = ctx.Request.ParentContext.Request.Service;
                return LogManager.GetLogger(type);
            });

            Bind<Service.WinService>().ToSelf();

        }
    }
}

