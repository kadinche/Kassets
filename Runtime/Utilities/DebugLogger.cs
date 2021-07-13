using System;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityDebug = UnityEngine.Debug;

// see also
// - https://qiita.com/toRisouP/items/d856d65dcc44916c487d
// - https://baba-s.hatenablog.com/entry/2019/09/02/080000

public static class Debug
{
    /// <summary>
    /// Development Buildを無効化してビルドした時はシーンロード前にログ出力を無効化する
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LogDisabler()
    {
        UnityDebug.unityLogger.logEnabled = UnityDebug.isDebugBuild;
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, string message, Object context)
    {
        UnityDebug.Assert(condition, message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, object message, Object context)
    {
        UnityDebug.Assert(condition, message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, string message)
    {
        UnityDebug.Assert(condition, message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, object message)
    {
        UnityDebug.Assert(condition, message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, Object context)
    {
        UnityDebug.Assert(condition, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition)
    {
        UnityDebug.Assert(condition);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void AssertFormat(bool condition, string format, params object[] args)
    {
        UnityDebug.AssertFormat(condition, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void AssertFormat(bool condition, Object context, string format, params object[] args)
    {
        UnityDebug.AssertFormat(condition, context, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Break()
    {
        UnityDebug.Break();
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void ClearDeveloperConsole()
    {
        UnityDebug.ClearDeveloperConsole();
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DebugBreak()
    {
        UnityDebug.DebugBreak();
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        UnityDebug.DrawLine(start, end, color, duration);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        UnityDebug.DrawLine(start, end, color);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest)
    {
        UnityDebug.DrawLine(start, end, color, duration, depthTest);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end)
    {
        UnityDebug.DrawLine(start, end);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
    {
        UnityDebug.DrawRay(start, dir, color, duration);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest)
    {
        UnityDebug.DrawRay(start, dir, color, duration, depthTest);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir)
    {
        UnityDebug.DrawRay(start, dir);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color)
    {
        UnityDebug.DrawRay(start, dir, color);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message, Object context)
    {
        UnityDebug.Log(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message)
    {
        UnityDebug.Log(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertion(object message, Object context)
    {
        UnityDebug.LogAssertion(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertion(object message)
    {
        UnityDebug.LogAssertion(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertionFormat(Object context, string format, params object[] args)
    {
        UnityDebug.LogAssertionFormat(context, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertionFormat(string format, params object[] args)
    {
        UnityDebug.LogAssertionFormat(format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogError(object message, Object context)
    {
        UnityDebug.LogError(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogError(object message)
    {
        UnityDebug.LogError(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogErrorFormat(string format, params object[] args)
    {
        UnityDebug.LogErrorFormat(format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogErrorFormat(Object context, string format, params object[] args)
    {
        UnityDebug.LogErrorFormat(context, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception, Object context)
    {
        UnityDebug.LogException(exception, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogException(Exception exception)
    {
        UnityDebug.LogException(exception);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(Object context, string format, params object[] args)
    {
        UnityDebug.LogFormat(context, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format,
        params object[] args)
    {
        UnityDebug.LogFormat(logType, logOptions, context, format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(string format, params object[] args)
    {
        UnityDebug.LogFormat(format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message)
    {
        UnityDebug.LogWarning(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message, Object context)
    {
        UnityDebug.LogWarning(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarningFormat(string format, params object[] args)
    {
        UnityDebug.LogWarningFormat(format, args);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarningFormat(Object context, string format, params object[] args)
    {
        UnityDebug.LogWarningFormat(context, format, args);
    }
}

namespace Kadinche.Kassets.Utilities
{
    public static class DebugLogExtension
    {
        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        private static void ColoredLog(this object t, string color, string message, bool logValue)
        {
            var componentName = t is Component component ? $" ({component.name})" : "";
            var value = logValue ? $" Value: {t}." : "";
            var context = t is Object obj ? obj : null;
            Debug.Log($"<color={color}>[{t.GetType().Name}{componentName}]{value} {message}</color>", context);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void White(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "white", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Black(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "black", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Red(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "red", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Green(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "green", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Cyan(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "cyan", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Yellow(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "yellow", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Orange(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "orange", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Magenta(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "magenta", message, logValue);
        }

        [Conditional("DEVELOPMENT_BUILD")]
        [Conditional("UNITY_EDITOR")]
        public static void Blue(this object t, string message = "", bool logValue = false)
        {
            ColoredLog(t, "blue", message, logValue);
        }
    }
}