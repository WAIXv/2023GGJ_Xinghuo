using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

namespace Utils.Serialization {
    /// <summary>
    /// 文件枚举
    /// </summary>
    /// <author>PhantasiaOwO</author>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum FolderType {
        /// <summary>
        /// 路径点系统相关
        /// </summary>
        WayPoint,
    }

    /// <summary>
    /// 文件选择器，统一获取文件的路径
    /// </summary>
    /// <author>PhantasiaOwO</author>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class FilePathGenerator {
        /// <summary>
        /// 系统保护路径
        /// </summary>
        private static string PersistentPath => Application.persistentDataPath;

        /// <summary>
        /// 资源文件夹
        /// </summary>
        private static string ResourcesPath => Path.Combine(Application.dataPath, "Resources");

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="folder">文件夹类型</param>
        /// <param name="path">文件名</param>
        /// <returns>文件的绝对路径</returns>
        public static string GenerateFilePath(FolderType folder, string path) {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Invalid file name.");

            return folder switch {
                FolderType.WayPoint => Path.Combine(ResourcesPath, path),
                _ => throw new ArgumentOutOfRangeException(nameof(folder), folder, null)
            };
        }
    }
}