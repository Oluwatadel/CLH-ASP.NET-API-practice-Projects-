using System.ComponentModel.DataAnnotations;

namespace Api_Ass.Model.RequestModel
{
    public class LoginRequestModel
    {
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
