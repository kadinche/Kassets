---
sidebar_position: 2
---

# JSON Extensions

Kassets instances that is derived from `IVariable` can be serialized to JSON format.
An extension method `ToJsonString` and `FromJsonString` is provided to serialize the instance to JSON format.

You can also handle conversion of the JSON file from the inspector. 
The following settings can be customized.

- `Json File Path` : The path where the JSON file will be saved.
  - `Data Path` : Points to `Application.dataPath` folder.
  - `Persistent Data Path` : Points to `Application.persistentDataPath` folder.
  - `Custom Path` : Custom path where the JSON file will be saved.
- `File Name` : The name of the JSON file.
  - `Default` : If checked, the file name will be the same as the instance name.
- `Save to Json` : Button to save the instance to the JSON file specified by settings above.
- `Load from Json` : Button to load the instance from the JSON file specified by settings above.

![image](https://github.com/kadinche/Kassets/assets/1290720/2a3129b0-cc2e-4ad1-97da-355af8850166)
