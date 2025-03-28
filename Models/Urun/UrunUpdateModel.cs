using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;    //models.urun yazmadık çünkü urun klasörü dısında da kullancagız

public class UrunUpdateModel
{
    public int Id { get; set; }
    [Display(Name = "Ürün Adı")]
    public string UrunAdi { get; set; } = null!;
    [Display(Name = "Ürün Fiyat")]
    public double Fiyat { get; set; }
    [Display(Name = "Ürün Resmi")]
    public string? ResimAdi { get; set; }

    public IFormFile ResimDosyasi { get; set; }
    public string? Aciklama { get; set; }
    public bool Aktif { get; set; }
    public bool Anasayfa { get; set; }


    #region Kategori Include
    public int KategoriId { get; set; }
    #endregion
}

