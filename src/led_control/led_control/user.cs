//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//namespace BlackSPACE.Server
//{
//    public class user
//    {
//        public byte[] buffer = new byte[1024];

//        private Socket Socket { get; set; }
//        public user (Socket hnd)
//        {
//            Socket = hnd;
//            hnd.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(Read_call_back), this);
//        }
//        public void Console(string a)
//        {
//            System.Console.WriteLine("PACKET - " + a);
//        }
//        static void Main()
//        {
//            socket.socket.start();
//        }
//        public void Read_call_back(IAsyncResult ar)
//        {   try
//            {
              
//                var rec_bytes = Socket.EndReceive(ar);

//                var current_packet = Encoding.UTF8.GetString(buffer,0,rec_bytes);
//                Console(current_packet);
//                System.Console.WriteLine(current_packet);
//                var s = System.Console.ReadLine();
//                    Send(  s );
//            }
//            catch
//            {

//            }
//        }
//        public void Send(string data)
//        {
//            Console(" SENT -" + data);
//            try
//            {
//                if (this.Socket != null && this.Socket.Connected)
//                {
//                  //  Out.WriteLine("To client: " + data, "Packets", ConsoleColor.DarkGreen);
//                    byte[] byteData = Encoding.UTF8.GetBytes(data + (char)0x00);
//                    this.Socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(this.SendCallback), Socket);
//                }
//            }
//            catch (SocketException e)
//            {
          
                
//            }
//        }
//        private void SendCallback(IAsyncResult ar)
//        {
//            try
//            {
//                // Retrieve the socket from the state object.
//                Socket handler = (Socket)ar.AsyncState;

//                // Complete sending the data to the remote device.
//                int bytesSent = handler.EndSend(ar);
//            }
//            catch (Exception e)
//            {
//               //.WriteLine(e.Message, "Exception", ConsoleColor.Red);
//            }
//        }
//    }
//}
