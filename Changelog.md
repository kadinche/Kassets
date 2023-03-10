# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Inspector button to save/load Variable derrived Kassets' Instance to/from json file.

### Changed

- Command's `Execute()` is now abstract on UniTask version and `ExecuteAsync()` is virtual (which simply calls `Execute()`).
- Fixed missing extension when checking existence of json file.
- Add log feedback when raising event from Inspector.
- Fixed some sample's dependency from UniRx.

## [2.5.1] - 2023-02-25

### Changed

- Fixed Editor Extension Custom Drawer to display properly when viewed over a Component.
- Json Extension Utility will convert custom types in root level without "value" label.
- Fixed Custom Editor not drawing Quaternion field properly on Unity 2020.

## [2.5.0] - 2023-01-19

### Added

- Sample for `KassetsJsonExtension` utility.

### Changed

- Fixed Editor Drawer compilation error on Unity 2020.3.
- Added Unity 2021 Conditional for `ObjectPool` samples.
- Added Non-Generic interface `IVariable` for Variable Systems.
- Json Extension Utility now use interface `IVariable<T>` as parameter instead of `VariableCore<T>`.
- Added Experimental feature on Json Extension Utility which use IVariable as parameter. This allows conversion to/from json string using reflection without knowing variable's generic type. Please use with care.

## [2.4.3] - 2022-10-26

### Changed
- Added `AsObservable()` and `AsAsyncEnumerable()` when both `UniRx` and `UniTask` exists together.
- Minor improvement and code formatting on editor code.
- Note : Updated minimum Unity version to 2020.1 due to some `C#` formatting.

## [2.4.2] - 2022-10-25

### Changed
- Fixed timing of entering play mode (use ExitingEditMode instead) which were used for `AutoResetValue` timing.

## [2.4.1] - 2022-10-24

### Added
- `QuaternionCollection`
- `PoseCollection`
- `InstanceSettings`: Grouped kassets instance's settings (i.e. Variable event type) into one class.

### Changed
- Fixed wrong type of `Vector3Collection`, from `Vector2` to the correct `Vector3`.
- Fixed conditionals (Scripting Define) on UniTask Samples.
- Improve Custom Editor code (Custom class no longer show "Value" dropdown).

### Known Issues
- Kassets' Instance Inline inspector editing for `Collection` currently doesn't work.
- Kassets' Instance Settings `Auto Reset Value` currently doesn't work properly.

## [2.4.0] - 2022-06-10

### Added
- Wrap Object Pool into Kassets' ScriptableObject
- Object Pool Sample
- Note: Object Pool is a new feature and only available from Unity version 2021 or later.

### Changed
- Fixed error when adding an element into `Collection<TKey, TValue>`
- Added Fire-and-Forget for `RequestResponse<TRequest, TResponse>`

## [2.3.1] - 2022-04-21

### Changed
- Fixed Subscribe to Request do not receive Request.

## [2.3.0] - 2022-04-21

### Added
- Added feature to Subscribe to Request of `RequestResponse<TRequest, TResponse>` without reponse.
- Added feature to Subscribe to Response of `RequestResponse<TRequest, TResponse>` without requesting.
- Added sample for Subscribe to Request and Subscribe to Response.

## [2.2.1] - 2022-04-14

### Changed
- Fixed `Collection<TKey, TValue>` doesn't reflect its value correctly as `IDictionary<TKey, TValue>`.
- Set independent `GameEvent` `buffered` default value to `false`.
- Fixed `Null Reference Exception` on subscription disposal and buffered subscription.

## [2.2.0] - 2022-04-04

### Added
- Scriptable Object can now be added to a Prefab or another Scriptable Object.
- Samples to demonstrate adding Scriptable Objects to a Prefab and other Scriptable Object.
- Refrenece : https://tsubakit1.hateblo.jp/entry/2017/08/03/233000

## [2.1.4] - 2022-03-29

### Changed
- Fixed `Variable`'s properties not drawn properly.

## [2.1.3] - 2022-03-29

### Added
- Editor Script to reorder `Variable` field. `Value` is now at the very bottom of the Inspector.

## [2.1.2] - 2022-02-15

### Changed
- Update `CancellationToken` handling.
- Handle `async` oprations using only UniTask when using Multiple library (both `UniRx` and `UniTask` imported).
- Fix typos

## [2.1.1] - 2022-01-09

### Changed
- `VariableCore` for `UniTask` now Inherits Interface `IAsyncReactiveProperty<>`
- Reword `Base` to `Core` for some filenames.
- `BoolUnityEventBinder` now handles negated `bool` value.
- `StringUnityEventBinder` now handles `ToString()` for value type other than string.
- Change subscription handling on `UnityEventBinder`.

## [2.1.0] - 2021-12-16

### Added
- Persistent Json Extension Utility for `VariableCore<T>` derivations.

## [2.0.0] - 2021-10-27

Major version upgrade. Not backward compatible with version 1.0.x.

### Added
- Interface ICommand, ICommand<T>
- Interface IGameEventRaiser, IGameEventRaiser<T>
- Interface IGameEventHandler, IGameEventHandler<T>
- Interface IVariable<T>
- KassetsCore. Base class for all Kassets' classes.
- Kassets now Independent from external libraries.
- Kassets supports two external libraries: [UniRx](https://github.com/neuecc/UniRx) and [UniTask](https://github.com/Cysharp/UniTask).
- Samples for each Kassets' classes.
- Editor Code to view or edit ScriptableObject's from MonoBehavior's Inspector window.

### Changed
- Renamed ExchangeEvent to RequestResponseEvent

### Removed
- DebugLogger.
- GameEvent Forwarder
- FileLoadUtility
- RecenterCommandBinder
- XRDeviceUtility
- TransformBindUtilityExtension
- GameObjectBindUtilityExtension
- ByteConverterUtilityExtension
- ExternalJsonHandler
- PersistentScriptableObject
- Kassets Unity's Components Binder
- Removed other Non-related component

## [1.0.1] - 2021-09-13
### Changed
- Fixed Transform binder comparison mistake.

## [1.0.0] - 2021-05-26
### Added
- Asynchronous Variable ScriptableObject
- Asynchronous GameEvent ScriptableObject
- Asynchronous Collection ScriptableObject
- Asynchronous ExchangeEvent (Transaction, Request-Response) Scriptable Object
- Binder and helper for Unity's Components (Transform, GameObject, etc)
- Binder and helper for Unity's UI Components
- Binder and helper for Unity's TextMeshPro
- Binder and helper for Unity's (new) Input System
- Release-excluded DebugLogger utility. See: [here](https://qiita.com/toRisouP/items/d856d65dcc44916c487d) and [there](https://baba-s.hatenablog.com/entry/2019/09/02/080000)
- PersistentScriptableObject. ScriptableObject that was made to be easily exported to json.
- ExternalJsonHandler. Unity's JsonUtility Extension Wrapper.
- ByteConverterUtilityExtension. Extention Methods to convert various value into byte array.
- GameObjectBindUtilityExtension. Extention Methods for Unity's GameObject to bind with Variable value.
- TransformBindUtilityExtension. Extention Methods for Unity's Transform to bind with values such as Position, Rotation, Pose, or bind with other Transforms.
- XRDeviceUtility. Contains XR related static methods and properties.
- RecenterCommandBinder. Bind GameEvents to XR Recenter Method.
- FileLoadUtility. Contains a static method to load/download files.
- GameEvent Forwarder. Forwards GameEvent to another GameEvent or UnityEvent.
- Context on DebugLogger. Click a log on Console to highlight the log caller's GameObject on hierarchy.
- Raise Button on Inspector. Click the button to simulate GameEvent Raise from inspector.
- CancellationTokenUtility. Helper class for cancelling and refreshing CancellationToken.

[Unreleased]: https://github.com/kadinche/Kassets/compare/2.5.1...HEAD
[2.5.1]: https://github.com/kadinche/Kassets/compare/2.5.0...2.5.1
[2.5.0]: https://github.com/kadinche/Kassets/compare/2.4.3...2.5.0
[2.4.3]: https://github.com/kadinche/Kassets/compare/2.4.2...2.4.3
[2.4.2]: https://github.com/kadinche/Kassets/compare/2.4.1...2.4.2
[2.4.1]: https://github.com/kadinche/Kassets/compare/2.4.0...2.4.1
[2.4.0]: https://github.com/kadinche/Kassets/compare/2.3.1...2.4.0
[2.3.1]: https://github.com/kadinche/Kassets/compare/2.3.0...2.3.1
[2.3.0]: https://github.com/kadinche/Kassets/compare/2.2.1...2.3.0
[2.2.1]: https://github.com/kadinche/Kassets/compare/2.2.0...2.2.1
[2.2.0]: https://github.com/kadinche/Kassets/compare/2.1.4...2.2.0
[2.1.4]: https://github.com/kadinche/Kassets/compare/2.1.3...2.1.4
[2.1.3]: https://github.com/kadinche/Kassets/compare/2.1.2...2.1.3
[2.1.2]: https://github.com/kadinche/Kassets/compare/2.1.1...2.1.2
[2.1.1]: https://github.com/kadinche/Kassets/compare/2.1.0...2.1.1
[2.1.0]: https://github.com/kadinche/Kassets/compare/2.0.0...2.1.0
[2.0.0]: https://github.com/kadinche/Kassets/compare/1.0.1...2.0.0
[1.0.1]: https://github.com/kadinche/Kassets/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/kadinche/Kassets/releases/tag/1.0.0
