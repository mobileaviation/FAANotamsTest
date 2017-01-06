using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPPostTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dit is een HTTP Post test");

            String request_Body = "searchType=0&designatorsForLocation=EHAA&designatorForAccountable=&latDegrees=&latMinutes=0&latSeconds=0&longDegrees=&longMinutes=0&longSeconds=0&radius=10&sortColumns=5+false&sortDirection=true&designatorForNotamNumberSearch=&notamNumber=&radiusSearchOnDesignator=false&radiusSearchDesignator=&latitudeDirection=N&longitudeDirection=W&freeFormText=&flightPathText=&flightPathDivertAirfields=&flightPathBuffer=4&flightPathIncludeNavaids=true&flightPathIncludeArtcc=false&flightPathIncludeTfr=true&flightPathIncludeRegulatory=false&flightPathResultsType=All+NOTAMs&archiveDate=&archiveDesignator=&offset=0&notamsOnly=false&filters=&minRunwayLength=&minRunwayWidth=&runwaySurfaceTypes=&predefinedAbraka=&predefinedDabra=&flightPathAddlBuffer=";

            SetRequest1(request_Body);
            //PostForm();
            Console.ReadKey();
        }


        public static void SetRequest(string mXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://notams.aim.faa.gov/notamSearch/search");
            request.Method = "POST";
            request.ContentType = "application/json, text/plain, */*";
            string postData = mXml;
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
        }

        public static void SetRequest1(string mxml)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://notams.aim.faa.gov/notamSearch/search");
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(mxml);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            myHttpWebRequest.ContentLength = data.Length;
            myHttpWebRequest.Referer = "https://notams.aim.faa.gov/notamSearch/nsapp.html#/";
            myHttpWebRequest.Host = "notams.aim.faa.gov";
            myHttpWebRequest.Accept = "application/json, text/plain, */*";

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();


            try
            {
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                Stream responseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                string pageContent = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

    }
}
