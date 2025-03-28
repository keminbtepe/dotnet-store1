using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;   //models.kategori yazmadık çünkü viewimportsda zaten bu tanımı yaptık

public class KategoriCreateModel
{
    [Display(Name = "Kategori Adı")]
    [Required(ErrorMessage ="Kategori Adı Boş Bırakılamaz.")]
    [StringLength(30)]
    public string KategoriAdi { get; set; } = null!;
    [Display(Name = "Url")]
    [Required(ErrorMessage ="Url Alanı Boş Bırakılamaz.")]
    [StringLength(60)]
    public string Url { get; set; } = null!;
}

