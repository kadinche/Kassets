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
<summary>Clone to Packages Folder | <em>for those who want to make and manage changes</em></summary>

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

### Create an Event Instance

Kassets instances can be created from Create context menu or from Assets/Create menu bar. Simply right click on the Project window and select any instance to create. For Event instances, select any of event types from `Create/Kassets/Game Events/` 

![CreateEvent](https://user-images.githubusercontent.com/1290720/137657526-5bbad8d6-a6c5-4361-a272-0ef30e684fad.gif)

### Create a Variable Instance

For Variables instances, select any of available types from `Create/Kassets/Variables/` 

![CreateVariable](https://user-images.githubusercontent.com/1290720/137657744-cc7ec167-d728-4a8a-8a6f-06b67bf01b14.gif)

Kassets' Variable instance on Inspector window

<img width="488" alt="Screen Shot 2021-10-20 at 3 43 15" src="https://user-images.githubusercontent.com/1290720/138010918-0ac58dfc-dd64-4044-b0df-2473a4f9ca37.png">

### Create Other Instances

Other available Kassets' Scriptable Object are
- Command. Class that contains an executable method.
- Collection. Can be either a List or Dictionary.
- ExchangeEvent. A Request-Response event.

## Using Kassets' ScriptableObject Instances

### Usage on MonoBehavior Script

Create a `MonoBehavior` Script and add Kassets' instance as a serialized field.

`Player.cs` :

<img width="431" alt="Screen Shot 2020-11-16 at 20 47 36 copy" src="https://user-images.githubusercontent.com/1290720/99601499-dda94c00-2a42-11eb-8c4c-5378a38f5602.png">

`HealthBarUI.cs` :

<img width="396" alt="Screen Shot 2020-11-16 at 21 06 10 copy" src="https://user-images.githubusercontent.com/1290720/99601502-df730f80-2a42-11eb-9c1f-0fa1bfad086a.png">

Drag and drop `PlayerHealth` (`FloatVariable`) to `Player`'s `Health` field :

<img width="877" alt="Screen Shot 2020-11-19 at 6 25 46" src="https://user-images.githubusercontent.com/1290720/99601580-0a5d6380-2a43-11eb-9d22-7428f7dae993.png">

Drag and drop `PlayerHealth` (`FloatVariable`) to `HealthBarUI`'s `Health` field :

<img width="878" alt="Screen Shot 2020-11-19 at 6 26 08" src="https://user-images.githubusercontent.com/1290720/99601584-0d585400-2a43-11eb-9ce3-dd143e260f0d.png">

From example above, `Player` component's field `Health` and `HealthBarUI` component's field `Health` both refer to the same FloatVariable ScriptableObject instance `PlayerHealth`. Since both component refer to the same variable instance, the `float` value they refer to is shared. Each component then manage their own need with the value without any coupling between components. I.e. `HealthBarUI` doesn't have to request the `Health` value to `Player` component and Player component can manage its own `Health` without the need to distribute its value to other components.

### Usage on UnityEvents

Kassets’s instance is a ScriptableObject asset. It can be referenced to UnityEvent via Inspector. You can aslo use dynamic method call on UnityEvent to pass a parameter.

![UnityEvent](https://user-images.githubusercontent.com/1290720/138039011-f32deac1-de5c-48ea-afaa-bf0f815b448d.gif)

![UnityEventDynamic](https://user-images.githubusercontent.com/1290720/138039041-0b42cbe8-254f-40ef-a2df-a45a19883c98.gif)

# Reactive with UniRx

If you had UniRx imported, you can use Reactive on Kassets' instances. First, make sure to import UniRx to your project. Upon import, Kassets will adjust internally to support UniRx using scripting define `KASSETS_UNIRX`. It would normally be defined when UniRx is imported using package manager. If somehow `KASSETS_UNIRX` is undefined, add it to `Scripting Define Symbols` on Project Settings.

When importing UniRx, Kassets' GameEvent becomes `Observable`. To use Kassets reactively, simply `Subscribe` to a GameEvent instances or its derivation.

![Screen Shot 2021-10-18 at 11 19 20](https://user-images.githubusercontent.com/1290720/137659609-61510ee1-44b3-4286-bc2a-993a6e369b59.png)

# Asynchronous with UniTask

If you had UniTask imported, you can use Asynchronous on Kassets' instances. First, make sure to import UniTask to your project. Upon import, Kassets will adjust internally to support UniTask using scripting define `KASSETS_UNITASK`. It would normally be defined when UniTask is imported using package manager. If somehow `KASSETS_UNITASK` is undefined, add it to `Scripting Define Symbols` on Project Settings.

To use Kassets Asynchronously, use the method `EventAsync()` and add `await` in front of it. Any Kassets' instances that derived from GameEvent can be used asynchronously. (For Command, use method `ExecuteAsync()`)

![Screen Shot 2021-10-18 at 11 40 16](https://user-images.githubusercontent.com/1290720/137661440-9f5800c9-3081-4e9a-b61a-90edd4573d40.png)

In the example above, an asynchronous operation `EventAsync()` on variable means to wait for its value to change. GameEvent in general, will wait for an event to fire.

According to this [slide (Japanese)](https://speakerdeck.com/torisoup/unitask2020?slide=52), It is a best practice to always use cancellation token on every UniTask's asynchronous operation. Since Unity is not asynchronous, any asynchronous operation can be left behind waiting infinitely when the process is not stopped.

# Using UniTask.Linq and its Usages with UniRx

UniTask v2 has support for Asynchronous LINQ. Asynchronous LINQ is an extension to `IUniTaskAsyncEnumerable<T>` and its usage can be very similar to UniRx, but the process behind it is different (UniRx is push-based while UniTask is pull-based).

Kassets' `ScriptableObject` also make use of Asynchronous LINQ. Kassets' `ScriptableObject` derived from `IUniTaskAsyncEnumerable<T>` so it is possible to directly apply various features of UniTask as explained in its [github](https://github.com/Cysharp/UniTask#asyncenumerable-and-async-linq) page or from this [slide](https://speakerdeck.com/torisoup/unitask2020?slide=110) (Japanese).

As an example, the sample class `CounterAttackSkill` above used `SubscribeAwait` which is part of UniTask.Linq. Since it is pull-based, when the process of `OnCounterActivate` is still running, it won't be called again until it is over no matter how many times the event has been raised during the process. Reversely, push-based will execute every event raise.

When both UniRx and UniTask are imported together, It can be confusing which of the `Subscription` behavior is in effect (pull-based or push-based?). Unless referenced by interface such as `IObservable` or `IUniTaskAsyncEnumerable`, Kassets instances Default Subscribe Behavior can be selected from the inspector window.

Note that UniTask Asynchronous LINQ is part of `Cysharp.Threading.Tasks.Linq` namespace. To use, add `UniTask.Linq` as reference to your project's Assembly Definition.

![Screen Shot 2021-10-18 at 12 12 02](https://user-images.githubusercontent.com/1290720/137663587-c384158b-ee0a-4190-a896-ba8a62b6983b.png)

# References:
- [https://github.com/neuecc/UniRx](https://github.com/neuecc/UniRx)
- [https://github.com/Cysharp/UniTask](https://github.com/Cysharp/UniTask)
- [https://speakerdeck.com/torisoup/unitask2020](https://speakerdeck.com/torisoup/unitask2020) (Japanese/日本語)
- [https://github.com/roboryantron/Unite2017](https://github.com/roboryantron/Unite2017)
- [https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects](https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects)
- [https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/](https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/) (Japanese/日本語)

# LICENSE

- Kassets is Licensed under [MIT License](https://github.com/kadinche/Kassets/blob/main/LICENSE.txt)
- UniRx is Licensed under  [MIT License](https://github.com/neuecc/UniRx/blob/master/LICENSE)
- UniTask is Licensed under  [MIT License](https://github.com/Cysharp/UniTask/blob/master/LICENSE)
