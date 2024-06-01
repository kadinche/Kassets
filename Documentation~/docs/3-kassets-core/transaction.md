---
sidebar_position: 5
---

# Transaction

Transaction is a two-way event that involves requests and responses.
Every time a request is sent, registered response event will process the request and return back to the requester.
One response event can be registered at a time.
Useful when you want to wait for event done.

__＊__ `Transaction` was renamed after `RequestResponseEvent` which is now marked as `Obsolete`

## Base Classes

Kassets provides default base classes that is usable immediately.

### Transactions

- `BoolTransaction`
- `ByteTransaction`
- `IntTransaction`
- `FloatTransaction`
- `LongTransaction`
- `DoubleTransaction`
- `StringTransaction`
- `Vector2Transaction`
- `Vector3Transaction`
- `QuaternionTransaction`
- `PoseTransaction`
