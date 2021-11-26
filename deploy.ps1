# https://dotnet.microsoft.com/download
# https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli
# https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda#install-the-azure-functions-core-tools

az login
az group create -l westeurope -n walker-assessment-rg-1
az deployment group create --resource-group "walker-assessment-rg-1" --template-file .\template.json  --parameters ./parameters.json

dotnet clean --configuration Release /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary
dotnet publish --configuration Release /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary

Start-Sleep -s 30

func azure functionapp publish "walker-assessment-fn-1" --publish-local-settings --nozip --force


