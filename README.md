# api-request-tool-csharp

Windows desktop application for sending signed API request to T Cloud Public written in C#.

![API Request Tool](./doc/APIRequestTool.png)

For signing request, this tool uses:
- https://github.com/opentelekomcloud-community/otc-api-sign-sdk-csharp 
  (hosted on [GitHub NuGet registry](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry) )

To use and install this tool following framework SDKs need to be installed:
- [.Net Framework SDK 10.0](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

## Environment variables 

Following enviribment variables should be set

| Environment variable | Description                                                |
| -------------------- | ---------------------------------------------------------- |
| GITHUB_USERNAME      | GitHub username to acces nuget repository on GitHub        |
| GITHUB_TOKEN         | GitHub token to acces nuget repository on GitHub           |
| OTC_SDK_AK           | T Cloud Public access key (used to prefill *key* field)    | 
| OTC_SDK_SK           | T Cloud Public secret key (used to prefill *secret* field) |                           |


Using powershell, variables can be set using:

```ps1
$env:GITHUB_USERNAME = "<your github username>"
$env:GITHUB_TOKEN = "<your github token>"
$env:OTC_SDK_AK = "<your access key>"
$env:OTC_SDK_SK = "<your secret key>"
```

## Build

To build execute:

```ps1
dotnet build api-request-tool.csproj -c Debug
```

## Run

To run execute:

```ps1
dotnet run --project api-request-tool.csproj -c Debug
```

## Build standalone exe

To build standalone exe:

```ps1
dotnet publish .\api-request-tool.csproj -c Release
```

Output will be in:

```ps1
bin/Release/net10.0-windows/win-x64/publish
```

Main file is:

```ps1
bin/Release/net10.0-windows/win-x64/publish/api-request-tool.exe
```


> Warranty Disclaimer
> -------------------
> THE OPEN SOURCE SOFTWARE IN THIS PRODUCT IS DISTRIBUTED IN THE HOPE THAT IT
> WILL BE USEFUL,BUT WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY
> OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE.
> 
> SEE THE APPLICABLE LICENSES FOR MORE DETAILS.
