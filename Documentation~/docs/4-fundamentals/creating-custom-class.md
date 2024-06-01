---
sidebar_position: 4
---

# Creating Custom Class

Create Custom Class derived from Kassets Core classes are pretty straight forward.
Simply inherit the necessary class and optionally add your class to the context menu.
Even if not added to context menu, you can create your custom instance from the inspector.

```cs
namespace Custom.Variable
{
    [CreateAssetMenu(fileName = "HealthVariable", menuName = MenuHelper.DefaultVariableMenu + "HealthVariable", order = 100)]
    public class HealthVariable : VariableCore<Health>
    {
    }

    public struct Health
    {
        public float current;
        public float max;
    }
}
```

Creating custom class is encouraged as you can setup the namespace in accordance to your projects.
This is beneficial if you try to track references, so that you don't ended up in a general Kassets' namespace.
