using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models.Account;

public class AccountCreateModel
{

    [Required]
    [Display(Name = "Ad Soyad")]
    //[RegularExpression("^[a-Za-Z0-9]*$", ErrorMessage = "Sadece Sayı Ve Harf Giriniz")]
    public string AdSoyad { get; set; } = null!;

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor")]
    public string ConfirmPassword { get; set; } = null!;

}

