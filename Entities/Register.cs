using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Register : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçerli bir Türkiye telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicture { get; set; }
    }
}
