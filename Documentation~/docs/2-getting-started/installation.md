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
  - com.cysharp.r3 (optional)
  - com.cysharp.unitask (optional)
```
- click Save
- open Package Manager
- Select ``My Registries`` in top left dropdown
- Select ``Kassets`` and click ``Install``
- Select ``R3`` and click ``Install`` (Optional) (see: Note)
- Select ``UniTask`` and click ``Install`` (Optional)

> [!NOTE]
> Installation for [R3] requires dependency imports from NuGet. See [R3 Unity Installation](https://github.com/Cysharp/R3?tab=readme-ov-file#unity) for further detail.

## GitHub URL

__Use the GitHub link for importing. Update using the Package Manager in Unity 2021.2 or later.__

The package can be added directly from GitHub on Unity 2019.4 and later versions.
To update to the main branch, use the Package Manager in Unity 2021.2 or later.
Otherwise, you need to update manually by removing and then adding back the package.

- Open the Package Manager
- Click the `+` icon
- Select the `Add from Git URL` option
- Paste the following URL: `https://github.com/kadinche/Kassets.git`
- Click `Add`

To install a specific version, you can refer to Kassets' release tags.
For example: `https://github.com/kadinche/Kassets.git#2.6.1`

## Clone to Packages Folder

Clone this repository to Unity Project's Packages directory.

Modify source codes from containing Unity Project.
Update changes to/from github directly just like usual github project.
You can also clone the project as Submodule.

- clone this project to `YourUnityProject/Packages/`


[Kassets]: https://github.com/kadinche/Kassets
[R3]: https://github.com/Cysharp/R3
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask
