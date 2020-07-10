using Kassets.EventSystem;
using UnityEngine;

namespace Kassets.Components
{
    public class GameEventForwarder : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEventToSubscribe;
        [SerializeField] private GameEvent _gameEventToRaise;

        private void Start() => Bind();

        private void Bind()
        {
            if (_gameEventToSubscribe is BoolEvent boolEvent)
                boolEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is ByteArrayEvent byteArrayEvent)
                byteArrayEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is ByteEvent byteEvent)
                byteEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is DoubleEvent doubleEvent)
                doubleEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is FloatEvent floatEvent)
                floatEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is GameObjectEvent gameObjectEvent)
                gameObjectEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is IntEvent intEvent)
                intEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is LongEvent longEvent)
                longEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is PoseEvent poseEvent)
                poseEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is QuaternionEvent quaternionEvent)
                quaternionEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is StringEvent stringEvent)
                stringEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is Texture2DEvent texture2DEvent)
                texture2DEvent.Subscribe(Raise);
            else if (_gameEventToSubscribe is Vector2Event vector2Event)
                vector2Event.Subscribe(Raise);
            else if (_gameEventToSubscribe is Vector3Event vector3Event)
                vector3Event.Subscribe(Raise);
            else
                _gameEventToSubscribe.Subscribe(_gameEventToRaise.Raise);
        }

        private void Raise<T>(T value)
        {
            if (_gameEventToRaise is BoolEvent boolEvent && value is bool boolValue)
                boolEvent.Raise(boolValue);
            else if (_gameEventToSubscribe is ByteArrayEvent byteArrayEvent && value is byte[] bytesValue)
                byteArrayEvent.Raise(bytesValue);
            else if (_gameEventToSubscribe is ByteEvent byteEvent && value is byte byteValue)
                byteEvent.Raise(byteValue);
            else if (_gameEventToSubscribe is DoubleEvent doubleEvent && value is double doubleValue)
                doubleEvent.Raise(doubleValue);
            else if (_gameEventToSubscribe is FloatEvent floatEvent && value is float floatValue)
                floatEvent.Raise(floatValue);
            else if (_gameEventToSubscribe is GameObjectEvent gameObjectEvent && value is GameObject gameObjectValue)
                gameObjectEvent.Raise(gameObjectValue);
            else if (_gameEventToSubscribe is IntEvent intEvent && value is int intValue)
                intEvent.Raise(intValue);
            else if (_gameEventToSubscribe is LongEvent longEvent && value is long longValue)
                longEvent.Raise(longValue);
            else if (_gameEventToSubscribe is PoseEvent poseEvent && value is Pose poseValue)
                poseEvent.Raise(poseValue);
            else if (_gameEventToSubscribe is QuaternionEvent quaternionEvent && value is Quaternion quaternionValue)
                quaternionEvent.Raise(quaternionValue);
            else if (_gameEventToSubscribe is StringEvent stringEvent && value is string stringValue)
                stringEvent.Raise(stringValue);
            else if (_gameEventToSubscribe is Texture2DEvent texture2DEvent && value is Texture2D texture2DValue)
                texture2DEvent.Raise(texture2DValue);
            else if (_gameEventToSubscribe is Vector2Event vector2Event && value is Vector2 vector2Value)
                vector2Event.Raise(vector2Value);
            else if (_gameEventToSubscribe is Vector3Event vector3Event && value is Vector3 vector3Value)
                vector3Event.Raise(vector3Value);
            else
                _gameEventToRaise.Raise();
        }
    }
}