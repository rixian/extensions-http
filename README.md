# Rixian HttpClient Extensions

**This library provides extension methods for setting up an HttpClient.**

[![NuGet package](https://img.shields.io/nuget/v/Rixian.Extensions.Http.svg)](https://nuget.org/packages/Rixian.Extensions.Http)
[![codecov](https://codecov.io/gh/rixian/extensions-http/branch/master/graph/badge.svg)](https://codecov.io/gh/rixian/extensions-http)

## Features

* Configure an [ITokenClient](https://github.com/rixian/extensions-tokens) as the source of the Authorization header.
* Configure the Authorization header with a static Bearer token value.
* Configure any header with a static value.

## Usage

### ITokenClient - Named
```csharp
IServiceCollection services = ...;

services.AddTokenClient("tokenClient123", ...);

services.AddHttpClient("test")
    .AddTokenClient("tokenClient123"); // Adds the header "Authorization: Bearer {access_token}"
```

### ITokenClient - DI Lookup
```csharp
IServiceCollection services = ...;

services.AddTokenClient(...);

services.AddHttpClient("test")
    .AddTokenClient(svc =>
    {
        return svc
            .GetRequiredService<ITokenClientFactory>()
            .GetTokenClient();
    }); // Adds the header "Authorization: Bearer {access_token}"
```

### ITokenClient - Direct Instance
```csharp
IServiceCollection services = ...;

ITokenClient tokenClient = ...;

services.AddHttpClient("test")
    .AddTokenClient(tokenClient); // Adds the header "Authorization: Bearer {access_token}"
```

### Static Bearer Token
```csharp
IServiceCollection services = ...;

string bearerToken = "REPLACE_ME";

services.AddHttpClient("test")
    .AddBearerToken(bearerToken); // Adds the header "Authorization: Bearer REPLACE_ME"
```

### Static Authorization Header
```csharp
IServiceCollection services = ...;

string scheme = "HDR_SCHEME";
string parameter = "HDR_PARAM";

services.AddHttpClient("test")
    .AddAuthorizationHeader(scheme, parameter); // Adds the header "Authorization: HDR_SCHEME HDR_PARAM"
```

### Static Header
```csharp
IServiceCollection services = ...;

string name = "HDR_NAME";
string value = "HDR_VALUE";

services.AddHttpClient("test")
    .AddHeader(name, value); // Adds the header "HDR_NAME: HDR_VALUE"
```
