# rpa-dmn-operation
This repo contains the sources for a DMN activity for the RPA tool UiPath.
For creating and evaluating the DMN decisions, an instance of [Camundas open-source engine](https://camunda.com/download/) is required.

## Installation

1. Go to the [releases](https://github.com/bptlab/rpa-dmn-operation/releases) section, and download the `*.nupkg` file from the _assets_.
1. Copy the package to the [UiPath directory for local packages](https://docs.uipath.com/studio/docs/managing-activities-packages#adding-custom-feeds).
1. In your UiPath project, [install the package](https://docs.uipath.com/studio/docs/managing-activities-packages#installing-packages). (_All packages_ → _local_, you can also search for `DMN`)
1. Now, the new DMN activity should be available for use as any other UiPath activity. You can find it by the name `External Decision Service`.

## Usage

After adding a DMN acivity to your workflow, the following elements need to be configured in the _Properties_-Panel:

**Inputs**:
- **Input Variables**: the list of variables in UiPath that should be passed to the decision service
- **Input Variables Names**: the list of input names of the decision table in the same logical order as the input variables.

**Options**:
- **Decision Key**: The identifier of the decision to evaluate. This ID can be found in the Camunda engine after deploying the decision table.
- **Service Host**: The URL under which the Camunda engine is reachable.

**Output**:
- **Decision Result**: The name of the UiPath variable, the decision result(s) should be stored in.

> Example: The decision table requires the inputs _Type of Shipment_ and _Destination_. In the RPA bot, those values are stored in the variables _shipmentType_ and _destination_. To provide the correct mapping of UiPath variables and inputs of the decision table the following must be configured:
> 
> Input Variables = `{shipmentType, destination}`
> 
> Input Variables Names = `{"Type of Shipment", "destination"}`
> 
> By this, the DMN activity knows how to map the variables to the decision table inputs.
>
> The decision key looks something like `"Decision_1hyvppg"` and the service host, if the engine is deployed locally, might be `"http://localhost:8080"`.



## Limitations
Please bear in mind that this activity is currently in the prototype stage.
One major current limitation is, that only string values can be handled. If you want to user other data types, you currently would need to convert them in your robot workflow.

