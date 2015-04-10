#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinSessionRestore.Events;
using XamarinSessionRestore.Repositories;
using XamarinSessionRestore.ViewModels;
using XamarinSessionRestore.Views;

namespace XamarinSessionRestore
{
    /// <summary>
    /// アプリケーション基盤クラス
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// セッションデータリポジトリ
        /// </summary>
        private ISessionRepository sessionRepository = null;

        /// <summary>
        /// DI コンテナ
        /// </summary>
        public static UnityContainer Container = new UnityContainer();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static App()
        {
            // EventAggregatorを登録
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

            // アプリケーション状態遷移イベントを購読
            Container.Resolve<IEventAggregator>().GetEvent<ApplicationStateChangedEvent>().Subscribe(
                (e) =>
                {
                    switch (e)
                    {
                        // 起動時
                        case ApplicationState.Starting:
                            ((App)App.Current).OnStart();
                            break;

                        // 中断時
                        case ApplicationState.Sleeping:
                            // とりあえず何もしない
                            break;

                        // 再開時
                        case ApplicationState.Resuming:
                            ((App)App.Current).OnResume();
                            break;
                    }
                });
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public App()
        {
            this.MainPage = new TopPage();

            this.sessionRepository = Container.Resolve<ISessionRepository>();
            this.sessionRepository.Initilize();
        }

        /// <summary>
        /// アプリ起動時処理
        /// </summary>
        protected override async void OnStart()
        {
            SimpleLogger.WriteLine("activated.");

            await this.sessionRepository.LoadAsync();
            var vm = this.sessionRepository.GetValue<TopPageViewModel>();

            if (vm != null)
            {
                this.MainPage.BindingContext = vm;
                SimpleLogger.WriteLine("Restore session data.");
            }
            else
            {
                this.MainPage.BindingContext = new TopPageViewModel();
                this.sessionRepository.SetValue<TopPageViewModel>(this.MainPage.BindingContext as TopPageViewModel);
                SimpleLogger.WriteLine("Create session data.");

                await this.sessionRepository.SaveAsync();
                SimpleLogger.WriteLine("Save session data.");
            }
        }

        /// <summary>
        /// アプリ中断時処理
        /// </summary>
        protected override async void OnSleep()
        {
            SimpleLogger.WriteLine("sleeped.");
        }

        /// <summary>
        /// アプリ再開時処理
        /// </summary>
        protected override async void OnResume()
        {
            this.sessionRepository = Container.Resolve<ISessionRepository>();
            await this.sessionRepository.LoadAsync();
            var vm = this.sessionRepository.GetValue<TopPageViewModel>();

            if (vm != null)
            {
                this.MainPage.BindingContext = vm;
                SimpleLogger.WriteLine("Restore session data.");
            }

            SimpleLogger.WriteLine("resumed.");
        }
    }
}
