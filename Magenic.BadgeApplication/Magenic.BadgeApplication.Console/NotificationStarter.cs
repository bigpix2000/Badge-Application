﻿using Magenic.BadgeApplication.Common;
using System.Configuration;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    public static class NotificationStarter
    {
        public static void Start()
        {
            Logger.Info<Processor.NotificationProcessor>($"{nameof(NotificationStarter)} initialized.");
            AutofacBootstrapper.Init();
            Task.Factory.StartNew(() => new Processor.NotificationProcessor().Start());
        }
    }
}
