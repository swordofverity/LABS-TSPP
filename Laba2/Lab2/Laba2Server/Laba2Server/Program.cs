using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.IO.Compression;


namespace Laba2Server
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] msg = null;
            string sql1 = "SELECT * FROM[Clients]";
            string sql2 = "SELECT * FROM[Holdings]";
            string sql3 = "SELECT * FROM[Master]";
            string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vladi\OneDrive\ХАИ\2017\ТСПП\Лабораторная работа №2\Lab2\Laba2Server\Laba2Server\Database1.mdf;Integrated Security=True";
           using(SqlConnection sqlConnection = new SqlConnection(connection))
           {
               sqlConnection.Open();
               SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, sqlConnection);
               DataSet dataSet = new DataSet();
               adapter1.Fill(dataSet, "Clients");           
               SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, sqlConnection);
               adapter2.Fill(dataSet, "Holdings");
               SqlDataAdapter adapter3 = new SqlDataAdapter(sql3, sqlConnection);
               adapter3.Fill(dataSet, "Master");
               Byte[] data;
               MemoryStream mem = new MemoryStream();
               GZipStream zip = new GZipStream(mem, CompressionMode.Compress);
               dataSet.WriteXml(zip, XmlWriteMode.WriteSchema);
               zip.Close();
               data = mem.ToArray();
               mem.Close();
               msg = data;         
           }
           IPHostEntry ipHost = Dns.GetHostEntry("localhost");
           IPAddress ipAddr = ipHost.AddressList[0];
           IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

           Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
           try
           {
               sListener.Bind(ipEndPoint);
               sListener.Listen(10);

            
               while (true)
               {                 
                   Socket handler = sListener.Accept();

                   handler.Send(msg);
                    Console.ForegroundColor = ConsoleColor.Green;
                   Console.WriteLine("Соединение с сервером: {0}.", ipEndPoint);
                  

                   handler.Shutdown(SocketShutdown.Both);
                   handler.Close();
                   Console.WriteLine("Соединение установлено!");
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
           }
           finally
           {
               Console.ReadLine();
           }
           Console.ReadLine();
        }
    }
}
