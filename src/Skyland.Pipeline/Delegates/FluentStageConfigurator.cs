namespace Skyland.Pipeline.Delegates
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public delegate void FluentStageConfigurator<TInput, TOutput>(FluentStageConfiguration<TInput, TOutput> configuration);
}
