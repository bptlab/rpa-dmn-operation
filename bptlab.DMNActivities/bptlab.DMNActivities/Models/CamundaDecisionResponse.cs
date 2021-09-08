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
            result = JsonSerializer.Deserialize<Dictionary<string,CamundaResponseVariableContent>[]>(jsonObject);
        }

        public Dictionary<String, Object> getResult()
        {
            var resultDictionary = new Dictionary<String, Object> { };
            foreach (var resultVariable in result[0])
            {
                var variableName = resultVariable.Key;
                var variableValue = resultVariable.Value.value;
                resultDictionary.Add(variableName, variableValue);
            }
            return resultDictionary;
        }

        private Dictionary<string, CamundaResponseVariableContent>[] result { get; set; }
    }

    public class CamundaResponseVariableContent
{
        public string type { get; set; }
        public dynamic value { get; set; }
        public object valueInfo { get; set; }
    }

}
