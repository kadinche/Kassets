---
sidebar_position: 1
---

# UniRx Integration

If you had UniRx imported, you can use Reactive on Kassets' instances. First, make sure to import UniRx to your project. Upon import, Kassets will adjust internally to support UniRx using scripting define `KASSETS_UNIRX`. It would normally be defined when UniRx is imported using package manager. If somehow `KASSETS_UNIRX` is undefined, add it to `Scripting Define Symbols` on Project Settings.

When importing UniRx, Kassets' GameEvent becomes `Observable`. To use Kassets reactively, simply `Subscribe` to a GameEvent instances or its derivation.

![Screen Shot 2021-10-18 at 11 19 20](https://user-images.githubusercontent.com/1290720/137659609-61510ee1-44b3-4286-bc2a-993a6e369b59.png)
