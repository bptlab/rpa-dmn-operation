using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bptlab.DMNActivities.Models
{
    public class CamundaDecisionResponse
    {
        public CamundaDecisionResponse(String jsonObject)
        {
            result = JsonSerializer.Deserialize<Dictionary<string, Dictionary<String, String>>[]>(jsonObject);
        }

        public string getResult()
        {
            return result[0]["result"]["value"];
        }

        private Dictionary<string, Dictionary<String, String>>[] result { get; }
    }
}
