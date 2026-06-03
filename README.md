# api-request-tool-csharp

> [!NOTE]
> Work in progress..


Windows tool for sending signed API request to T Cloud Public.

![API Request Tool](./doc/APIRequestTool.png)

Tool uses:
- https://github.com/opentelekomcloud-community/otc-api-sign-sdk-csharp

## Environment variables 

```
$env:GITHUB_USERNAME = 
$env:GITHUB_TOKEN = 
$env:OTC_SDK_AK =
$env:OTC_SDK_SK =
```

## Build

```
dotnet build api-request-tool.csproj -c Debug
```

## Run

```
dotnet run --project api-request-tool.csproj -c Debug
```


> Warranty Disclaimer
> -------------------
> THE OPEN SOURCE SOFTWARE IN THIS PRODUCT IS DISTRIBUTED IN THE HOPE THAT IT
> WILL BE USEFUL,BUT WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY
> OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
> 
> SEE THE APPLICABLE LICENSES FOR MORE DETAILS.
