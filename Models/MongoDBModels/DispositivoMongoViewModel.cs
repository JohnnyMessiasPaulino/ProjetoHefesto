using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHefesto.Models
{
    public class DispositivoMongoViewModel
    {

            public string _id { get; set; }

            public string NomeDispositivo { get; set; }

            public DateTime RecvTime { get; set; }

            public string AttrName { get; set; }

            public string AttrType { get; set; }

            public string AttrValue { get; set; }

    }
}
