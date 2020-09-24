using System.ComponentModel.DataAnnotations;

namespace OffShoreAspNetBoilerplate.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        [StringLength(100)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [StringLength(100)]
        //[DisableAuditing]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }
}
