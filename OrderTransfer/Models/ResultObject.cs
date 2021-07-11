using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models
{
    public class ResultObject
    {
        public bool IsSuccessful { get; set; }

        public string Content { get; set; }
    }

    //public class Property
    //{
    //    [JsonProperty("$type")]
    //    public string Type { get; set; }
    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //}

    //public class ErrorObject
    //{
    //    [JsonProperty("$type")]
    //    public string Type { get; set; }
    //    public string ModelType { get; set; }
    //    public List<Property> Properties { get; set; }
    //    public string ErrorCode { get; set; }
    //    public string Hint { get; set; }
    //    public string ClassName { get; set; }
    //}
}
