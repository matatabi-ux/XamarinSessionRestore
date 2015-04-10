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

namespace XamarinSessionRestore.Repositories
{
    /// <summary>
    /// セッションデータリポジトリのインタフェース
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// データを設定します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="value">データ</param>
        void SetValue<T>(T value) where T : class;

        /// <summary>
        /// データを取得します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <returns>データ</returns>
        T GetValue<T>() where T : class;

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        bool Initilize();

        /// <summary>
        /// セッションデータを読み込みます
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        Task<bool> LoadAsync();

        /// <summary>
        /// セッションデータを保存します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        Task<bool> SaveAsync();
    }
}
