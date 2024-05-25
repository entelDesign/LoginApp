using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginApp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Kullanıcı Adınız")]
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "Ad Soyad")]
    public string? AdSoyad { get; set; }


    [Required(ErrorMessage = "Parola alanı boş geçilemez")]
    [StringLength(10, ErrorMessage = "{0} alanı Mak. {1}, minimum {2} karakter girilmelidir.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Parola alanı boş geçilemez")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Parolanız eşeleşmiyor!")]
    [Display(Name = "Parola Doğrula")]
    public string? ConfirmPassword { get; set; }
}