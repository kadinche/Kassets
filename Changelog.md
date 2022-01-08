# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.1.1] - 2022-01-09

### Changed
- VariableCore for UniTask now Inherits Interface IAsyncReactiveProperty<>
- Reword "Base" to "Core" for some filenames.
- BoolUnityEventBinder now handles negated bool value.
- StringUnityEventBinder now handles ToString() for value type other than string.
- Change subscription handling on UnityEventBinder.

## [2.1.0] - 2021-12-16

### Added
- Persistent Json Extension Utility for VariableCore<T> derivations.

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

[Unreleased]: https://github.com/kadinche/Kassets/compare/2.1.1...HEAD
[2.1.1]: https://github.com/kadinche/Kassets/compare/2.1.0...2.1.1
[2.1.0]: https://github.com/kadinche/Kassets/compare/2.0.0...2.1.0
[2.0.0]: https://github.com/kadinche/Kassets/compare/1.0.0...2.0.0
[1.0.1]: https://github.com/kadinche/Kassets/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/kadinche/Kassets/releases/tag/1.0.0
