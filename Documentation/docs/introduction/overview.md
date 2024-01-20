---
sidebar_position: 2
---

# Overview

Kassets is an event-based system that encourages Pub/Sub pattern.
As a basic principle, you can think of the ScriptableObject's Instance and its name property as a "key" or "Topic".
You can then pair between publisher and subscriber using these "key".

## Core

Main features of Kassets are:

- Command
- Event
- Variable
- Collection
- Transaction

### Command

Implementation of [Command] pattern, utilizing ScriptableObject as an alternative of an Interface.
The CommandCore class itself is an abstract class, so an Implementation is required.
Can be useful for one-way execution, i.e. logging.

### Event

Event is something that happens within the program execution that requires specific response.
Implemented as `GameEvent`, each event must have at least one publisher and one subscriber.
`GameEvent` is the essence of Kassets, which other components is derived from.

### Variable



## Utility

[Command]: https://gameprogrammingpatterns.com/command.html
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask