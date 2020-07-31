using System.Collections.Generic;
using RestSharp;

namespace Framework.Src.Tools
{
    public class APIController 
    {
        public string BaseURL;
        public RestClient restClient;

        public APIController(string url)
        {
            BaseURL = url;
            restClient = new RestClient(url);
        }
 
        public IRestResponse Get(string endPoint, Dictionary<string,string> headers)
        {
            //Creates the http client
            var client = new RestClient(BaseURL + endPoint);

            //sets the time out
            client.Timeout = -1;

            //Sets the request type as a get
            var request = new RestRequest(Method.GET);

            //Adds headers
            if (headers != null)
            {
                foreach(KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            //Excutes the request
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}