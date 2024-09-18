namespace AsyncPipeline;

public class AddStep(int addend) : IAsyncPipelineStep<int, int>
{
    public async IAsyncEnumerable<int> ProcessAsync(IAsyncEnumerable<int> input)
    {
        await foreach (var item in input)
        {
            yield return item + addend;
        }
    }
}

public class MultiplyStep(int factor) : IAsyncPipelineStep<int, int>
{
    public async IAsyncEnumerable<int> ProcessAsync(IAsyncEnumerable<int> input)
    {
        await foreach (var item in input)
        {
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
    public async IAsyncEnumerable<string> ProcessAsync(IAsyncEnumerable<string> input)
    {
        await foreach (var item in input)
        {
            yield return new string(item.Reverse().ToArray());
        }
    }
}

public class ToUpperStep : IAsyncPipelineStep<string, string>
{
    public async IAsyncEnumerable<string> ProcessAsync(IAsyncEnumerable<string> input)
    {
        await foreach (var item in input)
        {
            yield return item.ToUpper();
        }
    }
}