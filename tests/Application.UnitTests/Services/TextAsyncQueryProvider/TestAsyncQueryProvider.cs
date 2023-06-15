using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.UnitTests.Services.TextAsyncQueryProvider;

public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    internal TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        object? value = _inner.Execute(expression);
        return value is not null ? value : throw new ArgumentNullException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(TResult).GetGenericArguments()[0];
        var executeAsyncMethod = GetType()
            .GetMethod(nameof(ExecuteAsyncInternal), BindingFlags.Instance | BindingFlags.NonPublic)
            ?.MakeGenericMethod(resultType);
        object? returnValue = executeAsyncMethod?.Invoke(this, new object[] { expression, cancellationToken });
        return (TResult)(returnValue is not null ? returnValue : throw new ArgumentNullException());
    }

    private async Task<TResult> ExecuteAsyncInternal<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        var result = _inner.Execute<TResult>(expression);
        return await Task.FromResult(result);
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
    {
        return new TestAsyncEnumerable<TResult>(expression);
    }
}
