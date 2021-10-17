# Kassets

[![openupm](https://img.shields.io/npm/v/com.kadinche.kassets?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kadinche.kassets/)

Kassets is an implementation of Scriptable Object Architecture. Scriptable Object Architecture provides a clean and decoupled code architecture. It is implemented based on [Ryan Hipple talk on Unite Austin 2017](https://youtu.be/raQ3iHhE_Kk).

Kassets can be used independently. However, Kassets was originally made as an attempt to extend functionality of UniRx and UniTask into ScriptableObjectArchitecture. So, it is recommended and would be more effective to use Kassets along with [UniRx], [UniTask], or both. To use them, simply import any or both of these library along with Kassets.

[UniRx](https://github.com/neuecc/UniRx) is a Reactive Extensions for Unity. 

[UniTask](https://github.com/Cysharp/UniTask) provides an efficient allocation free async/await integration to Unity.

### Unity Version
- Unity 2019.4+
- Note that this github project cannot be opened directly in Unity Editor. See [Installation](https://github.com/kadinche/Kassets#Installation) for cloning.

### Dependencies
- [UniRx](https://github.com/neuecc/UniRx) [Optional]
- [UniTask](https://github.com/Cysharp/UniTask) [Optional]

# Getting Started

## Installation

<details>
<summary>Add from OpenUPM | <em>import via scoped registry, update from Package Manager</em></summary>

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
- click <kbd>Save</kbd>
- open Package Manager
- Select ``My Registries`` in top left dropdown
- Select ``Kassets`` and click ``Install``
- Select ``UniRx`` and click ``Install`` (Optional)
- Select ``UniTask`` and click ``Install`` (Optional)
</details>

<details>
<summary>Add from GitHub | <em>cannot updates through Package Manager</em></summary>

Add package directly from GitHub on Unity 2019.4+.
You won't be able to receive updates through Package Manager this way, you'll have to update manually.

- open Package Manager
- click <kbd>+</kbd>
- select <kbd>Add from Git URL</kbd>
- paste `https://github.com/kadinche/Kassets.git`
- click <kbd>Add</kbd>
</details>

<details>
<summary>Clone to Packages Folder | <em>for those who want to contribute</em></summary>

Clone this repository to Unity Project's Packages directory.

Modify source codes from containing Unity Project.
Update changes to/from github directly just like usual github project.
You can also clone the project as Submodule.

- clone this project using https: https://github.com/kadinche/Kassets.git
- clone this project using ssh: git@github.com:kadinche/Kassets.git
- clone this project to YourUnityProject/Packages/
</details>

<details>
<summary><em>Importing UniRx and/or UniTask</em></summary>

Both UniRx and UniTask can be added either from OpenUPM or GitHub.

- scope on openupm:
  - [com.neuecc.unirx](https://openupm.com/packages/com.neuecc.unirx/)
  - [com.cysharp.unitask](https://openupm.com/packages/com.cysharp.unitask/)
- package link on github:
  - https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts
  - https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask
- for more detailed information, please look at the respective github pages.
</details>


## Creating Kassets' ScriptableObjects

### Create a Variable Instance

Variables can be created from Create context menu or from Assets/Create menu bar. Simply right click on the Project window and select any variables on `Create/Kassets/Variables/` 

<img width="770" alt="Screen Shot 2021-05-26 at 4 02 27" src="https://user-images.githubusercontent.com/1290720/119568205-3689e080-bde8-11eb-91e1-253a8cdaf4da.png">

### Create an Event Instance

For event instances, select any of events from `Create/Kassets/Events/` 

<img width="829" alt="Screen Shot 2021-05-26 at 4 05 49" src="https://user-images.githubusercontent.com/1290720/119568417-75b83180-bde8-11eb-940a-4aa00fa218a4.png">

### Create Other Instances

Other available Kassets' Scriptable Object are
- ExchangeEvent. A Request-Response event pair.
- Collection. Can be either a List or Dictionary.

(More Details TBD)

## Using Kassets' ScriptableObjects

Create a `MonoBehavior` Script and add variables and/or events as a serialized field.

`Player.cs` :

<img width="431" alt="Screen Shot 2020-11-16 at 20 47 36 copy" src="https://user-images.githubusercontent.com/1290720/99601499-dda94c00-2a42-11eb-8c4c-5378a38f5602.png">

`HealthBarUI.cs` :

<img width="396" alt="Screen Shot 2020-11-16 at 21 06 10 copy" src="https://user-images.githubusercontent.com/1290720/99601502-df730f80-2a42-11eb-9c1f-0fa1bfad086a.png">

Drag and drop `PlayerHealth` (`FloatVariable`) to `Player`'s `Health` field :

<img width="877" alt="Screen Shot 2020-11-19 at 6 25 46" src="https://user-images.githubusercontent.com/1290720/99601580-0a5d6380-2a43-11eb-9d22-7428f7dae993.png">

Drag and drop `PlayerHealth` (`FloatVariable`) to `HealthBarUI`'s `Health` field :

<img width="878" alt="Screen Shot 2020-11-19 at 6 26 08" src="https://user-images.githubusercontent.com/1290720/99601584-0d585400-2a43-11eb-9ce3-dd143e260f0d.png">

From example above, `Player` component's field `Health` and `HealthBarUI` component's field `Health` both refer to the same FloatVariable ScriptableObject instance `PlayerHealth`. Since both component refer to the same variable instance, the `float` value they refer to is shared. Each component then manage their own need with the value without any coupling between components. I.e. `HealthBarUI` doesn't have to request the `Health` value to `Player` component and Player component can manage its own `Health` without the need to distribute its value to other components.

# Asynchronous with UniTask

To use Kassets Asynchronously just add `await` in front of any of Kassets' ScriptableObject instance.

Asynchronous usage sample of FloatVariable `health` :

<img width="604" alt="Screen Shot 2020-11-17 at 5 46 08" src="https://user-images.githubusercontent.com/1290720/99601760-66c08300-2a43-11eb-9772-04efe3fec7b6.png">

To use cancellation token use `health.ValueAsync(token)`.

In the example above, an asynchronous operation on variable will wait for its value to change. In case of events, it will wait for any event to fire. To use cancellation token on event use `event.EventAsync(token)`.

According to this [slide (Japanese)](https://speakerdeck.com/torisoup/unitask2020?slide=52), It is a best practice to always use cancellation token on every UniTask's asynchronous operation. Since Unity is not asynchronous, any asynchronous operation can be left behind waiting infinitely when the process is not stopped.

In case of Kassets' `ScriptableObject`, a `CancellationTokenSource` is included in an instance's life cycle. As a result, it is assumed to be safe to use `await` directly on an instance. However, in Unity Editor, a `ScriptableObject` lifecycle is alongside with Unity Editor's lifetime. So, cancellation are handled on Kassets' `ScriptableObject` instance lifecycle.

# Using UniTask.Linq

UniTask v2 has support for Asynchronous LINQ. Asynchronous LINQ is an extension to `IUniTaskAsyncEnumerable<T>` and its usage can be very similar to UniRx, but the process behind it is different (UniRx is push-based while UniTask is pull-based).

Kassets' `ScriptableObject` also make use of Asynchronous LINQ. Kassets' `ScriptableObject` derived from `IUniTaskAsyncEnumerable<T>` so it is possible to directly apply various features of UniTask as explained in its [github](https://github.com/Cysharp/UniTask#asyncenumerable-and-async-linq) page or from this [slide](https://speakerdeck.com/torisoup/unitask2020?slide=110) (Japanese). An exception would be `GameEvent`(w/o value) which is a base class of `GameEvent<T>` (w/ value). It is necessary to call `AsUniTaskAsyncEnumerable()` on `GameEvent` which return `IUniTaskAsyncEnumerable<GameEvent>`. However it is possible to `Subscribe` directly to `GameEvent` as it is implemented manually.

Note that UniTask Asynchronous LINQ is part of `Cysharp.Threading.Tasks.Linq` namespace. To use, add `UniTask.Linq` as reference to your project's Assembly Definition.

Usage of Subscribe on health, one of Asynchronous LINQ feature :

<img width="536" alt="Screen Shot 2020-11-17 at 9 18 07" src="https://user-images.githubusercontent.com/1290720/99601821-8a83c900-2a43-11eb-885b-07e7c02c6de0.png">

# References:
- [https://github.com/neuecc/UniRx](https://github.com/neuecc/UniRx)
- [https://github.com/Cysharp/UniTask](https://github.com/Cysharp/UniTask)
- [https://speakerdeck.com/torisoup/unitask2020](https://speakerdeck.com/torisoup/unitask2020) (Japanese/日本語)
- [https://github.com/roboryantron/Unite2017](https://github.com/roboryantron/Unite2017)
- [https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects](https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects)
- [https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/](https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/) (Japanese/日本語)

# LICENSE

MIT
