#if UNITY_WEBGL
 
using System;
using System.Runtime.InteropServices;
using AOT;
using Cysharp.Threading.Tasks;

namespace Bro
{
    public static class WebStorage
    {
       
        [DllImport("__Internal")]
        private static extern bool Internal_IsWebStorageReady();
        
        [DllImport("__Internal")]
        private static extern void Internal_WebStorageSave(int id, string data);
        
        [DllImport("__Internal")]
        private static extern void Internal_WebStorageGet(int id, Action<string> callback);
        
        private static UniTaskCompletionSource<string> _getTaskSource;
 
        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void GetCallback(string val)
        {
            _getTaskSource.TrySetResult(val);  
        }
        
        public static bool IsWebStorageReady()
        {
            return Internal_IsWebStorageReady();
        }
        
        public static void WebStorageSave(int id, string data)
        {
            Internal_WebStorageSave(id, data);
        }
        
        public static UniTask<string> WebStorageGet(int id)
        {
            _getTaskSource = new UniTaskCompletionSource<string>();
            Internal_WebStorageGet(id, GetCallback);
            return _getTaskSource.Task;
        }
    }
}
#endif