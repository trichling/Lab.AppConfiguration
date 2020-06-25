## Connect to app settings store
> cd src\Lab.AppConfiguration
> dotnet user-secrets init
> dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
> dotnet restore
> dotnet user-secrets set ConnectionStrings:AppConfig <your_connection_string>