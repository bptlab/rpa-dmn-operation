using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bptlab.DMNActivities.Models
{
    public class CamundaDecisionRequest
    {
        public CamundaDecisionRequest() {
            variables = new Dictionary<String, Dictionary<String, String>> ();
        }
        public CamundaDecisionRequest(String[] names, String[] values)
        {
            variables = new Dictionary<String, Dictionary<String, String>>();

            if (names.Length != values.Length)
            {
                throw new Exception("Number of passed variables names must match with number of provided values.");
            }
            for (int i = 0; i < names.Length; i++)
            {
                addVariable(names[i], values[i]);
            }
        }

        public void addVariable(string name, string value)
        {
            variables[name] = new Dictionary<string, string> { ["value"] = value };
            return;
        }

        public String ToJson()
        {
            var requestBody = new Dictionary<string, Dictionary<string, Dictionary<String, String>>>
            {
                ["variables"] = variables
            };
            return JsonSerializer.Serialize(requestBody);
        }

        private Dictionary<String, Dictionary<String, String>> variables { get; }
    }
}
