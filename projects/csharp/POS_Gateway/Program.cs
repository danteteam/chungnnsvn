using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using EPAY_POS_GateWay.Common;
using EPAY_POS_GateWay.Configuration;
using EPAY_POS_GateWay.TopupInterface;
using EPAY_POS_GateWay.Proccess;
using Solab.Iso8583;
using Solab.Iso8583.Parsing;

namespace EPAY_POS_GateWay
{
    class Server
    {
        private static MessageFactory mfact;
        TcpClient socket;
        Transaction transObj = new Transaction();       

        public Server(TcpClient s)
        {
            socket = s;
        }

        private void Listen()
        {
            int count = 0;
            byte[] lenbuf = new byte[4];
            try
            {
                //For high volume apps you will be better off only reading the stream in this thread
                //and then using another thread to parse the buffers and process the requests
                //Otherwise the network buffer might fill up and you can miss a request.
                while (socket != null && socket.Connected && Thread.CurrentThread.IsAlive)
                {
                    if (socket.GetStream().Read(lenbuf, 0, 4) == 4)
                    {
                        int size, k;

                        size = 0;
                        for (k = 0; k < 4; k++)
                        {
                            size = size + (int)(lenbuf[k] - 48) * (int)(Math.Pow(10, 3 - k));
                        }

                        byte[] buf = new byte[size + 1];
                        //We're not expecting ETX in this case
                        socket.GetStream().Read(buf, 0, buf.Length);
                        count++;
                        //Set a job to parse the message and respond
                        //Delay it a bit to pretend we're doing something important
                        Processor p = new Processor(buf, socket, mfact);
                        new Thread(new ThreadStart(p.Respond)).Start();
                    }
                }
            }
            catch (IOException ex)
            {
                transObj.WriteLog("Exception occurred="+ ex.ToString());
            }
            transObj.WriteLog("Exiting after reading {"+ count.ToString() +"} requests");
            try
            {
                socket.Close();
            }
            catch (IOException) { }
        }

        private static IPAddress String2IPAddress(string IP_Address)
        {
            string[] arrServer_IP_Address = IP_Address.Split('.');
            byte[] byteIP = new byte[4];
            byteIP[0] = Convert.ToByte(arrServer_IP_Address[0]);
            byteIP[1] = Convert.ToByte(arrServer_IP_Address[1]); ;
            byteIP[2] = Convert.ToByte(arrServer_IP_Address[2]); ;
            byteIP[3] = Convert.ToByte(arrServer_IP_Address[3]); ;
            IPAddress Server_IP_Address = new IPAddress(byteIP);

            return Server_IP_Address;
        }
       
        static void Main(string[] args)
        {            
            Transaction transObj = new Transaction();
            TcpListener TcpListenerObj = new TcpListener(String2IPAddress(AppConfiguration.Server_IP_Address), AppConfiguration.Server_Port);
              
            //TcpListener TcpListenerObj = new TcpListener(9999);
            try
            {
                TcpListenerObj.Start();
            }
            catch (Exception ex)
            {
                transObj.WriteLog("Fail to start server("+ AppConfiguration.Server_IP_Address +"), port("+ AppConfiguration.Server_Port.ToString() +"), ex=" + ex.ToString());
            }

            transObj.WriteLog("EPAY_POS_GateWay Started at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:tt"));
            transObj.WriteLog("IP Address: " + AppConfiguration.Server_IP_Address + "), Port: " + AppConfiguration.Server_Port.ToString());
            transObj.WriteLog("Received messages:");

            try
            {
                mfact = new MessageFactory();               
                //@"D:\Working\Projects\pTopup\sources\POS_Gateway\POS_GW_Console\EPAY_POS_GateWay\POSConfig_ISOFile.xml"
                mfact = ConfigParser.CreateFromFile(AppConfiguration.App_Path + AppConfiguration.POSConfig_ISOFile);
            }
            catch (Exception ex)
            {
                transObj.WriteLog("Fail to load ISO config, ex=" + ex.ToString());                
            }                                           

            //transObj.WriteLog("test interface");
            //Proccess.TopupInterface TopupObj = new EPAY_POS_GateWay.Proccess.TopupInterface();
            //TopupObj.testTopupInterface();                       
            int nMessageCounter = 0;
            while (true)
            {
                try
                {
                    TcpClient client = TcpListenerObj.AcceptTcpClient();
                    Server sproc = new Server(client);
                    nMessageCounter++;
                    Console.Out.Write(nMessageCounter.ToString() + ". (" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:tt") + ")");
                    new Thread(new ThreadStart(sproc.Listen)).Start();
                    
                }
                catch (Exception ex)
                {
                    transObj.WriteLog("Fail to listen client, ex=" + ex.ToString());
                }

                //Console.ReadLine(); 
            }
                  
        }
    }

    class Processor
    {
        private byte[] msg;
        private TcpClient sock;
        MessageFactory mfact;

        public Processor(byte[] buf, TcpClient s, MessageFactory m)
        {
            msg = buf;
            sock = s;
            mfact = m;
        }

        public void Respond()
        {
            PosInterface posObj = new PosInterface();
            Transaction transObj = new Transaction();
            Thread.Sleep(400);
            try
            {
                //transObj.WriteLog("request ISO message: {"+ Encoding.ASCII.GetString(msg)+"}");
                IsoMessage request = mfact.ParseMessage(msg, 0);
                IsoMessage response = new IsoMessage();

                switch (request.Type.ToString("x"))
                {                    
                    //pos request logon/logoff
                    case "800":
                    {
                        response = posObj.PosSignOnOff(request); 
                        break;
                    }
                    //pos request download
                    case "200":
                    {
                        response = posObj.Download(request);
                        break;
                    }
                    //pos logon
                    default:
                    {
                        response = posObj.PosSignOnOff(request);
                        break;
                    }

                }
             
                //Response message
                byte[] outGoing = response.getByte(4, true);
                Console.Out.Write(System.Text.Encoding.ASCII.GetString(outGoing));                
                response.Write(sock.GetStream(), 4, true);
                

                //Console.Out.WriteLine("Sending response conf {0}", response.GetField(39));
                ////response.Write(sock.GetStream(), 4, true);
                //Console.Out.WriteLine("Parsing outgoing: {0}", Encoding.ASCII.GetString(response.getByte(4, false)));
                //byte[] outGoing = response.getByte(4, true);
                //sock.GetStream().Write(outGoing, 0, outGoing.Length);
                //IsoMessage OutGoingMSG = mfact.ParseMessage(outGoing, 4);
                //PrintMessage(incoming);
                //PrintMessage(OutGoingMSG);

                
                             
                //<parse type="0800">
                //<field num="2" type="LLVAR" length="0" />    	<!--mã đại lý -->   
                //<field num="7" type="NUMERIC" length="10"/>     <!--Transaction Date and Time -->
                //<field num="11" type="NUMERIC" length="6" />   <!--System Trace-->
                //<field num="18" type="NUMERIC" length="4" />    <!--Merchant Type: Giá trị là 6011 đối với POS-->
                //<field num="32" type="LLVAR" length="0" />      <!--Acquiring Institution Identification Code-->
                //<field num="48" type="LLVAR" length="0" />    <!--nội dung hướng dẫn -->
                //<field num="52" type="LLLVAR" length="0" />     <!--Đưa mã máy POS vào-->
                //<field num="70" type="LLLVAR" length="0" />    <!--kiểu network request cần xử lý-	001: Signon-	002: Signoff-	161: Key Exchange-->   
                // </parse>

               
            }
            catch (IOException ex)
            {
                transObj.WriteLog("Fail to response, ex="+ ex.ToString());
            }
        }       
    }

}
