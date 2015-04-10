using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Practices.Unity;
using UIKit;
using XamarinSessionRestore.iOS.Repositories;
using XamarinSessionRestore.Repositories;

namespace XamarinSessionRestore.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            App.Container.RegisterType<ISessionRepository, XmlRepositories>(new ContainerControlledLifetimeManager());

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
