---
slug: /
sidebar_position: 1
---

# Kassets

Kassets is an implementation of [Scriptable Object](https://docs.unity3d.com/Manual/class-ScriptableObject.html) Architecture.
Scriptable Object Architecture provides a clean and decoupled code architecture.
It is implemented based on [Ryan Hipple's talk at Unite Austin 2017](https://youtu.be/raQ3iHhE_Kk).

Though Kassets can function independently,
it was originally developed to extend the functionality of the libraries [UniRx] and [UniTask] into the Scriptable Object Architecture.
Therefore, it is recommended for enhanced functionality to use Kassets in conjunction with either or both the [UniRx] and [UniTask].
To do so, simply import any or both of these libraries along with Kassets.


Later, [R3] were released, as a replacement for [UniRx].
Being marked as public archive, [UniRx] is considered obsolete and no longer maintained.
Beginning v2.7.0, Kassets has been updated to support [R3], and most functionality of [UniRx] is being forwarded to [R3].
For v2.7.0 or newer, it is recommended to import [R3] instead of [UniRx] and [UniTask].

https://github.com/kadinche/Kassets

## License

This library is under the [MIT License](https://github.com/kadinche/Kassets/blob/main/LICENSE).

## Getting Started

- [Installation](../2-getting-started/installation.md)
- [Quick Start](../2-getting-started/quick-start.md)

[Kassets]: https://github.com/kadinche/Kassets
[R3]: https://github.com/Cysharp/R3
[UniRx]: https://github.com/neuecc/UniRx
[UniTask]: https://github.com/Cysharp/UniTask
