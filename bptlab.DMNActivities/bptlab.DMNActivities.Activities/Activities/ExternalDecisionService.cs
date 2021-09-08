using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using bptlab.DMNActivities.Activities.Properties;
using bptlab.DMNActivities.Models;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace bptlab.DMNActivities.Activities
{
    [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_DisplayName))]
    [LocalizedDescription(nameof(Resources.ExternalDecisionService_Description))]
    public class ExternalDecisionService : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_InputVariables_DisplayName))]
        [LocalizedDescription(nameof(Resources.ExternalDecisionService_InputVariables_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<dynamic[]> InputVariables { get; set; }

        [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_InputVariablesNames_DisplayName))]
        [LocalizedDescription(nameof(Resources.ExternalDecisionService_InputVariablesNames_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<String[]> InputVariablesNames { get; set; }

        [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_ServiceHost_DisplayName))]
        [LocalizedDescription(nameof(Resources.ExternalDecisionService_ServiceHost_Description))]
        [LocalizedCategory(nameof(Resources.Options_Category))]
        public InArgument<string> ServiceHost { get; set; }

        [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_DecisionKey_DisplayName))]
        [LocalizedDescription(nameof(Resources.ExternalDecisionService_DecisionKey_Description))]
        [LocalizedCategory(nameof(Resources.Options_Category))]
        public InArgument<string> DecisionKey { get; set; }

        [LocalizedDisplayName(nameof(Resources.ExternalDecisionService_DecisionResult_DisplayName))]
        [LocalizedDescription(nameof(Resources.ExternalDecisionService_DecisionResult_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<Dictionary<String, Object>> DecisionResult { get; set; }

        #endregion


        #region Constructors

        public ExternalDecisionService()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (InputVariables == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(InputVariables)));
            if (InputVariablesNames == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(InputVariablesNames)));
            if (ServiceHost == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ServiceHost)));
            if (DecisionKey == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(DecisionKey)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var inputVariables = InputVariables.Get(context);
            var inputVariablesNames = InputVariablesNames.Get(context);
            var serviceHost = ServiceHost.Get(context);
            var decisionKey = DecisionKey.Get(context);

            var decisionRequest = new CamundaDecisionRequest(inputVariablesNames, inputVariables);

            var httpClient = new HttpClient();
            var jsonDecisionRequest = decisionRequest.ToJson();
            var data = new StringContent(jsonDecisionRequest, Encoding.UTF8, "application/json");
            var url = serviceHost + "/engine-rest/decision-definition/key/" + decisionKey + "/evaluate";

            var response = await httpClient.PostAsync(url, data);

            var decisionResultJson = response.Content.ReadAsStringAsync().Result;

            var decisionReponse = new CamundaDecisionResponse(decisionResultJson);

            // Outputs
            return (ctx) => {
                DecisionResult.Set(ctx, decisionReponse.getResult());
            };
        }

        #endregion
    }
}

