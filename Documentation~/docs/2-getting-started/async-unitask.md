---
sidebar_position: 4
---

# Asynchronous with UniTask

If you had UniTask imported, you can use Asynchronous on Kassets' instances. First, make sure to import UniTask to your project. Upon import, Kassets will adjust internally to support UniTask using scripting define `KASSETS_UNITASK`. It would normally be defined when UniTask is imported using package manager. If somehow `KASSETS_UNITASK` is undefined, add it to `Scripting Define Symbols` on Project Settings.

To use Kassets Asynchronously, use the method `EventAsync()` and add `await` in front of it. Any Kassets' instances that derived from GameEvent can be used asynchronously. (For Command, use method `ExecuteAsync()`)

```csharp
public class CounterAttackSkill: MonoBehaviour
{
    [SerializeField] private GameEvent counterActivateEvent;
    [SerializeField] private FloatGameEvent attackGameEvent;
    [SerializeField] private FloatVariable health;
    
    private IDisposable subscription;
    
    private void Start()
    {
        // When using subscribe await, next event raise will wait for current activated counter to end.
        subscription = counterActivateEvent.SubscribeAwait(async _ => await OnCounterActivate());
    }
    
    // Activate counter.
    private async UniTask OnCounterActivate()
    {
        var currentHealth = health.Value;
        
        // asynchronously wait until damaged, which indicated by health value changed event.
        var afterDamaged = await health.EventAsync(cancellationToken);
        
        var damage = currentHealth - afterDamaged;
        
        // raise attack event with damage value of damage received.
        attackGameEvent.Raise(damage):
    }
    
    private void OnDestroy()
    {
        subscription?.Dispose();
    }
}
```

In the example above, an asynchronous operation `EventAsync()` on variable means to wait for its value to change. GameEvent in general, will wait for an event to fire.

According to this [slide (Japanese)](https://speakerdeck.com/torisoup/unitask2020?slide=52), It is a best practice to always use cancellation token on every UniTask's asynchronous operation. Since Unity is not asynchronous, any asynchronous operation can be left behind waiting infinitely when the process is not stopped.
