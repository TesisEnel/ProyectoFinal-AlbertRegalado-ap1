using Radzen;

public static class Extension
{
    public static void ShowNotification(this NotificationService notifier, string mensaje, NotificationSeverity severity = NotificationSeverity.Success)
    {
        var message = new NotificationMessage
        {
            Severity = severity,
            Summary = mensaje
        };

        notifier.Notify(message);
    }
}
