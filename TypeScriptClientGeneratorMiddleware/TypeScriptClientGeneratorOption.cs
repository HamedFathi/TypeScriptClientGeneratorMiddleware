﻿using NSwag.CodeGeneration.TypeScript;

namespace TypeScriptClientGeneratorMiddleware;

public class TypeScriptClientGeneratorOption
{
    public TypeScriptClientGeneratorSettings? Settings { get; set; }
    public string Endpoint { get; set; } = null!;
    public string SwaggerJsonEndpoint { get; set; } = null!;
    public Func<string, string>? Process { get; set; }
}