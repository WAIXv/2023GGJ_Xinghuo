using System;
using System.IO;
using UnityEngine;

namespace Utils.JsonSaveSystem {
    [Obsolete]
    public class JsonSaveSystem {
        #region Static

        // TODO 接入单例统一接口
        private static readonly JsonSaveSystem _instance = new JsonSaveSystem();

        public static JsonSaveSystem Instance => _instance;

        #endregion
        
        public bool TrySaveDataByJson<TData>(TData data, string path) {
            if (!File.Exists(path)) {
                Debug.LogError($"{nameof(JsonSaveSystem)}读取的文件不存在！路径:{path}");
                return false;
            }

            var jsonStr = JsonUtility.ToJson(data);
            File.WriteAllText(path, jsonStr);
            return true;
        }

        public bool TryLoadDataByJson<TData>(out TData data, string path) {
            if (!File.Exists(path)) {
                Debug.LogError($"{nameof(JsonSaveSystem)}读取的文件不存在！路径:{path}");
                data = default(TData);
                return false;
            }

            var jsonStr = File.ReadAllText(path);
            data = JsonUtility.FromJson<TData>(jsonStr);
            return true;
        }

        public void DeleteFile(string path) {
            if (!File.Exists(path)) return;

            File.Delete(path);
        }
    }
}