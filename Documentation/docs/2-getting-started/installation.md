---
sidebar_position: 1
---

# Installation

## OpenUPM 

__Import via scoped registry. Update from Package Manager.__

To add OpenUPM to your project:

- open `Edit/Project Settings/Package Manager`
- add a new Scoped Registry:
```
Name: OpenUPM
URL:  https://package.openupm.com/
Scope(s):
  - com.kadinche
  - com.neuecc.unirx (optional)
  - com.cysharp.unitask (optional)
```
- click Save
- open Package Manager
- Select ``My Registries`` in top left dropdown
- Select ``Kassets`` and click ``Install``
- Select ``UniRx`` and click ``Install`` (Optional)
- Select ``UniTask`` and click ``Install`` (Optional)

## GitHub URL

__Use github link to import. Update from Package Manager on Unity 2021.2 or later.__

Add package directly from GitHub on Unity 2019.4+.
Update to main branch from Package Manager on Unity 2021.2 or later.
Otherwise, you'll have to update manually by removing then adding back the package.

- open Package Manager
- click `+`
- select `Add from Git URL`
- paste `https://github.com/kadinche/Kassets.git`
- click `Add`

## Clone to Packages Folder

Clone this repository to Unity Project's Packages directory.

Modify source codes from containing Unity Project.
Update changes to/from github directly just like usual github project.
You can also clone the project as Submodule.

- clone this project to `YourUnityProject/Packages/`

## Importing UniRx and/or UniTask

Both UniRx and UniTask can be added either from OpenUPM or GitHub.

- scope on openupm:
    - [com.neuecc.unirx](https://openupm.com/packages/com.neuecc.unirx/)
    - [com.cysharp.unitask](https://openupm.com/packages/com.cysharp.unitask/)
- package link on github:
    - https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts
    - https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask
- for more detailed information, please look at the respective github pages.

[Kassets]: https://github.com/kadinche/Kassets
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask
