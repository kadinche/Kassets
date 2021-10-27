Kassets GameEvent Samples.

In this sample, the usage of GameEvent is demonstrated.

GameEventBasics.scene

1. The Button in this scene had referenece to OnButtonClicked.asset (A ScriptableObject instance of GameEvent).
2. Raise is fired from the Button's OnClick event. This can be done directly from Unity's Editor without creating any script.
3. A Subscriber will listen to the event Raise. In this sample it will display a text (see EventSubscriber.cs).