namespace Checkpoint.Core.Models.ViewModels
{
    public class DefaultResponseViewModel
    {
        public DefaultResponseViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
