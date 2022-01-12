using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.Authenticate
{
    public class RegisterModel:LoginModel
    {

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Confirm Password Required!")]
        [Compare("Password",ErrorMessage ="Password mismatch!")]
        public string RetypePassword { get; set; }
    }
}
