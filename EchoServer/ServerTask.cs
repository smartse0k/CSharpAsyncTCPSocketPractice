using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace EchoServer
{
    internal class ServerTask
    {
        private Socket acceptSocket;

        public ServerTask(ushort port)
        {
            Console.WriteLine("ServerTask 생성");

            acceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            acceptSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            acceptSocket.Listen(5);

            StartAccept();
        }

        private void StartAccept()
        {
            Console.WriteLine("StartAccept()");
            acceptSocket.AcceptAsync().ContinueWith(OnAccept);
        }

        private void OnAccept(Task<Socket> task)
        {
            Console.WriteLine("OnAccept()");

            Socket socket = task.Result;

            ClientTask client = new ClientTask(socket);

            StartAccept();
        }
    }
}
