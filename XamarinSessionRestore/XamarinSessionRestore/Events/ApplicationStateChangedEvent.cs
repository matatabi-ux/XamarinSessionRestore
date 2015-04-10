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
using Microsoft.Practices.Prism.PubSubEvents;

namespace XamarinSessionRestore.Events
{
    /// <summary>
    /// アプリケーション状態遷移の列挙値
    /// </summary>
    public enum ApplicationState
    {
        /// <summary>
        /// 起動中
        /// </summary>
        Starting = 0,

        /// <summary>
        /// 中断中
        /// </summary>
        Sleeping = 1,

        /// <summary>
        /// 再開中
        /// </summary>
        Resuming = 2,
    }

    /// <summary>
    /// アプリケーション状態遷移イベント
    /// </summary>
    public class ApplicationStateChangedEvent : PubSubEvent<ApplicationState>
    {
    }
}
