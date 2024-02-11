---
sidebar_position: 1
---

# Command

The `CommandCore` class in Kassets is an implementation of the [Command pattern](https://gameprogrammingpatterns.com/command.html),
using `ScriptableObject` as an alternative to an `Interface`.
The base class `CommandCore` is abstract, meaning that you'll need to provide a concrete implementation for it to function.
This pattern can be particularly effective in scenarios where you only need actions to be executed in a singular direction,
such as logging events or commands, where there is no need for a response.

There isn't a predefined base class for `Command`, but you can access a sample one for logging.
