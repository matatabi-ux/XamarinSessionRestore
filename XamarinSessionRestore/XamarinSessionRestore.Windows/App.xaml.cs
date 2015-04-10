using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using XamarinSessionRestore.Events;
using XamarinSessionRestore.Repositories;
using XamarinSessionRestore.WinPhone.Repositories;

// 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください

namespace XamarinSessionRestore.Windows
{
    /// <summary>
    /// 既定の Application クラスに対してアプリケーション独自の動作を実装します。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// イベントアグリゲーター
        /// </summary>
        private IEventAggregator eventAggregator = null;

        /// <summary>
        /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        /// 最初の行であり、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.eventAggregator = XamarinSessionRestore.App.Container.Resolve<IEventAggregator>();
            this.Suspending += OnSuspending;
            this.Resuming += (s, e) =>
            {
                // 再開した場合は Xamarin.Forms App Lifecycle に伝える
                this.eventAggregator.GetEvent<ApplicationStateChangedEvent>().Publish(ApplicationState.Resuming);
            };
            XamarinSessionRestore.App.Container.RegisterType<ISessionRepository, XmlRepositories>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。他のエントリ ポイントは、
        /// アプリケーションが特定のファイルを開くために呼び出されたときなどに使用されます。
        /// </summary>
        /// <param name="e">起動要求とプロセスの詳細を表示します。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // ウィンドウに既にコンテンツが表示されている場合は、アプリケーションの初期化を繰り返さずに、
            // ウィンドウがアクティブであることだけを確認してください
            if (rootFrame == null)
            {
                // ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
                rootFrame = new Frame();
                // 既定の言語を設定します
                rootFrame.Language = ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                // add this line
                Xamarin.Forms.Forms.Init(e); // requires LaunchActivatedEventArgs

                // フレームを現在のウィンドウに配置します
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // ナビゲーションの履歴スタックが復元されていない場合、最初のページに移動します。
                // このとき、必要な情報をナビゲーション パラメーターとして渡して、新しいページを
                // 作成します
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // 現在のウィンドウがアクティブであることを確認します
            Window.Current.Activate();

            // 起動したことを Xamarin.Forms App Lifecycle に伝える
            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                // Terminated からの起動は再開扱い
                this.eventAggregator.GetEvent<ApplicationStateChangedEvent>().Publish(ApplicationState.Resuming);
            }
            else
            {
                this.eventAggregator.GetEvent<ApplicationStateChangedEvent>().Publish(ApplicationState.Starting);
            }
        }

        /// <summary>
        /// 特定のページへの移動が失敗したときに呼び出されます
        /// </summary>
        /// <param name="sender">移動に失敗したフレーム</param>
        /// <param name="e">ナビゲーション エラーの詳細</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// アプリケーションの実行が中断されたときに呼び出されます。アプリケーションの状態は、
        /// アプリケーションが終了されるのか、メモリの内容がそのままで再開されるのか
        /// わからない状態で保存されます。
        /// </summary>
        /// <param name="sender">中断要求の送信元。</param>
        /// <param name="e">中断要求の詳細。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
            deferral.Complete();
        }
    }
}
