using Framework;

namespace Client.Services;

public class LogsService : object
{
    public LogsService() : base()
    {
        Logs = new List<ViewModels.LogViewModel>();
    }

    protected IList<ViewModels.LogViewModel> Logs { get; }

    public int LogCount
    {
        get
        {
            return Logs.Count;
        }
    }

    public void AddLog(Type type, string? message)
    {
        if (string.IsNullOrWhiteSpace(value: message))
        {
            return;
        }

        // **************************************************
        var stackTrace =
            new System.Diagnostics.StackTrace();

        var methodBase =
            stackTrace.GetFrame(index: 1)?.GetMethod();
        // **************************************************

        message =
            $"{type.Namespace} -> {type.Name} -> {methodBase?.Name}: {message.Fix()}";

        var log = new ViewModels
            .LogViewModel(message: message);

        //Logs.Add(log);
        Logs.Insert
            (index: 0, item: log);
    }

    public IList<ViewModels.LogViewModel> GetLogs()
    {
        return Logs;
    }

    public void ClearLogs()
    {
        Logs.Clear();
    }
}
