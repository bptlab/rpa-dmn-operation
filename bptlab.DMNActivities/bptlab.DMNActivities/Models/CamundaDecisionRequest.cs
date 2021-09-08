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
        public CamundaDecisionRequest()
        {
            variables = new Dictionary<String, CamundaDecisionRequestVariable>();
        }
        public CamundaDecisionRequest(String[] names, dynamic[] values)
        {
            variables = new Dictionary<String, CamundaDecisionRequestVariable>();

            if (names.Length != values.Length)
            {
                throw new Exception("Number of passed variables names must match with number of provided values.");
            }
            for (int i = 0; i < names.Length; i++)
            {
                variables[names[i]] = new CamundaDecisionRequestVariable(values[i]);
            }
        }

        public String ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public Dictionary<String, CamundaDecisionRequestVariable> variables { get; }
    }

    public class CamundaDecisionRequestVariable
    {
        public CamundaDecisionRequestVariable(dynamic varValue)
        {
            value = varValue;
            string typeIdentifier = value.GetType().ToString();
            type = typeIdentifier.Replace("System.", "");
        }
        public dynamic value { get; set; }
        public string type{ get; set; }
    }
}
