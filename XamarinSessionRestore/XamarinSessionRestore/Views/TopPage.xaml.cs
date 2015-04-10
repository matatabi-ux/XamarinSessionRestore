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
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSessionRestore.ViewModels;

namespace XamarinSessionRestore.Views
{
    /// <summary>
    /// トップ画面
    /// </summary>
    public partial class TopPage : ContentPage
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TopPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面表示時の処理
        /// </summary>
        protected override void OnAppearing()
        {
            SimpleLogger.WriteLine("TopPage Appearing.");

            base.OnAppearing();
        }
    }
}
