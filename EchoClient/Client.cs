using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class Client
    {
        private Socket socket;

        public Client(string host, ushort port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(host, port);

            SocketAsyncEventArgs recvEvent = new SocketAsyncEventArgs();
            recvEvent.SetBuffer(new byte[1024], 0, 1024);
            recvEvent.Completed += OnRecvComplete;
            StartReceive(recvEvent);
        }

        private void StartReceive(SocketAsyncEventArgs e)
        {
            Console.WriteLine("StartReceive()");

            socket.ReceiveAsync(e);
        }

        public int Send(byte[] data)
        {
            return socket.Send(data);
        }

        private void OnRecvComplete(object? sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("OnRecvComplete()");

            if (e.BytesTransferred == 0)
            {
                Console.WriteLine("  연결이 끊어짐!");
                return;
            }

            if (e.Buffer == null)
            {
                Console.WriteLine("  OnRecvComplete() 실패! 버퍼가 존재하지 않음.");
                return;
            }

            Console.WriteLine("  수신된 크기: {0} 바이트", e.BytesTransferred.ToString());
            Console.WriteLine("  수신된 정보: {0}", Encoding.Default.GetString(e.Buffer, 0, e.BytesTransferred));

            StartReceive(e);
        }
    }
}
