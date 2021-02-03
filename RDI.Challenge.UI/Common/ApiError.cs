using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.UI.Common
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("Erro: " + Message);
            s.AppendLine("StatusCode: " + StatusCode);
            s.AppendLine("StatusDescription: " + StatusDescription);
            s.AppendLine("Message: " + Message);

            return s.ToString();
        }
    }
}
