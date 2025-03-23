using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;   //models.kategori yazmadık çünkü viewimportsda zaten bu tanımı yaptık

public class KategoriCreateModel
{
    [Display(Name = "Kategori Adı")]
    public string KategoriAdi { get; set; } = null!;
    [Display(Name = "Url")]
    public string Url { get; set; } = null!;
}

