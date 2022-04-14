using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Logging.Elasticsearch.Data
{
    public class BaseLogObject
    {
        public BaseLogObject()
        {
            creationDate = DateTime.UtcNow;
        }
        public string type { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string serverIdentity { get; set; }
        public string userIdentity { get; set; }
        public string applicationName { get; set; }
        public DateTime creationDate { get; private set; }
        public string ip
        {
            get; set;
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("{");
        //    sb.Append($"\"Id\":\"{id}\",");
        //    sb.Append($"\"Type\":\"{type}\",");
        //    sb.Append($"\"Message\":\"{message}\",");
        //    sb.Append($"\"Data\":\"{data}\",");
        //    sb.Append($"\"ServerIdentity\":\"{serverIdentity}\",");
        //    sb.Append($"\"UserIdentity\":\"{userIdentity}\",");
        //    sb.Append($"\"ApplicationName\":\"{applicationName}\",");
        //    sb.Append($"\"CreationDate\":\"{creationDate}\"");
        //    sb.Append($"\"IP\":\"{ip}\"");
        //    sb.Append("}");

        //    return sb.ToString();
        //}
    }
}
