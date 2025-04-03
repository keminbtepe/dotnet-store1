namespace dotnet_store.Models
{
    public class AdminIndexViewModel
    {
        public List<Urun> Urunler { get; set; }

        public List<Kategori> Kategoriler { get; set; }

        public string  Mesaj { get; set; }
    }
}
