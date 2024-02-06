---
sidebar_position: 1
---

# Command

Implementation of [Command pattern](https://gameprogrammingpatterns.com/command.html), utilizing `ScriptableObject` as an alternative to an `Interface`.
The `CommandCore` class itself is an abstract class, so an implementation is required.
Can be useful for one-way execution, i.e. logging.

Command has no ready-to-use base class, but you can get one for logging from the sample.
