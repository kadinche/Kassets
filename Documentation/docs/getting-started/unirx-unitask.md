---
sidebar_position: 5
---

# Pull vs Push Subscription

UniTask v2 has support for Asynchronous LINQ.
Asynchronous LINQ is an extension to `IUniTaskAsyncEnumerable<T>` and its usage can be very similar to UniRx,
but the process behind it is different (UniRx is push-based while UniTask is pull-based).

Kassets' `ScriptableObject` also make use of Asynchronous LINQ.
Kassets' `ScriptableObject` derived from `IUniTaskAsyncEnumerable<T>` so it is possible to directly apply
various features of UniTask as explained in its [github page](https://github.com/Cysharp/UniTask#asyncenumerable-and-async-linq) or from this [slide](https://speakerdeck.com/torisoup/unitask2020?slide=110) (Japanese).

When using pull-based subscriptions of UniTask.Linq, when the process of handling an event is still running,
it won't be called again until it is over, no matter how many times the event has been raised during the process.
Reversely, push-based subscriptions of UniRx will execute every event raise.

When both UniRx and UniTask are imported together, It can be confusing which of the `Subscription` behavior is in effect (pull-based or push-based?).
To use Kassets' instance as `IObservable`, use `AsObservable()`.
To use Kassets' instance as `IUniTaskAsyncEnumerable` use `AsAsyncEnumerable()`.
Unless referenced by interface, Kassets instances Default Subscribe Behavior can be selected from the inspector window.

Note that UniTask Asynchronous LINQ is part of `Cysharp.Threading.Tasks.Linq` namespace.
To use, add `UniTask.Linq` as reference to your project's Assembly Definition.

<img width="514" alt="Screenshot 2023-06-12 at 16 49 31" src="https://github.com/kadinche/Kassets/assets/1290720/dea3da9d-cc3e-45a2-82a5-e590cfca84ee">
