using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    internal class Client
    {
        private Socket socket;

        public Client(Socket socket)
        {
            Console.WriteLine("Client 생성");

            this.socket = socket;

            SocketAsyncEventArgs recvEvent = new SocketAsyncEventArgs();
            recvEvent.Completed += OnRecvComplete;
            recvEvent.SetBuffer(new byte[1024], 0, 1024);

            StartReceive(recvEvent);
        }

        private void StartReceive(SocketAsyncEventArgs e)
        {
            Console.WriteLine("StartReceive()");

            socket.ReceiveAsync(e);
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

            byte[] data = e.Buffer;

            Console.WriteLine("  수신된 크기: {0} 바이트", e.BytesTransferred.ToString());

            socket.Send(data, 0, e.BytesTransferred, SocketFlags.None);
            StartReceive(e);
        }
    }
}
