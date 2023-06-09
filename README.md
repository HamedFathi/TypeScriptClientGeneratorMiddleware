For a long time, I have been looking for a way to connect the back-end and front-end teams working on the same APIs.

In this use case, the back-end team provides a bunch of APIs, then the front-end team should follow them to call APIs correctly, but how is it possible to get the correct info from the back-end side or update the front-end side after some changes frequently?

One good solution is using `Swagger` as a source of truth. Still, the front-end team needs help accessing client definitions like TypeScript interfaces inside thier applications. Can the process be more straightforward?

Recently, I made an ASP.NET Core middleware based on [NSwag TypeScriptClientGenerator](https://github.com/RicoSuter/NSwag/wiki/TypeScriptClientGenerator) for sharing more fine-grained data from the back-end side to front-end side, so, front-end team can get more info, update their side based on it or even make sure some changes happened or no.

### [Nuget](https://www.nuget.org/packages/TypeScriptClientGeneratorMiddleware)

[![Open Source Love](https://badges.frapsoft.com/os/mit/mit.svg?v=102)](https://opensource.org/licenses/MIT)
![Nuget](https://img.shields.io/nuget/v/TypeScriptClientGeneratorMiddleware)
![Nuget](https://img.shields.io/nuget/dt/TypeScriptClientGeneratorMiddleware)

```
Install-Package TypeScriptClientGeneratorMiddleware

dotnet add package TypeScriptClientGeneratorMiddleware
```

### Usage

```cs
// SERVICES
builder.Services.AddTypeScriptClientGenerator(options =>
{
    options.Options = new List<TypeScriptClientGeneratorOption>
    {
        // You can set array of endpoints.
        new()
        {
            // The URI you want to see the generated result and share it.
            Endpoint = "my_ts_client",  
            // Absolute URI of Swagger/Open API JSON file.
            SwaggerJsonEndpoint = "https://localhost:7031/swagger/v1/swagger.json", 
            // You can pass your own source code processor to customize final result, If don't you will get full source code which is generated by NSwag.
            // ExtractInterfacesAndEnums is a method provided by the library to extract just interfaces and enums to share with others.
            // At first line always you can see SHA256 hash of the content, after changing, by comparing with the old one you will understand part of the code changed.
            Process = GeneratedSourceProcessor.ExtractInterfacesAndEnums,
            // You can change the content type if you want. default is "text/plain".
            // ContentType = "application/json"
            // You can customize NSwag generator too
            // Settings = ...
        },
        new()
        {
            Endpoint = "petstore_ts_client",
            SwaggerJsonEndpoint = "https://raw.githubusercontent.com/OAI/OpenAPI-Specification/main/examples/v3.0/petstore.json",
            Process = GeneratedSourceProcessor.ExtractInterfacesAndEnums
        }
    };
});

// APP
app.UseTypeScriptClientGenerator();
```

**With Process (ExtractInterfacesAndEnums)**

![Screenshot_1](https://user-images.githubusercontent.com/8418700/224740386-7f03e0f4-59ad-4846-ab4e-8b5c45edbe3d.png)
![Screenshot_2](https://user-images.githubusercontent.com/8418700/224740388-6e9c08c9-de80-485e-84f7-7229dd6f6c16.png)
![image](https://user-images.githubusercontent.com/8418700/224741593-7b521b6d-1803-43ee-b35c-7759c9980bce.png)

**Without Process (Full Source Code)**

![Screenshot_3](https://user-images.githubusercontent.com/8418700/224741163-aa46a289-6485-461e-a5a7-4306d2220f48.png)
![Screenshot_4](https://user-images.githubusercontent.com/8418700/224753400-ec93a0b1-b23b-4070-bb24-2cfd7a0f34f4.png)


