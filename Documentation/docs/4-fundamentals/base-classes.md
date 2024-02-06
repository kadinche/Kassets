---
sidebar_position: 3
---

# Base Classes

Kassets provides default base classes that is usable immediately.

### GameEvents

- `GameEvent`: Default Type-less GameEvent.
- `BoolGameEvent`
- `ByteGameEvent`
- `ByteArrayGameEvent`
- `IntGameEvent`
- `FloatGameEvent`
- `LongGameEvent`
- `DoubleGameEvent`
- `StringGameEvent`
- `Vector2GameEvent`
- `Vector3GameEvent`
- `QuaternionGameEvent`
- `PoseGameEvent`
- `Texture2DGameEvent`
- `GameObjectGameEvent`

### Variables

- `BoolVariable`
- `ByteVariable`
- `IntVariable`
- `FloatVariable`
- `LongVariable`
- `DoubleVariable`
- `StringVariable`
- `Vector2Variable`
- `Vector3Variable`
- `QuaternionVariable`
- `PoseVariable`
- `Texture2DVariable`
- `GameObjectVariable`

### Collections

- `BoolCollection`
- `ByteCollection`
- `IntCollection`
- `FloatCollection`
- `LongCollection`
- `DoubleCollection`
- `StringCollection`
- `Vector2Collection`
- `Vector3Collection`
- `QuaternionCollection`
- `PoseCollection`
- `TransformCollection`

### Transactions

- `BoolTransaction`
- `ByteTransaction`
- `IntTransaction`
- `FloatTransaction`
- `LongTransaction`
- `DoubleTransaction`
- `StringTransaction`
- `Vector2Transaction`
- `Vector3Transaction`
- `QuaternionTransaction`
- `PoseTransaction`

### Unity Event Binder

A Component that forwards events raised by a `GameEvent` into `UnityEvent`.
Also known as `EventListener`.

- `UnityEventBinder`: Default Type-less event binder.
- `UnityEventBatchBinder`: Accepts multiple GameEvents as a trigger.
- `BoolUnityEventBinder`
- `ByteUnityEventBinder`
- `IntUnityEventBinder`
- `FloatUnityEventBinder`
- `LongUnityEventBinder`
- `DoubleUnityEventBinder`
- `StringUnityEventBinder`
- `Vector2UnityEventBinder`
- `Vector3UnityEventBinder`
- `QuaternionUnityEventBinder`
- `PoseUnityEventBinder`
