# Skyland Pipeline

Skyland Pipeline Is a C # implementation of the Chain of Responsibility pattern.

## Build Status

[![AppVeyor build status](https://img.shields.io/appveyor/ci/josephrodriguez/skyland-pipeline/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/josephrodriguez/skyland-pipeline)


## Nuget  [![NuGet Status](http://img.shields.io/nuget/v/Skyland.Pipeline.svg?style=flat)](https://www.nuget.org/packages/Skyland.Pipeline/)

    PM> Install-Package Skyland.Pipeline
	
## Getting started

Declare inline pipeline Which receives a string as input and returns an integer.

```cs
class Program
{
    static void Main(string[] args)
    {
        var pipeline = new PipelineBuilder<string, int>()
            .Register(
                new Stage<string, string>(i => i.Trim())
                    .WithFilter(i => !string.IsNullOrEmpty(i))
                    .WithHandler(i => Console.WriteLine("Hanndler for trimmed string: {0}.", i)))
            .Register(
                new Stage<string, int>(int.Parse)
                    .WithFilter(new SecondFilter()))
            .Build();

        var output = pipeline.Execute("  5768 ");

        Console.WriteLine("Output status: {0}.", output.Status);
        Console.WriteLine("Output result: {0}.", output.Result);

        Console.ReadLine();
    }
}

class SecondFilter : IPipelineFilter<string>
{
    private readonly Regex _regex = new Regex("\\d+");
 
    public bool Filter(string input)
    {
        return _regex.IsMatch(input);
    }
}
```

Output:

    Hanndler for trimmed string: 5768.
    Output status: Completed.
    Output result: 5768.
