using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APBD_Cw5.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        [Required]
        public string OrganizerName { get; set; } = string.Empty;

        [Required]
        public string Topic { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "EndTime must be later than StartTime",
                    new[] { nameof(EndTime) });
            }
        }
    }
}