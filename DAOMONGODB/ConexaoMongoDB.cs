using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProjetoHefesto.DATA;
using ProjetoHefesto.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.DAOMONGODB
{
    public class ConexaoMongoDB
    {

        /// <summary>
        /// Obter conexão com o MongoDB através de uma string de conexão 
        /// </summary>
        /// <returns></returns>
        public MongoClient GetConexaoMongoDB()
        {
            string userName = "helix";
            string password = "H3l1xNG";
            string ipEPorta = "35.199.78.175:27000";
            string databaseName = "sth_helixiot";
            

            MongoClient client = new MongoClient($"mongodb://{userName}:{password}@{ipEPorta}/{databaseName}?retryWrites=true");

            return client;
        }

        /// <summary>
        /// Montar dados recebidos em um objeto do tipo Lista de DispositivoMongoViewModel
        /// </summary>
        /// <param name="collectionNumero">Número do dispositivo</param>
        /// <returns>Retorna Lista de DispositivoMongoViewModel </returns>
        public List<DispositivoMongoViewModel> MontaDadosRecebidosEmObjeto()
        {
            string databaseName = "sth_helixiot";
            ObterDadosDispositivo disp = new ObterDadosDispositivo();
            int numeroDispositivos = disp.ObtemNumeroDeDispositivos();

            List<DispositivoMongoViewModel> dispositivoObtidoMongo = new List<DispositivoMongoViewModel>();

            for (int i = 0; i <= numeroDispositivos; i++)
            {
                string collectionName = $"sth_/_urn:ngsi-ld:entity:{i.ToString("D3")}_iot";
                ConexaoMongoDB mongo = new ConexaoMongoDB();
                MongoClient client = mongo.GetConexaoMongoDB();
                IMongoDatabase database = client.GetDatabase($"{databaseName}");
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>($"{collectionName}");

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("attrType", "float");
                List<BsonDocument> result = collection.Find(filter).ToList();

                
                var dotNetList = result.ConvertAll(BsonTypeMapper.MapToDotNetValue);

                string jsonLista = JsonConvert.SerializeObject(dotNetList);

                List<DispositivoMongoViewModel> obterJson = new List<DispositivoMongoViewModel>();
                obterJson = JsonConvert.DeserializeObject<List<DispositivoMongoViewModel>>(jsonLista);

                DateTime dateTime = DateTime.Now;

                foreach (var dis in obterJson)
                {
                    dispositivoObtidoMongo.Add(dis);
                    dis.NomeDispositivo = collectionName;
                }
            }

            return dispositivoObtidoMongo;
        }


    }
}
