using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.User
{
    public class GetUserModel
    {
        public string UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email Id is Invalid!")]
        public string EmailId { get; set; }
    }
}
