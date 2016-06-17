using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ieee_oui
{
    class Program
    {
        static string _address = "http://standards-oui.ieee.org/oui.txt";

        static void Main(string[] args)
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();

            Regex delimiter = new Regex(@"\t+");

            Regex regex = new Regex("[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}"); // OUI/MA-L

            Regex company_id = new Regex("[0-9a-fA-F]{6}");

            List<OUI> list = new List<OUI>();

            string s = string.Empty;

            int lineNo = 0;

            // Send a request asynchronously continue when complete
            client.GetAsync(_address, HttpCompletionOption.ResponseHeadersRead).ContinueWith(
                (requestTask) =>
                {
                    // Get HTTP response from completed task.
                    HttpResponseMessage response = requestTask.Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JsonValue and write out top facts for each country
                    response.Content.ReadAsStreamAsync().ContinueWith(
                        (readTask) =>
                        {
                            using (StreamReader sr = new StreamReader(readTask.Result))
                            {
                                // skip header
                                sr.ReadLine(); // OUI/MA-L                                                    Organization
                                sr.ReadLine(); // company_id                                                  Organization
                                sr.ReadLine(); //                                                             Address

                                while (!sr.EndOfStream)
                                {
                                    s = sr.ReadLine();
                                    Console.WriteLine(s);
                                    if (!String.IsNullOrWhiteSpace(s))
                                    {
                                        OUI oui = new OUI();
                                        Match match = regex.Match(s);
                                        if (match.Success)
                                        {
                                            oui.IEEE_OUI = match.Value;
                                            oui.Organization = delimiter.Split(s)[1];
                                            s = sr.ReadLine(); // company_id 
                                            match = company_id.Match(s);
                                            if (match.Success)
                                            {
                                                oui.Company_ID = match.Value;
                                                s = sr.ReadLine();
                                                if (!String.IsNullOrWhiteSpace(s))
                                                {
                                                    oui.Street = s.Trim();
                                                    s = sr.ReadLine();
                                                    oui.State = s.Trim();
                                                    s = sr.ReadLine();
                                                    oui.Country = s.Trim();
                                                }
                                            }
                                        }
                                        list.Add(oui);
                                    }
                                } // end of while
                            }
                            Console.WriteLine(list.Count());
                        });
                    Console.WriteLine("Read content as a stream in an asynchronous operation => ");
                });
            Console.WriteLine("GET request as an asynchronous operation => ");
            Console.ReadLine();
        }
    }

    class OUI
    {
        public string IEEE_OUI { get; set; }
        public string Company_ID { get; set; }
        public string Organization { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
