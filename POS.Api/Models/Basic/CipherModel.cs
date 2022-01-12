using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.Basic
{
    public class CipherModel
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Text { get; set; }

    }
}
