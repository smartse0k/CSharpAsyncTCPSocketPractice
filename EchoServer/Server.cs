using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace EchoServer
{
    internal class Server
    {
        private Socket acceptSocket;

        public Server(ushort port)
        {
            Console.WriteLine("Server 생성");

            acceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            acceptSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            acceptSocket.Listen(5);

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += OnAccept;

            StartAccept(e);
        }

        private void StartAccept(SocketAsyncEventArgs e)
        {
            Console.WriteLine("StartAccept()");

            e.AcceptSocket = null;
            acceptSocket.AcceptAsync(e);
        }

        private void OnAccept(object? sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("OnAccept()");

            if (e.SocketError != SocketError.Success)
            {
                Console.WriteLine("  OnAccept 실패! SocketError={}", e.SocketError);
                return;
            }

            if (e.AcceptSocket == null)
            {
                Console.WriteLine("  OnAccept 실패! 수락된 소켓이 없음.");
                return;
            }

            Client client = new Client(e.AcceptSocket);

            StartAccept(e);
        }
    }
}
