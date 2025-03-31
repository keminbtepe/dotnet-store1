﻿namespace dotnet_store.Models;

public class SliderGetModel
{
    public int Id { get; set; }

    public string Resim { get; set; } = null!;

    public string? Baslik { get; set; }

    public int Index { get; set; }
    public bool Aktif { get; set; }
}

