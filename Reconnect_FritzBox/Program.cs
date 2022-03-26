using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Reconnect_FritzBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create("http://fritz.box:49000/igdupnp/control/WANIPConn1");
                httpRequest.Method = "POST";
                httpRequest.Headers["SOAPACTION"] = "urn:schemas-upnp-org:service:WANIPConnection:1#ForceTermination";
                httpRequest.ContentType = "text/xml; charset=utf-8";
                var data = "<?xml version=\"1.0\" encoding=\"utf-8\"?><s:Envelope s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope\"><s:Body><u:ForceTermination xmlns:u=\"urn:schemas-upnp-org:service:WANIPConnection:1\" /></s:Body></s:Envelope>";
                httpRequest.ContentLength = data.Length;
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                Console.WriteLine(httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed");
                Console.ReadLine();
            }
            Thread.Sleep(3000);
        }
    }
}