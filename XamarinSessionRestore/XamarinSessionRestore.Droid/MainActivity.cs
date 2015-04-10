using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Practices.Unity;
using XamarinSessionRestore.Droid.Repositories;
using XamarinSessionRestore.Repositories;

namespace XamarinSessionRestore.Droid
{
    [Activity(Label = "XamarinSessionRestore", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            App.Container.RegisterType<ISessionRepository, XmlRepositories>(new ContainerControlledLifetimeManager());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

