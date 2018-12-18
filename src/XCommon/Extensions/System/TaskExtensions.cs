using XCommon.Application.Executes;

namespace System.Threading.Tasks
{
	public static class TaskExtensions
	{
		public static async Task<TResult> MapAsync<TSource, TResult>(this Task<TSource> source, Func<TSource, Task<TResult>> fn) => await fn(await source);

		public static async Task<TResult> MapAsync<TSource, TResult>(this Task<TSource> source, Func<TSource, Task<TResult>> fn, Execute execute)
		{
			if (execute != null && execute.HasErro)
			{
				return await Task.FromResult<TResult>(default(TResult));
			}

			return await fn(await source);
		}

		public static async Task<TResult> Map<TSource, TResult>(this Task<TSource> source, Func<TSource, TResult> fn) => fn(await source);

		public static async Task<TResult> Map<TSource, TResult>(this Task<TSource> source, Func<TSource, TResult> fn, Execute execute)
		{
			if (execute != null && execute.HasErro)
			{
				return default(TResult);
			}

			return fn(await source);
		}
	}
}
