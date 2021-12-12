using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DATA
{
    public class DeleteData
    {
        public void DeletarDispositivo(int numeroDispositivo)
        { 
            var client = new RestClient($"http://35.199.78.175:1026/v2/entities/urn:ngsi-ld:entity:{numeroDispositivo.ToString("D3")}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("fiware-service", "helixiot");
            request.AddHeader("fiware-servicepath", "/");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
