#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamarinSessionRestore
{
    /// <summary>
    /// シンプルなログ出力クラス
    /// </summary>
    public class SimpleLogger
    {
        /// <summary>
        /// ログ出力します
        /// </summary>
        /// <param name="message">出力内容</param>
        /// <param name="member">メソッド名</param>
        /// <param name="filepath">ファイルパス</param>
        public static void WriteLine(string message, [CallerMemberName] string member = "", [CallerFilePath] string filepath = "")
        {
            Debug.WriteLine(string.Format("{0}.{1}: {2}", Path.GetFileNameWithoutExtension(filepath).Split('\\').LastOrDefault(), member, message));
        }
    }
}
