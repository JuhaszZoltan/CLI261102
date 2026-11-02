class Product
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public float Rating { get; set; }

    public override string ToString() =>
        $"\tAzonosító: {Id}\n" +
        $"\tTermékkategória: {Category}\n" +
        $"\tGyártó: {Manufacturer}\n" +
        $"\tModell: {Model}\n" +
        $"\tÁr: {Price:C0}\n" +
        $"\tKészleten: {Stock} db\n" +
        $"\tÉrtékelés: {Rating:0.0}/5,0";
}