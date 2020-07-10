# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

[Unreleased]: https://github.com/kadinche/Kassets/compare/1.0.0...HEAD
[1.0.0]: https://github.com/kadinche/Kassets/releases/tag/1.0.0
