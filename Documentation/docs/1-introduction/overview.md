---
sidebar_position: 2
---

# Overview

Kassets is an event-based system that encourages the use of the Pub/Sub pattern.
As a fundamental principle, consider the instance of the [ScriptableObject] and its `name` property as a 'key' or 'Topic'.
You can then create a pair between the publisher and the subscriber using this 'key'.

## Core

Main features of Kassets are:

- Command
- Event
- Variable
- Collection
- Transaction

### Command

Implementation of [Command pattern](https://gameprogrammingpatterns.com/command.html), utilizing `ScriptableObject` as an alternative to an `Interface`.
The `CommandCore` class itself is an abstract class, so an implementation is required.
Can be useful for one-way execution, i.e. logging.

### Event

Event is something that happens within the program execution that requires specific response.
Implemented as `GameEvent`, each event must have at least one publisher and one subscriber.
`GameEvent` is the essence of Kassets, from which other components are derived.

### Variable

Variable is data stored in a `ScriptableObject` that can be manipulated.
`VariableCore` in Kassets is derived from `GameEvent`, which triggers a value-changed event.

### Collection

Collection is a data structure that can contain a number of items.
`CollectionCore` is derived from `VariableCore`.
`CollectionCore` is a wrapper for `List` and `Dictionary`, with additional events that are triggered when an item is added, removed, cleared, or updated.

### Transaction

Transaction is a two-way event that involves requests and responses.
Every time a request is sent, registered response event will process the request and return back to the requester.
One response event can be registered at a time.
Useful when you want to wait for event done.

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

## Utility

Kassets are provided with a few helpful tools:

### Json Extension

An Editor tools to convert Kassets' Variables into a json string or local json file.

[ScriptableObject]: https://docs.unity3d.com/Manual/class-ScriptableObject.html
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask