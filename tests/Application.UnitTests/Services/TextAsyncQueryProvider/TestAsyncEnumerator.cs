namespace Application.UnitTests.Services.TextAsyncQueryProvider;

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public T Current => _inner.Current;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public async ValueTask DisposeAsync()
    {
        _inner.Dispose();
        await Task.CompletedTask;
    }
}
