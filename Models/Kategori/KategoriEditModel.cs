using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;   //models.kategori yazmadık çünkü viewimportsda zaten bu tanımı yaptık

public class KategoriEditModel
{
    public int Id { get; set; }
    [Display(Name = "Kategori Adı Güncelle")]
    [Required(ErrorMessage ="Ürün Adı Girmelisiniz")] //eror mesajı
    [StringLength(30)] //eror mesajı
    public string KategoriAdi { get; set; } = null!;
    [Display(Name = "Url Güncelle")]
    [Required(ErrorMessage ="URL Girmelisiniz")]
    [StringLength(60)]
    public string Url { get; set; } = null!;
}

