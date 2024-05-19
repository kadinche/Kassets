---
sidebar_position: 1
---

# Editor Extensions

Kassets provides a set of editor extensions to help you work with Kassets more efficiently.
These extensions are designed to help you create, manage, and debug Kassets instances.

## Create Kassets Instances from Inspector

If you have a `ScriptableObject` field in your `MonoBehaviour` script,
you can create a new instance by clicking the `Create` button in the inspector.
The new instance will be automatically assigned to the field.
 `Create` button will appear when the field is empty.

![Screenshot 2024-05-20 at 4 08 51](https://github.com/kadinche/Kassets/assets/1290720/99812122-b7c7-42ff-a2e2-8985ff90b77e)

## Raise Events from Inspector

Instances of `GameEvent` and its derived classes can have their events raised directly from the inspector.
This can be useful for debugging or testing purposes.
Simply click the `Raise` button in the inspector to raise the event.
The `Raise` button is enabled on Play mode.

![Screenshot 2024-05-20 at 4 56 16](https://github.com/kadinche/Kassets/assets/1290720/457d92df-5057-4e90-a9db-9adfef04c740)

## Customize Instance's Behavior

Each instance of `Kassets` can have its behavior customized from the inspector.
Customizable behaviors vary depending on the instance type.

![Screenshot 2024-05-20 at 4 57 46](https://github.com/kadinche/Kassets/assets/1290720/f7a3d5c4-123e-4c04-ab53-c51baf168241)

__GameEvent__

- `Subscribe Behavior` : Select how the event should behave when it is raised, whether `Pull` or `Push`.
  - Note : This setting is only available when both UniRx and UniTask are imported.

__Variable__

- `Variable Event Type` : Select the event type that will be raised on variable events.
  - `ValueAssign` : Raise an event when the variable value is assigned, regardless of whether the value changes.
  - `ValueChanged` : Raise an event only when the variable value changes.
- `Auto Reset Value` : Reset the variable value to its initial value when Play Mode is stopped.
  Initial Value is defined as the value set before entering Play Mode.

:::note
By Unity's Scriptable Object default behavior, any changes made to the Scriptable Object instance in Play Mode will be saved.
`Auto Reset Value` allows you to reset the value to the initial value when Play Mode is stopped.
:::

