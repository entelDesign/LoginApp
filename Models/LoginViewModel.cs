using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginApp.Models;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Kullanıcı adınız")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Parola alanı boş geçilemez")]
    [StringLength(10, ErrorMessage = "{0} alanı Mak. {1}, minimum {2} karakter girilmelidir.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string? Password { get; set; }
}