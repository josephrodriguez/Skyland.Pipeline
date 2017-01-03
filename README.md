# Skyland Pipeline

Skyland Pipeline Is a C # implementation of the Chain of Responsibility pattern.

## Build Status

[![AppVeyor build status](https://img.shields.io/appveyor/ci/josephrodriguez/skyland-pipeline/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/josephrodriguez/skyland-pipeline)


## Nuget  [![NuGet Status](http://img.shields.io/nuget/v/Skyland.Pipeline.svg?style=flat)](https://www.nuget.org/packages/Skyland.Pipeline/)

    PM> Install-Package Skyland.Pipeline
	
## Getting started

Example of pipeline that receive a path of file and return the content of file parsed to MockClass instance.

```cs
class Program
{
    static void Main(string[] args)
    {
        var pipeline = new Pipeline<string, MockClass>.Builder()
            .Register<string, string>(
                stage =>
                    stage
                        .Filter(filepath => !string.IsNullOrEmpty(filepath))
                        .Filter(File.Exists)
                        .Job(File.ReadAllText)
                        .Handler(content => Console.WriteLine("Stage #1 handler: Readed content {0}.", content)))
            .Register<string, MockClass>(
                stage =>
                    stage
                        .Job<XmlSerializerJob>()
                        .Handler(mock => Console.WriteLine("Stage #2: Parsed MockClas instance.")))
            .OnError((sender, exception) => Console.WriteLine("Handled exception: {0} on component of type {1}.", exception.ToString(), sender.GetType()))
            .Build();

            var output = pipeline.Execute("default.xml");

            Console.WriteLine("Output completed: {0}.", output.IsCompleted);
            Console.WriteLine("Output result: {0}.", output.Result);

            Console.ReadLine();
    }
}

class XmlSerializerJob : IJobComponent<string, MockClass>
{
    public MockClass Execute(string content)
    {
        var serializer = new XmlSerializer(typeof(MockClass));

        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        using (var reader = new XmlTextReader(stream))
        {
            var obj = serializer.Deserialize(reader);
            return obj as MockClass;
        }
    }
}
```