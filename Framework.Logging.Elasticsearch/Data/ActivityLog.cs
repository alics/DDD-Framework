using System;
using System.Text;

namespace Framework.Logging.Elasticsearch.Data
{
    public class ActivityLog: BaseLogObject
    { 
        public string IP
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
        //    sb.Append($"\"IP\":\"{IP}\"");
        //    sb.Append("}");

        //    return sb.ToString();
        //}
    }
}
