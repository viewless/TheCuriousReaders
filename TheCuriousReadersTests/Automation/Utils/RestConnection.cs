using RestSharp;
using System.Runtime.CompilerServices;

namespace SpecFlowTemplate.Utils
{
    public static class RestConnection
    {
        private static RestClient _restClient;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void initConnection(string url)
        {
            if (_restClient is null)
            {
                _restClient = new RestClient(url);
            }
        }

        public static RestClient getAPIConnection()
        {
            return _restClient;
        }
    }
}