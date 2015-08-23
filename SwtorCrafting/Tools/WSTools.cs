using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SwtorCrafting.Tools
{
    public class WSTools
    {
        /**
         * The constant CHARSET_UTF8.
         */
        public const string CHARSET_UTF8 = "UTF-8";

        public const string FORMAT_JSON = "json";

        public const string FORMAT_XML = "xml";

        public string uriBase { get; set; }
        public string apiKey { get; set; }
        public string charset { get; set; }

        public WSTools(string uriBase, string apiKey, string charset)
        {
            this.uriBase = uriBase;
            this.apiKey = apiKey;
            this.charset = charset;
        }

        /**
         * Call web service.
         *
         * @param String uri
         * @param String apiKey
         * @param Int16 method
         * @param String charset
         * @return the result
         */
        public List<DataObject> GetResource<DataObject>(string resourceUri, string format) 
            where DataObject : new()
        {
            List<DataObject> result = new List<DataObject>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(this.uriBase);
            string requestUri = BuildRequestUri(resourceUri, format);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = response.Content.ReadAsAsync<DataObject[]>().Result;
                foreach (var dataObject in dataObjects)
                {
                    result.Add(dataObject);
                }
            }
            else
            {
                result = null;
            }  

            return result;
        }

        protected string BuildRequestUri(string requestUri, string format)
        {
            return requestUri + ".json?api_key=" + this.apiKey;
        }


    }
}
