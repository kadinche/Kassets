---
sidebar_position: 1
---

# Command

The `CommandCore` class in Kassets is an implementation of the [Command pattern](https://gameprogrammingpatterns.com/command.html),
using `ScriptableObject` as an alternative to an `Interface`.
The base class `CommandCore` is abstract, meaning that you'll need to provide a concrete implementation for it to function.
This pattern can be particularly effective in scenarios where you only need actions to be executed in a singular direction,
such as logging events or commands, where there is no need for a response. There isn't a predefined base class for `Command`,
but you can access the Sample.

Below are some examples of how you can use the `Command` pattern in your project.

```csharp
[CreateAssetMenu(fileName = "LogCommand", menuName = MenuHelper.DefaultCommandMenu + "LogCommand")]
public class LogCommand : CommandCore
{
    [SerializeField] private string message;

    public override void Execute()
    {
        Debug.Log(message);
    }
}
```

```csharp
[CreateAssetMenu(fileName = "CustomLogCommand", menuName = MenuHelper.DefaultCommandMenu + "CustomLogCommand")]
public class CustomLogCommand : CommandCore<string>
{
    public override void Execute(string message)
    {
        Debug.Log(message);
    }
}
```
