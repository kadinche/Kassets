---
sidebar_position: 3
---

# Reactive with R3

If you had R3 imported, you can use Reactive on Kassets' instances. First, make sure to import R3 to your project. Upon import, Kassets will adjust internally to support R3 using scripting define `KASSETS_R3`. It would normally be defined when R3 is imported using package manager. If somehow `KASSETS_R3` is undefined, add it to `Scripting Define Symbols` on Project Settings.

To use Kassets reactively, simply `Subscribe` to a GameEvent instances or its derivation.

```csharp
public class HealthBarUI : MonoBehavior
{
    [SerializeField] private FloatVariable health;
    [SerializeField] private FloatVariable maxHealth;
    [SerializeField] private Image healthBar;
    
    private IDisposable subscription;

    private void Start()
    {
        subscription = health.Subscribe(UpdateHealthBar);
    }

    private void UpdateHealthBar(float currentHealth)
    {
        var rt = healthBar.rectTransform;
        
        var sizeDelta = rt.sizeDelta;
        sizeDelta.x = 2 * (currentHealth / maxHealth);
        
        rt.sizeDelta = sizeDelta;
    }    
    
    private void OnDestroy()
    {
        subscription?.Dispose();
    }
}
```

> [!NOTE]
> Being marked as public archive, [UniRx] is now considered obsolete.
> If [UniRx] are imported together with [R3], All functionality of [UniRx] is being forwarded to [R3].
> Use `AsSystemObservable()` to convert to `IObservable` and continue using [UniRx].
