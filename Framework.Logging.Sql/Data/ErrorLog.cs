using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;

namespace Framework.Logging.Sql.Data
{
    public class ErrorLog
    {
        public ErrorLog()
        {
            CreationDate = DateTime.UtcNow;
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string ServerIdentity { get; set; }
        public string UserIdentity { get; set; }
        public string ApplicationName { get; set; }
        public DateTime CreationDate { get; private set; }
        public string IP
        {
            get; set;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append($"\"Id\":\"{Id}\",");
            sb.Append($"\"Type\":\"{Type}\",");
            sb.Append($"\"Message\":\"{Message}\",");
            sb.Append($"\"Data\":\"{Data}\",");
            sb.Append($"\"ServerIdentity\":\"{ServerIdentity}\",");
            sb.Append($"\"UserIdentity\":\"{UserIdentity}\",");
            sb.Append($"\"ApplicationName\":\"{ApplicationName}\",");
            sb.Append($"\"CreationDate\":\"{CreationDate}\"");
            sb.Append($"\"IP\":\"{IP}\"");
            sb.Append("}");

            return sb.ToString();
        }
    }
}
