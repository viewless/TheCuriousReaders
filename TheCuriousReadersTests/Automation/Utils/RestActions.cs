using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using static SpecFlowTemplate.Utils.RestConnection;
using static Newtonsoft.Json.JsonConvert;

namespace SpecFlowTemplate.Utils
{
    public class RestActions
    {
        public IRestResponse sendRequest(string uri, string method)
        {
            IRestRequest restRequest = buildRequest(uri, method);

            return getAPIConnection().Execute(restRequest);
        }

        public IRestResponse sendRequest(string uri, string method, JObject body)
        {
            IRestRequest restRequest = buildRequest(uri, method);
            restRequest.AddParameter("application/json",
                                      body,
                                      ParameterType.RequestBody);

            return getAPIConnection().Execute(restRequest);
        }

        private IRestRequest buildRequest(string uri, string method)
        {
            RestRequest restRequest = new RestRequest(uri);
            restRequest.Method = method switch
            {
                "POST" => Method.POST,
                "PUT" => Method.PUT,
                "GET" => Method.GET,
                "DELETE" => Method.DELETE,
                _ => throw new ArgumentOutOfRangeException($"HTTP method \"{method}\" is not supported"),
            };

            return restRequest;
        }

        public T DeserializeJsonResponse<T>(IRestResponse restResponse)
        {
            if (restResponse is null)
            {
                throw new System.Exception();
            }

            var content = restResponse.Content;
            T deseiralizeObject = DeserializeObject<T>(content);
            return deseiralizeObject;
        }
    }
}