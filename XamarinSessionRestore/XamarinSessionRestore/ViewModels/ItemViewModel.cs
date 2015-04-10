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
using Microsoft.Practices.Prism.Mvvm;

namespace XamarinSessionRestore.ViewModels
{
    /// <summary>
    /// アイテム ViewModel
    /// </summary>
    public class ItemViewModel : BindableBase
    {
        /// <summary>
        /// 日付情報
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 時間情報
        /// </summary>
        public TimeSpan TimeSpan { get; set; }
    }
}
