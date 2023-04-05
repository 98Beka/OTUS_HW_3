using System.ComponentModel.DataAnnotations;

namespace HW_3.Entities {
    public class Customer {
        public long Id { get; init; }

        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }

    }
}
