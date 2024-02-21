using System;
using System.Security.Cryptography;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Bro
{
    public static class Storage
    {
        public static void Save(string key, string data)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            var id = GetHash(key);
            WebStorage.WebStorageSave(id, data);
            return;
            #endif

            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }
        
        #pragma warning disable CS1998 
        public static async UniTask<string> Get(string key)
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            var id = GetHash(key);
            return await WebStorage.WebStorageGet(id);
            #endif
            
            return PlayerPrefs.GetString(key);
        }
        
        public static int GetHash(string key)
        {
            var md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToInt32(hashed, 0);
        }
    }
}