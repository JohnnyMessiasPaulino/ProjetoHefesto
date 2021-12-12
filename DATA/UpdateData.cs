using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DATA
{
    public class UpdateData
    {
        public void EnviarDados (int idDispositivo, double temperatura, double umidade)
        {
            var client = new RestClient($"http://35.199.78.175:1026/v2/entities/urn:ngsi-ld:entity:{idDispositivo.ToString("D3")}/attrs");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "helixiot");
            request.AddHeader("fiware-servicepath", "/");
                                    var body = @"${
                        " + "\n" +
                                    @"  ""temperature"": {
                        " + "\n" +
                                    @"  ""type"": ""float"",
                        " + "\n" +
                                    @$"  ""value"":{temperatura}
                        " + "\n" +
                                    @"    }
                        " + "\n" +
                                    @",
                        " + "\n" +
                                    @"  ""humidity"": {
                        " + "\n" +
                                    @"  ""type"": ""float"",
                        " + "\n" +
                                    @$"  ""value"": {umidade}
                        " + "\n" +
                                    @"	}
                        " + "\n" +
                                    @"}
                        " + "\n" +
                                    @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }
    }
}
