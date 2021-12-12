using Newtonsoft.Json;
using ProjetoHefesto.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DATA
{
    public class ObterDadosDispositivo
    {
        /// <summary>
        /// Obtem dados do dispositivo através do método get do postman
        /// </summary>
        /// <param name="ip">ip do local que o helix esta instalado</param>
        /// <returns></returns>
        public IRestResponse ObterDados()
        {
            //string ipHelix = ip;
            string ipHelix = "35.199.78.175"; //Alterar para receber ip como parametro
            var client = new RestClient($"http://{ipHelix}:1026/v2/entities");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("fiware-service", "helixiot");
            request.AddHeader("fiware-servicepath", "/");
            IRestResponse response = client.Execute(request);
            return response;
            //Console.WriteLine(response.Content);
        }

        /// <summary>
        /// Método para converter o Json obtido em um objeto
        /// </summary>
        /// <param name="response">parametro em Json obtido no ObterDados</param>
        /// <returns></returns>
        public List <DispositivoViewModel> MontaDispositivos(IRestResponse response)
        {
            List<DispositivoViewModel> dispositivos = JsonConvert.DeserializeObject<List<DispositivoViewModel>>(response.Content);
            return dispositivos;
        }

        /// <summary>
        /// Método para obter a quantidade de dispositivos(Sensores) cadastrados no helix
        /// </summary>
        /// <returns></returns>
        public int ObtemNumeroDeDispositivos()
        {
            ObterDadosDispositivo dados = new ObterDadosDispositivo();
            IRestResponse response = dados.ObterDados();
            List<DispositivoViewModel> dispositivos = dados.MontaDispositivos(response);
            int numeroDispositivos = dispositivos.Count();

            return numeroDispositivos;
        }

        public SensorViewModel UpdateSensor(SensorViewModel sensorRecebido)
        {
            ObterDadosDispositivo dis = new ObterDadosDispositivo();
            List<DispositivoViewModel> dispositivos = new List<DispositivoViewModel>();

            IRestResponse response = dis.ObterDados();
            dispositivos = dis.MontaDispositivos(response);

            SensorViewModel sensorAtualizado = new SensorViewModel();
            sensorAtualizado = sensorRecebido;

            foreach (DispositivoViewModel d in dispositivos)
            {
                if (sensorRecebido.Id == Convert.ToInt32(d.Id.Substring(d.Id.Length - 3, 3)))
                {
                    sensorAtualizado.Temperatura = Convert.ToDouble(d.Temperature.Value);
                    sensorAtualizado.Umidade = Convert.ToDouble(d.Humidity.Value);
                    sensorAtualizado.DataAtualizacao = DateTime.Now.ToString();
                }
            }

            return sensorAtualizado;
        }
    }
}
