mergeInto(LibraryManager.library, {
 
    Internal_IsWebStorageReady: function () 
    {
        return IsWebStorageReady();
    },

    Internal_WebStorageSave: function (id, data) 
    {
        str = UTF8ToString(data)
        WebStorageSave(id, str);
    },

    Internal_WebStorageGet: function(id, callback)
    {
        WebStorageGet(id, function(result)
        {
            var bufferSize = lengthBytesUTF8(result) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(result, buffer, bufferSize);

            dynCall_vi(callback, buffer);
        });
    }
});