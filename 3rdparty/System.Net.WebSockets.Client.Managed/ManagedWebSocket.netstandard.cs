using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets.Managed
{
    internal sealed partial class ManagedWebSocket : WebSocket
    {
        private Task ValidateAndReceiveAsync(Task receiveTask, byte[] buffer, CancellationToken cancellationToken)
        {
            if (receiveTask == null ||
                (receiveTask.Status == TaskStatus.RanToCompletion &&
                !(receiveTask is Task<WebSocketReceiveResult> wsrr && wsrr.Result.MessageType == WebSocketMessageType.Close)))
            {
                receiveTask = ReceiveAsyncPrivate<WebSocketReceiveResultGetter, WebSocketReceiveResult>(new ArraySegment<byte>(buffer), cancellationToken).AsTask();
            }

            return receiveTask;
        }
    }
}
