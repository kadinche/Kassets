---
sidebar_position: 2
---

# Quick Start

## Creating Kassets' ScriptableObjects

Instances of Kassets can be created from the `Create` context menu or from `Assets/Create` menu bar. Simply right click in the Project window and select the type of instance you want to create.

### Create a `GameEvent` Instance

For `GameEvent` instances, select any of the provided event types from `Create/Kassets/Game Events/`

![CreateEvent](https://user-images.githubusercontent.com/1290720/137657526-5bbad8d6-a6c5-4361-a272-0ef30e684fad.gif)

### Create a `Variable` Instance

For `Variable` instances, select any of the provided variable types from `Create/Kassets/Variables/`

![CreateVariable](https://user-images.githubusercontent.com/1290720/137657744-cc7ec167-d728-4a8a-8a6f-06b67bf01b14.gif)

Below are the preview of Kassets' `Variable` instance on Inspector window.

<img width="551" alt="Screenshot 2023-06-12 at 16 34 50" src="https://github.com/kadinche/Kassets/assets/1290720/3d6d1a6d-6b95-4395-8b94-a9426185ae14"></img>

## Using Kassets' ScriptableObject Instances

### Usage on MonoBehavior Script

Create a `MonoBehavior` script and add Kassets' instance as a serialized field.

`Player.cs` :

<img width="431" alt="Screen Shot 2020-11-16 at 20 47 36 copy" src="https://user-images.githubusercontent.com/1290720/99601499-dda94c00-2a42-11eb-8c4c-5378a38f5602.png"></img>

`HealthBarUI.cs` :

<img width="396" alt="Screen Shot 2020-11-16 at 21 06 10 copy" src="https://user-images.githubusercontent.com/1290720/99601502-df730f80-2a42-11eb-9c1f-0fa1bfad086a.png"></img>

Drag and drop `PlayerHealth` (`FloatVariable`) to `Player`'s `Health` field :

<img width="877" alt="Screen Shot 2020-11-19 at 6 25 46" src="https://user-images.githubusercontent.com/1290720/99601580-0a5d6380-2a43-11eb-9d22-7428f7dae993.png"></img>

Drag and drop `PlayerHealth` (`FloatVariable`) to `HealthBarUI`'s `Health` field :

<img width="878" alt="Screen Shot 2020-11-19 at 6 26 08" src="https://user-images.githubusercontent.com/1290720/99601584-0d585400-2a43-11eb-9ce3-dd143e260f0d.png"></img>

From the example above, `Player` component's field `Health` and `HealthBarUI` component's field `Health` both refer to the same `FloatVariable` `ScriptableObject` instance `PlayerHealth`.
Since both components refer to the same variable instance, the `float` value they refer to is shared.
Each component then manages its own need with the value without any coupling between components.
For instance, `HealthBarUI` doesn't need to request the `Health` value from the `Player` component and the `Player` component can manage its own `Health` value without needing to distribute it to other components.

### Usage on UnityEvents

Kassets’s instance is a ScriptableObject asset. It can be referenced to UnityEvent via Inspector. You can aslo use dynamic method call on UnityEvent to pass a parameter.

![UnityEvent](https://user-images.githubusercontent.com/1290720/138039011-f32deac1-de5c-48ea-afaa-bf0f815b448d.gif)

![UnityEventDynamic](https://user-images.githubusercontent.com/1290720/138039041-0b42cbe8-254f-40ef-a2df-a45a19883c98.gif)
