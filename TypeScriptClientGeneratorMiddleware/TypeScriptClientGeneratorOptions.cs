namespace TypeScriptClientGeneratorMiddleware;

public class TypeScriptClientGeneratorOptions
{
    public TypeScriptClientGeneratorOptions()
    {
        Options = new List<TypeScriptClientGeneratorOption>();
    }

    public List<TypeScriptClientGeneratorOption> Options { get; set; }
}