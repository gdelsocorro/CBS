namespace CBS.Shared.Services;

public class NotificationStateService
{
    private readonly List<AppNotification> _items = new();

    public IReadOnlyList<AppNotification> Items => _items;
    public int UnreadCount => _items.Count(n => !n.IsRead);

    public event Action? OnChanged;

    public void Push(string title, string message, AppNotificationSeverity severity = AppNotificationSeverity.Info)
    {
        _items.Insert(0, new AppNotification
        {
            Title    = title,
            Message  = message,
            Severity = severity,
            Time     = DateTime.Now
        });
        if (_items.Count > 60) _items.RemoveAt(_items.Count - 1);
        OnChanged?.Invoke();
    }

    public void MarkRead(Guid id)
    {
        var n = _items.FirstOrDefault(x => x.Id == id);
        if (n is null) return;
        n.IsRead = true;
        OnChanged?.Invoke();
    }

    public void MarkAllRead()
    {
        _items.ForEach(n => n.IsRead = true);
        OnChanged?.Invoke();
    }

    public void Remove(Guid id)
    {
        _items.RemoveAll(n => n.Id == id);
        OnChanged?.Invoke();
    }

    public void Clear()
    {
        _items.Clear();
        OnChanged?.Invoke();
    }
}

public class AppNotification
{
    public Guid   Id       { get; init; } = Guid.NewGuid();
    public string Title    { get; set; }  = string.Empty;
    public string Message  { get; set; }  = string.Empty;
    public AppNotificationSeverity Severity { get; set; }
    public DateTime Time   { get; set; }  = DateTime.Now;
    public bool   IsRead   { get; set; }
}

public enum AppNotificationSeverity { Info, Success, Warning, Error }
