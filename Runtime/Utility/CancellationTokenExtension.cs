using System.Threading;

namespace Kadinche.Kassets
{
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