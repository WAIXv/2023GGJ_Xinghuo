using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils.Serialization {
    /// <summary>
    /// Json序列化工具
    /// </summary>
    /// <author>PhantasiaOwO</author>
    public static class JsonSerializer {
        /*
         * Windows平台下的换行为"\r\n"
         * 在下方的三个Convert方法使用这个来进行分割
         * 拥有文件操作的方法不需要这个进行分割
         */

        #region Serilization Rule

        private const string FileTypeFlag = "Json";

        private const int JsonFlagArea = 0;
        private const int TypeNameArea = 1;
        private const int DataArea = 2;

        #endregion

        #region Sync IO

        /// <summary>
        /// 转换为Json并保存
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">数据类/结构体对象</param>
        /// <typeparam name="TDataType">数据类/结构体的类型参数</typeparam>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        /// <exception cref="FileSaveException">保存文件异常</exception>
        public static void SaveFileByJson<TDataType>([NotNull] string filePath, [NotNull] TDataType data) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);
            if (data == null) throw new ArgumentNullException(nameof(data));

            var content = new string[3];
            content[JsonFlagArea] = FileTypeFlag; // 第一行为Json标识
            content[TypeNameArea] = typeof(TDataType).Name; // 第二行为类型表示
            content[DataArea] = JsonUtility.ToJson(data); // 第三行为Json字符串

            // IO操作
            if (!File.Exists(filePath)) { CreateDirectory(filePath); }
            File.WriteAllLines(filePath, content, Encoding.UTF8);
            if (!File.Exists(filePath)) { throw new FileSaveException(filePath); }
        }

        /// <summary>
        /// 转换为Json并保存
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">数据类/结构体对象</param>
        /// <param name="type">数据类/结构体对象的类型</param>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        /// <exception cref="FileSaveException">保存文件异常</exception>
        public static void SaveFileByJson([NotNull] string filePath, [NotNull] object data, [NotNull] Type type) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (type == null) throw new ArgumentNullException(nameof(type));

            var content = new string[3];
            content[JsonFlagArea] = FileTypeFlag; // 第一行为Json标识
            content[TypeNameArea] = type.Name; // 第二行为类型表示
            content[DataArea] = JsonUtility.ToJson(data); // 第三行为Json字符串

            // IO操作
            if (!File.Exists(filePath)) { CreateDirectory(filePath); }
            File.WriteAllLines(filePath, content, Encoding.UTF8);
            if (!File.Exists(filePath)) { throw new FileSaveException(filePath); }
        }

        /// <summary>
        /// 读取Json保存的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <typeparam name="TDataType">数据类/结构体的类型参数</typeparam>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        /// <exception cref="FileLoadException">读取文件异常</exception>
        public static TDataType LoadFileByJson<TDataType>([NotNull] string filePath) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);

            // IO操作
            if (!File.Exists(filePath)) { throw new FileLoadException(); }

            var content = File.ReadAllLines(filePath, Encoding.UTF8);

            // 如果开头的Flag与标准不匹配，则不满足序列化规则
            if (!content[JsonFlagArea].Equals(FileTypeFlag)) { throw new InvalidDataException(); }

            // 类型参数检查，如果不一致需要报错，而不是让JsonUtility使用类的默认值
            if (!content[TypeNameArea].Equals(typeof(TDataType).Name)) { throw new TypeArgumentException(filePath, content[TypeNameArea]); }

            return JsonUtility.FromJson<TDataType>(content[DataArea]);
        }

        /// <summary>
        /// 读取Json保存的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="result">读取的结果</param>
        /// <typeparam name="TDataType">数据类/结构体的类型参数</typeparam>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        /// <exception cref="FileLoadException">读取文件异常</exception>
        public static void LoadFileByJson<TDataType>([NotNull] string filePath, out TDataType result) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);

            // IO操作
            if (!File.Exists(filePath)) { throw new FileLoadException(); }

            var content = File.ReadAllLines(filePath, Encoding.UTF8);

            // 如果开头的Flag与标准不匹配，则不满足序列化规则
            if (!content[JsonFlagArea].Equals(FileTypeFlag)) { throw new InvalidDataException(); }

            // 类型参数检查，如果不一致需要报错，而不是让JsonUtility使用类的默认值
            if (!content[TypeNameArea].Equals(typeof(TDataType).Name)) { throw new TypeArgumentException(filePath, content[TypeNameArea]); }

            result = JsonUtility.FromJson<TDataType>(content[DataArea]);
        }

        /// <summary>
        /// 读取Json保存的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="type">对象的类型</param>
        /// <returns></returns>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        /// <exception cref="FileLoadException">读取文件异常</exception>
        public static object LoadFileByJson([NotNull] string filePath, [NotNull] Type type) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);
            if (type == null) throw new ArgumentNullException(nameof(type));

            // IO操作
            if (!File.Exists(filePath)) { throw new FileLoadException(); }

            var content = File.ReadAllLines(filePath, Encoding.UTF8);

            // 如果开头的Flag与标准不匹配，则不满足序列化规则
            if (!content[JsonFlagArea].Equals(FileTypeFlag)) { throw new InvalidDataException(); }

            // 类型参数检查，如果不一致需要报错，而不是让JsonUtility使用类的默认值
            if (!content[TypeNameArea].Equals(type.Name)) { throw new TypeArgumentException(filePath, content[TypeNameArea]); }

            return JsonUtility.FromJson(content[DataArea], type);
        }

        /*
        /// <summary>
        /// 将特定文本类型转换为数据
        /// </summary>
        /// <param name="customString"></param>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns></returns>
        public static TDataType ConvertStringToData<TDataType>(string customString) {
            var content = customString.Split("\r\n");
            
            // 如果开头的Flag与标准不匹配，则不满足序列化规则
            if (!content[JsonFlagArea].Equals(FileTypeFlag)) { throw new InvalidDataException(); }
        
            // 类型参数检查，如果不一致需要报错，而不是让JsonUtility使用类的默认值
            if (!content[TypeNameArea].Equals(typeof(TDataType).Name)) { throw new TypeArgumentException(content[TypeNameArea]); }
            
            return JsonUtility.FromJson<TDataType>(content[DataArea]);
        }
        
        /// <summary>
        /// 将特定文本类型转换为数据
        /// </summary>
        /// <param name="customString"></param>
        /// <param name="data"></param>
        /// <typeparam name="TDataType"></typeparam>
        public static void ConvertStringToData<TDataType>(string customString, out TDataType data) {
            var content = customString.Split("\r\n");
        
            // 如果开头的Flag与标准不匹配，则不满足序列化规则
            if (!content[JsonFlagArea].Equals(FileTypeFlag)) { throw new InvalidDataException(); }
        
            // 类型参数检查，如果不一致需要报错，而不是让JsonUtility使用类的默认值
            if (!content[TypeNameArea].Equals(typeof(TDataType).Name)) { throw new TypeArgumentException(content[TypeNameArea]); }
            
            data = JsonUtility.FromJson<TDataType>(content[DataArea]);
        }
        
        /// <summary>
        /// 将数据转换为特定字符串
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns></returns>
        public static string ConvertDataToString<TDataType>(TDataType data) {
            var sb = new StringBuilder("Json\n");
            sb.Append($"{typeof(TDataType).Name}\n");
            sb.Append(JsonUtility.ToJson(data));
            return sb.ToString();
        }
        */

        #endregion

        #region Wrapper of File Operation

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        public static void RemoveFile([NotNull] string filePath) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);

            if (!File.Exists(filePath)) { return; }

            File.Delete(filePath);
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <exception cref="InvalidPathException">路径不合法异常</exception>
        private static void CreateDirectory([NotNull] string filePath) {
            if (!Path.IsPathRooted(filePath)) throw new InvalidPathException(filePath);

            // 使用栈存储，逐级向上检查
            var path = new Stack<string>();
            path.Push(Path.GetDirectoryName(filePath));
            while (path.Count > 0 && !Directory.Exists(path.Peek()) && path.Peek() != null) {
                if (path.Count > 20) { throw new InvalidPathException("Too much unreachable folder in the path.", filePath); }

                path.Push(Path.GetDirectoryName(path.Peek()));
            }

            while (path.Count > 0) {
                Directory.CreateDirectory(path.Pop());
            }
        }

        #endregion


        // TODO Async Version of Serializer
    }
}