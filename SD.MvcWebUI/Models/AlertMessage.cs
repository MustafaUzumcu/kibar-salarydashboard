namespace SD.MvcWebUI.Models
{
    public class AlertMessage
    {
        public const string TempDataKey = "TempDataAlertMessage";

        public string AlertType { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public bool Dismissible { get; set; }
    }

    public static class AlertType
    {
        public const string Success = "success";
        public const string Error = "error";
        public const string Warning = "warning";
        public const string Information = "info";
    }
}
