namespace dotnet_store.Models;

public class SliderCreateModel
{
    public IFormFile Resim { get; set; } = null!;

    public string Aciklama { get; set; }
    public string? Baslik { get; set; }

    public int Index { get; set; }
    public bool Aktif { get; set; }
}

