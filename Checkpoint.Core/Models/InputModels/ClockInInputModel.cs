using System.ComponentModel.DataAnnotations;

namespace Checkpoint.Core.Models.InputModels
{
    public class ClockInInputModel
    {
        /// <summary>
        /// A character value being A for Arrival or E for Exit.
        /// </summary>
        [Required]
        public char CheckpointType { get; set; }
    }
}
