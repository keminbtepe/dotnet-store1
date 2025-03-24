using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;   //models.kategori yazmadık çünkü viewimportsda zaten bu tanımı yaptık

public class KategoriEditModel
{
    public int Id { get; set; }
    [Display(Name = "Kategori Adı Güncelle")]
    public string KategoriAdi { get; set; } = null!;
    [Display(Name = "Url Güncelle")]
    public string Url { get; set; } = null!;
}

