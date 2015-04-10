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
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using XamarinSessionRestore.Repositories;

#if __IOS__
namespace XamarinSessionRestore.iOS.Repositories
#else
namespace XamarinSessionRestore.Droid.Repositories
#endif
{
    /// <summary>
    /// XML ���|�W�g��
    /// </summary>
    public class XmlRepositories : ISessionRepository
    {
        /// <summary>
        /// �ۑ���t�H���_
        /// </summary>
        private static readonly string Folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        /// <summary>
        /// �f�[�^�X�g�A
        /// </summary>
        private Dictionary<Type, object> storage = new Dictionary<Type, object>();

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        static XmlRepositories()
        {

        }

        /// <summary>
        /// �f�[�^��ݒ肵�܂�
        /// </summary>
        /// <typeparam name="T">�f�[�^�̌^</typeparam>
        /// <param name="value">�f�[�^</param>
        public void SetValue<T>(T value) where T : class
        {
            this.storage[typeof(T)] = value;
        }

        /// <summary>
        /// �f�[�^���擾���܂�
        /// </summary>
        /// <typeparam name="T">�f�[�^�̌^</typeparam>
        /// <returns>�f�[�^</returns>
        public T GetValue<T>() where T : class
        {
            if (!this.storage.ContainsKey(typeof(T)))
            {
                return null;
            }
            return this.storage[typeof(T)] as T;
        }

        /// <summary>
        /// ���������܂�
        /// </summary>
        /// <returns>���������ꍇ<code>true</code>�A����ȊO��<code>false</code></returns>
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
        /// �Z�b�V�����f�[�^��ǂݍ��݂܂�
        /// </summary>
        /// <returns>���������ꍇ<code>true</code>�A����ȊO��<code>false</code></returns>
        public async Task<bool> LoadAsync()
        {
            var result = true;

            try
            {
                var types = new List<string>();
                var indexFile = Path.Combine(Folder, "session-keys.xml");

                using (var stream = new StreamReader(indexFile))
                {
                    var serializer = new XmlSerializer(types.GetType());
                    types = serializer.Deserialize(stream) as List<string>;
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
                    var file = Path.Combine(Folder, string.Format("{0}.xml", data.Key.Name));
                    using (var stream = new StreamReader(file))
                    {
                        var serializer = new XmlSerializer(data.Key);
                        this.storage[data.Key] = serializer.Deserialize(stream);
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
        /// �Z�b�V�����f�[�^��ۑ����܂�
        /// </summary>
        /// <returns>���������ꍇ<code>true</code>�A����ȊO��<code>false</code></returns>
        public async Task<bool> SaveAsync()
        {
            var result = true;

            try
            {
                var types = (from t in this.storage
                             select t.Key.AssemblyQualifiedName).ToList();
                var indexFile = Path.Combine(Folder, "session-keys.xml");
                using (var stream = new StreamWriter(indexFile))
                {
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
                    var file = Path.Combine(Folder, string.Format("{0}.xml", data.Key.Name));
                    using (var stream = new StreamWriter(file))
                    {
                        var serializer = new XmlSerializer(data.Key);
                        serializer.Serialize(stream, data.Value);
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