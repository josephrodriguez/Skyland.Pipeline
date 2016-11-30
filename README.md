# Skyland Pipeline

Skyland Pipeline Is a C # implementation of the Chain of Responsibility pattern.

## Build Status

[![AppVeyor build status](https://img.shields.io/appveyor/ci/josephrodriguez/skyland-pipeline/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/josephrodriguez/skyland-pipeline)


## Nuget  [![NuGet Status](http://img.shields.io/nuget/v/Skyland.Pipeline.svg?style=flat)](https://www.nuget.org/packages/Skyland.Pipeline/)

    PM> Install-Package Skyland.Pipeline
	
## Getting started

Declare inline pipeline Which receives a string as input and returns an integer.

```cs
var pipeline = new PipelineBuilder<string, int>()
	.Register<string, string>(s => s.Trim())
    .Register<string, int>(int.Parse)
    .Register<int, int>(i => i%2)
    .OnError((sender, exception) => Console.WriteLine(exception))
    .Build();

int result = pipeline.Execute(" 123  ");
```