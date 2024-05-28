---
sidebar_position: 3
---

# Designing Reusable Modules

Modules are a part of a program, that designed to be handle only one task.
Think of it as a way to conform the Single Responsibility Principle, in which a module has one responsibility.
By designing and creating a module properly, you can reuse modules in different projects, not limited to just one.

Using Kassets, you can design reusable modules quite easily.
The three key points for creating modules are Input, Process, Output.

### Input

Use Kassets instance as an input to be processed.
You can easily replace from which instance the input come from, which can be very useful when doing an isolated testing.

### Process

This is the core of your module, where the process of the input data happened.
For simplicity, this would be the MonoBehavior components that has Kassets instance as an input, and optionally an output.

### Output

Use Kassets instance as the output of the module.
Output are optional, because not every module has them.
Output should belong to the module, whereas input can be anywhere and replacable.

```cs
public class SampleModule : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private IntGameEvent _inputValue;

    [Header("Output")]
    [SerializeField] private FloatVariable _outputValue;
    
    [Header("Local Fields")]
    [SerializeField] private TMP_Text _displayText;
    
    private IDisposable _subscription;

    private void Start()
    {
        // Module starts with a subscription to the Input 
        _subscription = _inputValue.Subscribe(ProcessInput);
    }

    private void ProcessInput(int value)
    {
        // processes the Input value according to each module's requirements.
        var processedValue = value * 0.5f;
        
        // optionally handle the processed value within internal components.
        _displayText.text = value.ToString();
        
        // send the processed value as an Output that can be accessed from outside the module.
        _outputValue.Value = processedValue;
    }

    private void OnDestroy()
    {
        _subscription.Dispose();
    }
}
```

There's no limitation on the size of the module.
You can always put several Inputs and/or Outputs, and other internal MonoBehavior components.
Freely adjust according to the needs of the module.
