using AsyncPipeline;

var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(250);

var numberProcessingPipeline = AsyncPipeline<int, string>.Create()
    .AddStep(new MultiplyStep(2))
    .AddStep(new AddStep(10))
    .AddStep(new MultiplyStep(2))
    .AddStep(new AddStep(5));
var numbersProcessingTask = Task.Run(async () =>
{
    await foreach (var result in numberProcessingPipeline.ExecuteAsync(GetNumbersAsync(), cancellationTokenSource.Token))
        Console.WriteLine(result);
}, cancellationTokenSource.Token);

var textProcessingPipeline = AsyncPipeline<string, string>.Create()
    .AddStep(new ToUpperStep())
    .AddStep(new ReverseStep());
var textsProcessingTask = Task.Run(async () =>
{
    await foreach (var result in textProcessingPipeline.ExecuteAsync(GetTextsAsync(), cancellationTokenSource.Token))
        Console.WriteLine(result);
}, cancellationTokenSource.Token);

await Task.WhenAll(numbersProcessingTask, textsProcessingTask);

return;

async IAsyncEnumerable<int> GetNumbersAsync()
{
    var random = new Random();
    for (var i = 1; i <= 4; i++)
    {
        yield return i;
        await Task.Delay(random.Next(0, 25));
    }
}

async IAsyncEnumerable<string> GetTextsAsync()
{
    var random = new Random();
    yield return "olleh";
    await Task.Delay(random.Next(0, 15));
    yield return "dlrow";
    await Task.Delay(random.Next(0, 15));
    yield return ".";
    await Task.Delay(random.Next(0, 15));
}