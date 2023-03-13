For a long time, I was looking for finding a way to connect Back-end and Front-end teams together when they are working on same APIs and time ti time update them.

In this use case, Back-end team provide bunch of APIs then Front-end team should follow them to call APIs correctly but how possible to get correct info from back-end side correctly or update front-end client after some changes?

One good solution is using `Open API or Swagger` as a source of truth but you don't have access to client definitions like TypeScript interfaces, Is it possible to make the process a bit easier?

Recently, I made an ASP.NET Core middleware based on [NSwag TypeScriptClientGenerator](https://github.com/RicoSuter/NSwag/wiki/TypeScriptClientGenerator).

## Installation


## Usage

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
            Process = GeneratedSourceProcessor.ExtractInterfacesAndEnums 
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
![Screenshot_4](https://user-images.githubusercontent.com/8418700/224741172-d8512694-6737-4182-bf95-8e0418d8ca15.png)



