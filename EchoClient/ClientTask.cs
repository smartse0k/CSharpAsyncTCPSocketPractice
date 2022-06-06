using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class ClientTask
    {
        private Socket socket;
        private byte[] buffer;

        public ClientTask(string host, ushort port)
        {
            Console.WriteLine("ClientTask 생성");

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(host, port);

            buffer = new byte[1024];

            StartReceive();
        }

        private void StartReceive()
        {
            Console.WriteLine("StartReceive()");

            socket.ReceiveAsync(buffer, SocketFlags.None).ContinueWith(OnRecvComplete);
        }

        public int Send(byte[] data)
        {
            return socket.Send(data);
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
            Console.WriteLine("  수신된 정보: {0}", Encoding.Default.GetString(buffer, 0, receivedBytes));

            StartReceive();
        }
    }
}
