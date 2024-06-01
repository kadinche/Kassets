---
sidebar_position: 2
---

# Overview

Kassets is an event-based system that encourages the use of the Pub/Sub pattern.
As a fundamental principle, consider the instance of the [ScriptableObject] and its `name` property as a 'key' or 'Topic'.
You can then create a pair between the publisher and the subscriber using this 'key'. Here's an overview of Kassets features.

### [Command](../3-kassets-core/command.md)

Implementation of [Command pattern](https://gameprogrammingpatterns.com/command.html), utilizing `ScriptableObject` as an alternative to an `Interface`.
The `CommandCore` class itself is an abstract class, so an implementation is required.
Can be useful for one-way execution, i.e. logging.

### [Event](../3-kassets-core/gameevent.md)

Event is something that happens within the program execution that requires specific response.
Implemented as `GameEvent`, each event must have at least one publisher and one subscriber.
`GameEvent` is the essence of Kassets, from which other components are derived.

### [Variable](../3-kassets-core/variable.md)

Variable is data stored in a `ScriptableObject` that can be manipulated.
`VariableCore` in Kassets is derived from `GameEvent`, which triggers a value-changed event.

### [Collection](../3-kassets-core/collection.md)

Collection is a data structure that can contain a number of items.
`CollectionCore` is derived from `VariableCore`.
`CollectionCore` is a wrapper for `List` and `Dictionary`, with additional events that are triggered when an item is added, removed, cleared, or updated.

### [Transaction](../3-kassets-core/transaction.md)

Transaction is a two-way event that involves requests and responses.
Every time a request is sent, registered response event will process the request and return back to the requester.
One response event can be registered at a time.
Useful when you want to wait for event done.

### [Base Classes](../4-fundamentals/base-classes.md)

Kassets provides default base classes that is usable immediately. You can access them from from the `Create/Kassets/` context menu or from `Assets/Create/Kassets/` menu bar.
Note that Base Classes use a different `.asmdef`. If you manage your own `.asmdef` references, you may need to add a reference to `Kassets.Base` in your project.

### [Unity Event Binder](../3-kassets-core/gameevent.md#unity-event-binder)

A Component that forwards events raised by a `GameEvent` into `UnityEvent`.
Also known as `EventListener` in Scriptable Object Architecture's terms.

### [Json Extension](../5-utilities/json-extension.md)

An Editor tools to convert Kassets' Variables into a json string or local json file.
You can access them from Kassets variable's inspector window.

[ScriptableObject]: https://docs.unity3d.com/Manual/class-ScriptableObject.html
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask