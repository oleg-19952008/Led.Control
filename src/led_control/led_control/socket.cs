//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//namespace BlackSPACE.socket
//{
//    public class socket
//    {
//        public byte[] buffer = new byte[1024];
//        public static Socket socket_ = null;
//        public socket(Socket sock)
//        {
//            socket_ = sock;
//        }
//        public static ManualResetEvent alldone,connectdone = new ManualResetEvent(false);
//        public static void start(/*int port*/)
//        {
//            //var s = new Socket(socket_);
//            int port = 2511;
//            var LEP = new IPEndPoint(IPAddress.Parse("1.1.1.2"), port);
//            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            try
//            {
//                //listener.Bind(LEP);
//                //listener.Listen(100);
//                while (true)
//                {
//                ///    alldone.Reset();
//                    listener.BeginConnect(LEP,new AsyncCallback(socket.ConnectCallback), listener);
//                    alldone.WaitOne();
//                    listener.Send(Encoding.UTF8.GetBytes("asd"));
//                }
//            }
//            catch
//            {

//            }

//        }
//        private static void ConnectCallback(IAsyncResult ar)
//        {
//            try
//            {
//                // Retrieve the socket from the state object.
//                Socket client = (Socket)ar.AsyncState;

//                // Complete the connection.
//                client.EndConnect(ar);

//                Console.WriteLine("Socket connected to {0}",
//                    client.RemoteEndPoint.ToString());

//                // Signal that the connection has been made.
//                connectdone.Set();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//        public static void Accept_call_back(IAsyncResult ar)
//        {
//            alldone.Set();
//            var listener = (Socket)ar.AsyncState;
//            var handler = listener.EndAccept(ar);
//            new BlackSPACE.Server.user(handler);
//        }
//    }
//}
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

// State object for receiving data from remote device.
//public class StateObject
//{
//    // Client socket.
//    public Socket workSocket = null;
//    // Size of receive buffer.
//    public const int BufferSize = 256;
//    // Receive buffer.
//    public byte[] buffer = new byte[BufferSize];
//    // Received data string.
//    public StringBuilder sb = new StringBuilder();
//}

public class AsynchronousClient
{
    // The port number for the remote device.
    private const int port = 2511;

    // ManualResetEvent instances signal completion.
    private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
    private static ManualResetEvent sendDone =
        new ManualResetEvent(false);
    private static ManualResetEvent receiveDone =
        new ManualResetEvent(false);
    public static Socket sck;

    // The response from the remote device.
    private static String response = String.Empty;
    public static byte[] buffer = new byte[1024];
    public static string ip = "";
    public static void StartClient()
    {
        // Connect to a remote device.
        try
        {
            // Establish the remote endpoint for the socket.
            // The name of the 
            // remote device is "host.contoso.com".
            var LEP = new IPEndPoint(IPAddress.Parse(ip), port);
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            AsynchronousClient.sck = client;

            // Create a TCP/IP socket.


            // Connect to the remote endpoint.
            //while (true)
            //{
            client.BeginConnect(LEP,
              new AsyncCallback(ConnectCallback), client);
            connectDone.WaitOne();
            //}
            // Send test data to the remote device.
            //Send("This is a test<EOF>");


            sendDone.WaitOne();
            // Receive the response from the remote device.
            Receive();
            receiveDone.WaitOne();

            // Write the response to the console.
            Console.WriteLine("Response received : {0}", response);

            // Release the socket.
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public static string packet(int pin, int int1, int pin2, int int2, int pin3, int int3)
    {
        var pck = pin + "|" + int1 + "|" + pin2 + "|" + int2 + "|" + pin3 + "|" + int3 + "|\n";
        if (pin == pin2 || pin2 == pin3 || pin == pin3)
        {
            //Console.WriteLine("error - pinN = pinN");
        }
        if (int1 == 0 || int2 == 0 || int3 == 0)
        {
            int1 = 1;
            int2 = 1;
            int3 = 1;
        }
        Send(pck);
        return pck;
    }
    public static void logic()
    {
        /*
        pin|int|pin|int|pin|int
        5|255|3|123|4|221
          */
        //   while (true)
        //for (int asd = 0; asd < 100; asd++)
        //{
        //   Send(packet(7, 244, 5, 12, 7, 44));
        //  } //Send("test_logic");
        //while (true) { var s = Console.ReadLine();
        //Send(s);
        //  }
    }
    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;

            // Complete the connection.
            client.EndConnect(ar);

            Console.WriteLine("Socket connected to {0}",
                client.RemoteEndPoint.ToString());

            // Signal that the connection has been made.
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Receive()
    {
        try
        {
            // Create the state object.
            var client = sck;
            Console.WriteLine("RECEIVED - " + Encoding.ASCII.GetString(buffer, 0, received_bytes));

            // Begin receiving the data from the remote device.
            client.BeginReceive(buffer, 0, buffer.Length, 0,
                new AsyncCallback(ReceiveCallback), sck);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public static int received_bytes = 0;
    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            var client = sck;

            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);
            received_bytes = bytesRead;
            Console.WriteLine("[BYTES] received count " + received_bytes);
            if (bytesRead > 0)
            {
                // There might be more data, so store the data received so far.


                // Get the rest of the data.
                client.BeginReceive(buffer, 0, buffer.Length, 0,
                    new AsyncCallback(ReceiveCallback), sck);
            }
            else
            {
                // All the data has arrived; put it in response.
                //if (state.sb.Length > 1)
                //{
                //    response = state.sb.ToString();
                //}
                // Signal that all bytes have been received.
                receiveDone.Set();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Send(String data)
    {
    System.Threading.Thread.Sleep(20);
        Console.WriteLine("DATA SENT - " + data + "\n");

        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        sck.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), sck);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);

            // Signal that all bytes have been sent.
            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}