using System;
using System.IO;

namespace Utils.Serialization {
    /// <summary>
    /// 保存文件异常，在IO流读取文件错误时触发
    /// </summary>
    /// <author>PhantasiaOwO</author>
    public sealed class FileSaveException : FileNotFoundException {
        public FileSaveException(string filePath)
            : base($"Fail to save file because unable to find the specified file.\nFile path: \"{filePath}\".", filePath) { }
    }

    /// <summary>
    /// 类型不匹配异常，在Json转换类型不匹配时触发
    /// </summary>
    /// <author>PhantasiaOwO</author>
    public sealed class TypeArgumentException : ArgumentException {
        public TypeArgumentException(string filePath, string targetType)
            : base($"The type parameter is not accord with the serialized file.\nThe type of serialized file is \"{targetType}\".\nFile path: \"{filePath}\".") { }

        public TypeArgumentException(string targetType) : base($"The type parameter is not accord with the serialized file.\nThe type of serialized file is \"{targetType}\".\n") { }
    }

    /// <summary>
    /// 路径不合法异常，每次写入/读取文件时检查
    /// </summary>
    /// <author>PhantasiaOwO</author>
    public sealed class InvalidPathException : ArgumentException {
        public InvalidPathException(string filePath)
            : base($"The path is invalid in this operation system IO.\nFile path: \"{filePath}.\"") { }

        public InvalidPathException(string message, string filePath)
            : base($"{message}\nFile path: \"{filePath}\"") { }
    }
}