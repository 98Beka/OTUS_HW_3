using HW_3.Abstractions;

namespace HW_3.Entities {
    public class User : BaseEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
