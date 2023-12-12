namespace ExampleBlazor.Classes;
public struct TaskWithData<TData, TResult>//: INotifyCompletion
{
    private readonly TData data;
    private readonly Task<TResult> taskToExecute;

    public TaskWithData(TData data, Task<TResult> taskToExecute)
    {
        ArgumentNullException.ThrowIfNull(taskToExecute);
        this.data = data;
        this.taskToExecute = taskToExecute;
    }
    #region add this to have Task.WhenAll
    public async Task<(TData data, TResult res)> GetTask()
    {
        var res = await taskToExecute;
        return (data, res);
    }
    public static explicit operator Task<(TData data, TResult res)>(TaskWithData<TData, TResult> b)
        => b.GetTask();

    #endregion
}
public static class Extensions
{
    #region transform to task
    public static Task<(TData data, TResult res)> AddData<TData, TResult>(this Task<TResult> taskToExecute, TData data)
    {
        var td = new TaskWithData<TData, TResult>(data, taskToExecute);
        return td.GetTask();
    }
    public static Task<(TData data, TResult res)>[] SelectTaskWithData<TData, TResult>(this TData[] arr, Func<TData, Task<TResult>> func)
    {
        return arr.Select(it => func(it).AddData(it)).ToArray();
    }
    #endregion
}
