namespace AsyncPipeline;

/// <summary>
/// Represents a step in a pipeline that processes a sequence of items asynchronously.
/// </summary>
public interface IAsyncPipelineStep<in TInput, out TOutput>
{
    IAsyncEnumerable<TOutput> ProcessAsync(IAsyncEnumerable<TInput> input);
}