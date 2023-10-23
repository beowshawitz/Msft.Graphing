# Introduction 
I had a use case and need for some more practice, where I wanted to utilize some of the Microsoft Graph functionality to look up user and group information within Microsoft Entra ID. I started with a console application; and then I moved into a Azure Function App, where I could submit HTTP requests for the information. 

# Getting Started
This is a .Net Core 6 application, which uses the Microsoft Authentication Library (MSAL) to acquire an authentication token. In order to run these two projects, you will need to have the following:
- An Azure account with an active subscription
- An application registration, configured with a client secret, like this [URL](https://learn.microsoft.com/en-us/graph/auth-register-app-v2)
- Graph "Application permissions" granted for "User.Read.All & Group.Read.All" permissions like [Get access without a user](https://learn.microsoft.com/en-us/graph/auth-v2-service?tabs=http)
- Enter the specific configuration for your Azure information in either the Visual Studio secrets or an appsettings file.
  - "AzureCloudInstance": "AzurePublic"
  - "Domain": ""
  - "TenantId": ""
  - "ClientId": ""
  - "ClientSecret": ""

Once the configuration is completed, you should be able to run the project and get the results to come back in either the console application or through Http Requests on the function.
