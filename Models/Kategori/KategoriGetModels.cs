namespace dotnet_store.Models;

public class KategoriGetModels
{
    public int Id { get; set; }
    public string KategoriAdi { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int UrunSayisi { get; set; }
}

