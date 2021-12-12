using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DATA
{
    public class CreateDispositivo
    {
        /// <summary>
        /// Cria dispositivo através do método post disponibilizado no postman
        /// </summary>
        /// <param name="idDispositivo"></param>
        public void CriarDispositivo(int idDispositivo)
        {
            var client = new RestClient("http://35.199.78.175:1026/v2/entities");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "helixiot");
            request.AddHeader("fiware-servicepath", "/");
            var body = @"{
                        " + "\n" +
                       @$"  ""id"": ""urn:ngsi-ld:entity:{idDispositivo.ToString("D3")}"",

                       " + "\n" +
                                    @"  ""type"": ""iot"",
                        " + "\n" +
                                    @"  ""temperature"": {
                        " + "\n" +
                                    @"  ""type"": ""float"",
                        " + "\n" +
                                    @"  ""value"": 0
                        " + "\n" +
                                    @"    }
                        " + "\n" +
                                    @",
                        " + "\n" +
                                    @"  ""humidity"": {
                        " + "\n" +
                                    @"  ""type"": ""float"",
                        " + "\n" +
                                    @"  ""value"": 0
                        " + "\n" +
                                    @"	}
                        " + "\n" +
                                    @"}
                        " + "\n" +
                                    @"
                        " + "\n" +
                                    @"  
                        " + "\n" +
                                    @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

    }
}
