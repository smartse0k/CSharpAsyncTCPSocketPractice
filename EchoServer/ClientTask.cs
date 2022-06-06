using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    internal class ClientTask
    {
        private Socket socket;
        private byte[] buffer;

        public ClientTask(Socket socket)
        {
            Console.WriteLine("ClientTask 생성");
            
            this.socket = socket;
            buffer = new byte[1024];

            StartReceive();
        }

        private void StartReceive()
        {
            Console.WriteLine("StartReceive()");

            socket.ReceiveAsync(buffer, SocketFlags.None).ContinueWith(OnRecvComplete);
        }

        private void OnRecvComplete(Task<int> task)
        {
            Console.WriteLine("OnRecvComplete()");

            int receivedBytes = task.Result;

            if (receivedBytes == 0)
            {
                Console.WriteLine("  연결이 끊어짐!");
                return;
            }

            Console.WriteLine("  수신된 크기: {0} 바이트", receivedBytes.ToString());

            socket.Send(buffer, 0, receivedBytes, SocketFlags.None);
            StartReceive();
        }
    }
}
