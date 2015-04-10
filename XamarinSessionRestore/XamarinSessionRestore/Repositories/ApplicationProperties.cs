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
    /// アプリケーション Properties
    /// </summary>
    public class ApplicationProperties : ISessionRepository
    {
        /// <summary>
        /// データを設定します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="value">データ</param>
        public void SetValue<T>(T value) where T : class
        {
            App.Current.Properties[typeof(T).FullName] = value;
        }

        /// <summary>
        /// データを取得します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <returns>データ</returns>
        public T GetValue<T>() where T : class
        {
            if (!App.Current.Properties.ContainsKey(typeof (T).FullName))
            {
                return null;
            }
            return App.Current.Properties[typeof(T).FullName] as T;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        public bool Initilize()
        {
            try
            {
                App.Current.Properties.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// セッションデータを読み込みます
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        public Task<bool> LoadAsync()
        {
            // 読み込み不要
            return Task.FromResult(true);
        }

        /// <summary>
        /// セッションデータを保存します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        public Task<bool> SaveAsync()
        {
            // 保存不要
            return Task.FromResult(true);
        }
    }
}
