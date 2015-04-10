#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace XamarinSessionRestore.ViewModels
{
    /// <summary>
    /// トップ画面の ViewModel
    /// </summary>
    public class TopPageViewModel : BindableBase
    {
        /// <summary>
        /// 日付情報
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 時間情報
        /// </summary>
        public TimeSpan TimeSpan { get; set; }

        /// <summary>
        /// 子 ViewModel 情報
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; set; }

        public TopPageViewModel()
        {
            this.DateTime = DateTime.Now;
            this.TimeSpan = TimeSpan.FromTicks(this.DateTime.Ticks);
            this.Items = new ObservableCollection<ItemViewModel>
            {
                new ItemViewModel
                {
                    DateTime = DateTime.Now,
                    TimeSpan = TimeSpan.FromTicks(this.DateTime.Ticks),
                },
            };


        }
    }
}
