using System.Threading;

namespace Kadinche.Kassets.Utilities
{
    public static class CancellationTokenUtility
    {
        public static CancellationToken RefreshToken(ref CancellationTokenSource source)
        {
            source.CancelAndDispose();
            source = new CancellationTokenSource();
            return source.Token;
        }
    }

    public static class CancellationTokenExtension
    {
        public static void CancelAndDispose(this CancellationTokenSource source)
        {
            if (source == null) return;
            if (!source.IsCancellationRequested)
                source.Cancel();
            source.Dispose();
        }
    }
}