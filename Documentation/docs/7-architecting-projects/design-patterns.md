---
sidebar_position: 2
---

# Design Patterns

There are several design patterns that fits well with Scriptable Object Architecture, such as Observer, Command, and some architectural patterns like MVC, MVP, and MVVM.
The definition and implementation of design patterns varies for each person, so you can adjust the implementation to fit your needs.
You can always use any other design patterns that you see fits.

## Observer Pattern

Observer pattern is a behavioral design pattern that defines a one-to-many dependency between objects so that when one object changes state,
all its dependents are notified and updated automatically.
This fits very well with Kassets' `GameEvent` class.
Given a `GameEvent` instance, you can subscribe to it and raise the event from anywhere in the project.
For more details, see the provided Sample Project.

```csharp
public class GameEventSubscriber : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    private void Start()
    {
        gameEvent.Subscribe(OnEventRaised);
    }
    
    private void OnEventRaised()
    {
        // Do something
    }
}
```

## [Command Pattern]

[Command pattern] is a behavioral design pattern that turns a request into a stand-alone object that contains all information about the request.
Kassets provides an abstract class `Command` that you can derive from to create your own command.
However, the `Command` class implemented in Kassets are designed for stand-alone, so it cannot be forwarded to another objects internally.
The best practice on using `Command` is to execute a simple task or send data online without any response.
For example, you can create a `LogCommand` that logs a message to the console, or you can create `ShareCommand` that shares a message to social media.
For more details, see the provided Sample Projects.

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

[Command pattern] can also utilize Kassets' `GameEvent` class to notify target objects when the command is executed.
However, this will looks very similar to Observer pattern, and it is recommended to use Observer pattern instead.

## Architectural Patterns

### [Model-View-Controller (MVC)]

[Model-View-Controller (MVC)] is a design pattern that separates the application into three main components: Model, View, and Controller.

### [Model-View-Presenter (MVP)]

[Model-View-Presenter (MVP)] is a design pattern that separates the application into three main components: Model, View, and Presenter.
You can utilize Kassets' `Variable` and `GameEvent` instances as the Model, then create a `MonoBehaviour` as the Presenter,
and Unity's Components or UI elements, such as Button, Text, or Image, as the View.

### [Model-View-ViewModel (MVVM)]

[Model-View-ViewModel (MVVM)] is a design pattern that separates the application into three main components: Model, View, and ViewModel.


[Command pattern]: https://gameprogrammingpatterns.com/command.html
[Model-View-Controller (MVC)]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller
[Model-View-Presenter (MVP)]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93presenter
[Model-View-ViewModel (MVVM)]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
