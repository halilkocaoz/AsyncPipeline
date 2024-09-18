namespace AsyncPipeline;

/// <summary>
/// Represents a pipeline of asynchronous steps that process a sequence of items.
/// </summary>
public class AsyncPipeline<TInput, TOutput>
{
    private readonly Func<IAsyncEnumerable<TInput>, IAsyncEnumerable<TOutput>> _pipelineFunc;

    private AsyncPipeline(Func<IAsyncEnumerable<TInput>, IAsyncEnumerable<TOutput>> pipelineFunc)
    {
        _pipelineFunc = pipelineFunc;
    }

    public static AsyncPipeline<TInput, TInput> Create()
    {
        return new AsyncPipeline<TInput, TInput>(input => input);
    }

    public AsyncPipeline<TInput, TNewOutput> AddStep<TNewOutput>(IAsyncPipelineStep<TOutput, TNewOutput> step)
    {
        return new AsyncPipeline<TInput, TNewOutput>(input => step.ProcessAsync(_pipelineFunc(input)));
    }

    public IAsyncEnumerable<TOutput> ExecuteAsync(IAsyncEnumerable<TInput> input)
    {
        return _pipelineFunc(input);
    }
}