var databaseSuffix = "suffix";

databaseSuffix = {{{ JSON.stringify(PRODUCT_NAME) }}}; // !

var databaseName = "game_database_" + databaseSuffix;
var databaseStore = "game_store_" + databaseSuffix;

var indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB || window.shimIndexedDB;
var open = indexedDB.open(databaseName, 1);

open.onupgradeneeded = function() 
{
    var db = open.result;
    var store = db.createObjectStore(databaseStore, {keyPath: "id"});
};

open.onsuccess = function() 
{
    var db = open.result;
    var tx = db.transaction(databaseStore, "readwrite");
    var store = tx.objectStore(databaseStore);
};

function IsWebStorageReady()
{
    return true;
}

function WebStorageSave(id, data)
{
  console.log("save id = %s", id);

  var db = open.result;
  var tx = db.transaction(databaseStore, "readwrite");
  var store = tx.objectStore(databaseStore);

  store.put({id: id, data: data});
}

function WebStorageGet(id, callback)
{
  console.log("get id = %s", id);

  var db = open.result;
  var tx = db.transaction(databaseStore, "readwrite");
  var store = tx.objectStore(databaseStore);

  var request = store.get(id);

  request.onsuccess = function() 
  {
    var result = request.result;
    if (result)
    {
      var data = result.data;
      console.log("get id = %s - has data", id);
      callback(data);
    }
    else
    {
      console.log("get id = %s - no data", id);
      callback("");
    }
  };   
}