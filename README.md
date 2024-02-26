
By default, Unity saves data in indexedDB, assigning IDs based on file paths and names.
his causes problems when rebuilding or updating games on platforms like itch.io or Kongregate, as IDs change and saved data breaks. 
This project provides a solution, demonstrating how to maintain stability by using a fixed address for indexedDB storage.



You have to include the javascript file Assets/WebGLTemplates/Custom/TemplateData/database.js in your html template.

```
<script src="TemplateData/database.js"></script>
```


Watch out for these files.

```
Assets/WebGLTemplates/Custom/TemplateData/database.js
Assets/Scripts/Storage.cs
Assets/Plugins/WebglStorage.jslib
```
