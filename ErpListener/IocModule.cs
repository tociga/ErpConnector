using Common.Logging;
using Ninject.Modules;
using System;

namespace ErpConnector.Listener
{
    class IocModule : NinjectModule
    {
        public override void Load()
        {

            Bind<ILog>().ToMethod(ctx =>
            {
                Type type = ctx.Request.ParentContext.Request.Service;
                return LogManager.GetLogger(type);
            });

            Bind<WinService>().ToSelf();

        }
    }
}

