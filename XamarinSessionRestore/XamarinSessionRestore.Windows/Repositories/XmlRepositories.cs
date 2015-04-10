#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using XamarinSessionRestore.Repositories;
using XamarinSessionRestore.ViewModels;

namespace XamarinSessionRestore.WinPhone.Repositories
{
    /// <summary>
    /// XML リポジトリ
    /// </summary>
    public class XmlRepositories : ISessionRepository
    {
        /// <summary>
        /// 保存先フォルダ
        /// </summary>
        private static readonly StorageFolder Folder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// データストア
        /// </summary>
        private Dictionary<Type, object> storage = new Dictionary<Type, object>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static XmlRepositories()
        {
            
        }

        /// <summary>
        /// データを設定します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="value">データ</param>
        public void SetValue<T>(T value) where T : class
        {
            this.storage[typeof(T)] = value;
        }

        /// <summary>
        /// データを取得します
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <returns>データ</returns>
        public T GetValue<T>() where T : class
        {
            if (!this.storage.ContainsKey(typeof(T)))
            {
                return null;
            }
            return this.storage[typeof(T)] as T;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        public bool Initilize()
        {
            try
            {
                this.storage.Clear();
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
        public async Task<bool> LoadAsync()
        {
            var result = true;

            try
            {
                var types = new List<string>();
                var indexFile = await Folder.GetFileAsync("session-keys.xml");

                using (var stream = await indexFile.OpenSequentialReadAsync())
                {
                    var serializer = new XmlSerializer(types.GetType());
                    types = serializer.Deserialize(stream.AsStreamForRead()) as List<string>;
                }
                foreach (var typename in types)
                {
                    this.storage[Type.GetType(typename)] = null;
                }
            }
            catch (Exception)
            {
            }

            foreach (var data in this.storage.ToList())
            {
                try
                {
                    var file = await Folder.GetFileAsync(string.Format("{0}.xml", data.Key.Name));
                    using (var stream = await file.OpenSequentialReadAsync())
                    {
                        var serializer = new XmlSerializer(data.Key);
                        this.storage[data.Key] = serializer.Deserialize(stream.AsStreamForRead());
                    }

                }
                catch (FileNotFoundException)
                {
                    this.storage[data.Key] = null;
                    continue;
                }
                catch (InvalidOperationException)
                {
                    this.storage[data.Key] = null;
                    continue;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// セッションデータを保存します
        /// </summary>
        /// <returns>成功した場合<code>true</code>、それ以外は<code>false</code></returns>
        public async Task<bool> SaveAsync()
        {
            var result = true;

            try
            {
                var types = (from t in this.storage
                             select t.Key.AssemblyQualifiedName).ToList();
                var indexFile = await Folder.CreateFileAsync("session-keys.xml", CreationCollisionOption.ReplaceExisting);
                using (var stream = await indexFile.OpenStreamForWriteAsync())
                {
                    var writer = XmlWriter.Create(
                         stream,
                         new XmlWriterSettings()
                         {
                             Encoding = Encoding.UTF8,
                             Indent = false,
                             NewLineChars = string.Empty,
                         });
                    var serializer = new XmlSerializer(types.GetType());
                    serializer.Serialize(stream, types);
                }
            }
            catch (Exception)
            {
            }

            foreach (var data in this.storage)
            {
                try
                {
                    var file = await Folder.CreateFileAsync(string.Format("{0}.xml", data.Key.Name), CreationCollisionOption.ReplaceExisting);
                    using (var stream = await file.OpenStreamForWriteAsync())
                    {
                        var writer = XmlWriter.Create(
                         stream,
                         new XmlWriterSettings()
                         {
                             Encoding = Encoding.UTF8,
                             Indent = false,
                             NewLineChars = string.Empty,
                         });
                        var serializer = new XmlSerializer(data.Key);
                        serializer.Serialize(writer, data.Value);
                    }
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
