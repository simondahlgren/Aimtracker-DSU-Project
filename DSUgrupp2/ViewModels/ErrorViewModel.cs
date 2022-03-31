namespace DSUgrupp2.Models
{
    public class ErrorViewModel
    { 
    /// <summary>
    /// Model for handeling Errors.
    /// </summary>
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}