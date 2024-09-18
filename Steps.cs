using System.Runtime.CompilerServices;

namespace AsyncPipeline;

public class AddStep(int addend) : IAsyncPipelineStep<int, int>
{
    public async IAsyncEnumerable<int> ProcessAsync(IAsyncEnumerable<int> input, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in input.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            yield return item + addend;
        }
    }
}

public class MultiplyStep(int factor) : IAsyncPipelineStep<int, int>
{
    public async IAsyncEnumerable<int> ProcessAsync(IAsyncEnumerable<int> input, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in input.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (item > 10)
            {
                yield return item;
                continue;
            }

            yield return item * factor;
        }
    }
}

public class ReverseStep : IAsyncPipelineStep<string, string>
{
    public async IAsyncEnumerable<string> ProcessAsync(IAsyncEnumerable<string> input, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in input.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            yield return new string(item.Reverse().ToArray());
        }
    }
}

public class ToUpperStep : IAsyncPipelineStep<string, string>
{
    public async IAsyncEnumerable<string> ProcessAsync(IAsyncEnumerable<string> input, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in input.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            yield return item.ToUpper();
        }
    }
}