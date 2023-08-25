using System.ComponentModel.DataAnnotations;

namespace Checkpoint.Core.Models.InputModels
{
    public class ConfirmEmailInputModel
    {   
        /// <summary>
        /// Confirmation code received by email with 6 digits, which may contain capital letters and numbers
        /// </summary>
        [Required]
        public string ConfirmationCode { get; set; }
    }
}
