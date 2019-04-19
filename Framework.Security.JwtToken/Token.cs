using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Security.JwtToken
{
    public class Token
    {
        public string access_token { get; set; }

        public string refresh_token { get; set; }

        public string token_type { get; set; }
    }
}
