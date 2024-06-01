---
sidebar_position: 2
---

# GameEvent

Event is something that happens within the program execution that requires specific response.
Implemented as `GameEvent`, each event must have at least one publisher and one subscriber.
`GameEvent` is the essence of Kassets, from which other components are derived.

## Base Classes

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


### Unity Event Binder

A Component that forwards events raised by a `GameEvent` into `UnityEvent`.
Also known as `EventListener`.
