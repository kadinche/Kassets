# Kassets

[![openupm](https://img.shields.io/npm/v/com.kadinche.kassets?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kadinche.kassets/)

Kassets is an implementation of [Scriptable Object](https://docs.unity3d.com/Manual/class-ScriptableObject.html) Architecture.
Scriptable Object Architecture provides a clean and decoupled code architecture.
It is implemented based on [Ryan Hipple's talk at Unite Austin 2017](https://youtu.be/raQ3iHhE_Kk).

Though Kassets can function independently,
it was originally developed to extend the functionality of the libraries [UniRx] and [UniTask] into the Scriptable Object Architecture.
Therefore, it is recommended for enhanced functionality to use Kassets in conjunction with either or both the [UniRx] and [UniTask].
To do so, simply import any or both of these libraries along with Kassets.

> [!NOTE]
> From v2.7.0, Kassets also supports [R3] as an alternative to [UniRx].
> Being marked as public archive, [UniRx] is now considered obsolete.
> Hence, all functionality of [UniRx] is now being forwarded to [R3].

[R3] The new future of dotnet/reactive and UniRx.

[UniRx] is a Reactive Extensions for Unity. 

[UniTask] provides an efficient allocation free async/await integration to Unity.

### Unity Version
- Unity 2021.3+
- Note that this GitHub project cannot be opened directly in Unity Editor. See [Installation](https://github.com/kadinche/Kassets#Installation) for cloning.

__For further details, see [Documentation]__

# Getting Started

## Installation

<details>
<summary>Add from OpenUPM | <em>Import via scoped registry. Update from Package Manager.</em></summary>

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
- click <kbd>Save</kbd>
- open Package Manager
- Select ``My Registries`` in top left dropdown
- Select ``Kassets`` and click ``Install``
- Select ``R3`` and click ``Install`` (Optional) (see: Note)
- Select ``UniTask`` and click ``Install`` (Optional)

> [!NOTE]
> Installation for [R3] requires dependency imports from NuGet. See [R3 Unity Installation](https://github.com/Cysharp/R3?tab=readme-ov-file#unity) for further detail.

</details>

<details>
<summary>Add from GitHub | <em>Use github link to import. Update from Package Manager on Unity 2021.2 or later.</em></summary>

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
</details>

<details>
<summary>Clone to Packages Folder | <em>For those who want to make and manage changes.</em></summary>

Clone this repository to Unity Project's Packages directory.

Modify source codes from containing Unity Project.
Update changes to/from github directly just like usual github project.
You can also clone the project as Submodule.

- clone this project to `YourUnityProject/Packages/`
</details>

## Creating Kassets' ScriptableObjects

### Create an Event Instance

Kassets instances can be created from Create context menu or from Assets/Create menu bar. Simply right click on the Project window and select any instance to create. For Event instances, select any of event types from `Create/Kassets/Game Events/` 

![CreateEvent](https://user-images.githubusercontent.com/1290720/137657526-5bbad8d6-a6c5-4361-a272-0ef30e684fad.gif)

### Create a Variable Instance

For Variables instances, select any of available types from `Create/Kassets/Variables/` 

![CreateVariable](https://user-images.githubusercontent.com/1290720/137657744-cc7ec167-d728-4a8a-8a6f-06b67bf01b14.gif)

Kassets' Variable instance on Inspector window

<img width="551" alt="Screenshot 2023-06-12 at 16 34 50" src="https://github.com/kadinche/Kassets/assets/1290720/3d6d1a6d-6b95-4395-8b94-a9426185ae14">

### Create Other Instances

Other available Kassets' ScriptableObjects are
- Command. An Abstract class that contains an `Execute()` method.
- Collection. Can be either a List or Dictionary.
- Transaction. A Request-Response event.

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

# Reactive with <s>UniRx</s> R3

> [!NOTE]
> Being marked as public archive, [UniRx] is now considered obsolete.
> If [UniRx] are imported together with [R3], All functionality of [UniRx] is being forwarded to [R3].
> Use `AsSystemObservable()` to convert to `IObservable` and continue using [UniRx].

If you had R3 imported, you can use Reactive on Kassets' instances.
First, make sure to import R3 to your project. Upon import, Kassets will adjust internally to support R3 using scripting define `KASSETS_R3`. It would normally be defined when R3 is imported using package manager. If somehow `KASSETS_R3` is undefined, add it to `Scripting Define Symbols` on Project Settings.

To use Kassets reactively, simply `Subscribe` to a GameEvent instances or its derivation.

```csharp
public class HealthBarUI : MonoBehavior
{
    [SerializeField] private FloatVariable health;
    [SerializeField] private FloatVariable maxHealth;
    [SerializeField] private Image healthBar;
    
    private IDisposable subscription;

    private void Start()
    {
        subscription = health.Subscribe(UpdateHealthBar);
    }

    private void UpdateHealthBar(float currentHealth)
    {
        var rt = healthBar.rectTransform;
        
        var sizeDelta = rt.sizeDelta;
        sizeDelta.x = 2 * (currentHealth / maxHealth);
        
        rt.sizeDelta = sizeDelta;
    }    
    
    private void OnDestroy()
    {
        subscription?.Dispose();
    }
}
```

# Asynchronous with UniTask

If you had UniTask imported, you can use Asynchronous on Kassets' instances. First, make sure to import UniTask to your project. Upon import, Kassets will adjust internally to support UniTask using scripting define `KASSETS_UNITASK`. It would normally be defined when UniTask is imported using package manager. If somehow `KASSETS_UNITASK` is undefined, add it to `Scripting Define Symbols` on Project Settings.

To use Kassets Asynchronously, use the method `EventAsync()` and add `await` in front of it. Any Kassets' instances that derived from GameEvent can be used asynchronously. (For Command, use method `ExecuteAsync()`)

```csharp
public class CounterAttackSkill: MonoBehaviour
{
    [SerializeField] private GameEvent counterActivateEvent;
    [SerializeField] private FloatGameEvent attackGameEvent;
    [SerializeField] private FloatVariable health;
    
    private IDisposable subscription;
    
    private void Start()
    {
        // When using subscribe await, next event raise will wait for current activated counter to end.
        subscription = counterActivateEvent.SubscribeAwait(async _ => await OnCounterActivate());
    }
    
    // Activate counter.
    private async UniTask OnCounterActivate()
    {
        var currentHealth = health.Value;
        
        // asynchronously wait until damaged, which indicated by health value changed event.
        var afterDamaged = await health.EventAsync(cancellationToken);
        
        var damage = currentHealth - afterDamaged;
        
        // raise attack event with damage value of damage received.
        attackGameEvent.Raise(damage):
    }
    
    private void OnDestroy()
    {
        subscription?.Dispose();
    }
}
```

In the example above, an asynchronous operation `EventAsync()` on variable means to wait for its value to change. GameEvent in general, will wait for an event to fire.

According to this [slide (Japanese)](https://speakerdeck.com/torisoup/unitask2020?slide=52), It is a best practice to always use cancellation token on every UniTask's asynchronous operation. Since Unity is not asynchronous, any asynchronous operation can be left behind waiting infinitely when the process is not stopped.

# Using UniTask.Linq and its Usages with R3

UniTask v2 has support for Asynchronous LINQ. Asynchronous LINQ is an extension to `IUniTaskAsyncEnumerable<T>` and its usage can be very similar to UniRx, but the process behind it is different (UniRx is push-based while UniTask is pull-based).

Kassets' `ScriptableObject` also make use of Asynchronous LINQ. Kassets' `ScriptableObject` derived from `IUniTaskAsyncEnumerable<T>` so it is possible to directly apply various features of UniTask as explained in its [github](https://github.com/Cysharp/UniTask#asyncenumerable-and-async-linq) page or from this [slide](https://speakerdeck.com/torisoup/unitask2020?slide=110) (Japanese).

As an example, the sample class `CounterAttackSkill` above used `SubscribeAwait` which is part of UniTask.Linq. Since it is pull-based, when the process of `OnCounterActivate` is still running, it won't be called again until it is over no matter how many times the event has been raised during the process. Reversely, push-based will execute every event raise.

When both UniRx and UniTask are imported together, It can be confusing which of the `Subscription` behavior is in effect (pull-based or push-based?). To use Kassets' instance as `IObservable`, use `AsObservable()`. To use Kassets' instance as `IUniTaskAsyncEnumerable` use `AsAsyncEnumerable()`. Unless referenced by interface, Kassets instances Default Subscribe Behavior can be selected from the inspector window.

<img width="514" alt="Screenshot 2023-06-12 at 16 49 31" src="https://github.com/kadinche/Kassets/assets/1290720/dea3da9d-cc3e-45a2-82a5-e590cfca84ee">

> [!NOTE]
> UniTask Asynchronous LINQ is part of `Cysharp.Threading.Tasks.Linq` namespace. To use, add `UniTask.Linq` as reference to your project's Assembly Definition. 

> [!NOTE]
> From v2.7.0, Upon importing [R3], handling pull/push based can be done by converting Kassets instances using `AsObservable()` for push-based, and `ToAsyncEnumerable()` for pull-based.

# [Documentation]

# References:
- [https://github.com/roboryantron/Unite2017](https://github.com/roboryantron/Unite2017)
- [https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects](https://www.slideshare.net/RyanHipple/game-architecture-with-scriptable-objects)
- [R3 — A New Modern Reimplementation of Reactive Extensions for C#](https://neuecc.medium.com/r3-a-new-modern-reimplementation-of-reactive-extensions-for-c-cf29abcc5826)
- [https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/](https://forpro.unity3d.jp/unity_pro_tips/2019/07/27/57/) (Japanese/日本語)
- [https://github.com/neuecc/UniRx](https://github.com/neuecc/UniRx)
- [https://github.com/Cysharp/UniTask](https://github.com/Cysharp/UniTask)
- [https://speakerdeck.com/torisoup/unitask2020](https://speakerdeck.com/torisoup/unitask2020) (Japanese/日本語)

# LICENSE

- Kassets is Licensed under [MIT License](https://github.com/kadinche/Kassets/blob/main/LICENSE.txt)
- [R3] is Licensed under [MIT License](https://github.com/Cysharp/R3/blob/main/LICENSE)
- [UniRx] is Licensed under  [MIT License](https://github.com/neuecc/UniRx/blob/master/LICENSE)
- [UniTask] is Licensed under  [MIT License](https://github.com/Cysharp/UniTask/blob/master/LICENSE)

[R3]: https://github.com/Cysharp/R3
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask
[Documentation]: https://Kadinche.github.io/Kassets/