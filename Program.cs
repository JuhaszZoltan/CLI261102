List<Product> products = [];

#region file beolvasása
const string FilePath = "..\\..\\..\\data\\arukeszlet.csv";
using StreamReader sr = new(FilePath);
_ = sr.ReadLine();
while (!sr.EndOfStream)
{
    var t = sr.ReadLine().Split(',');
    products.Add(new()
    {
        Id = int.Parse(t[0]),
        Category = t[1],
        Manufacturer = t[2],
        Model = t[3],
        Price = int.Parse(t[4]),
        Stock = int.Parse(t[5]),
        Rating = float.Parse(t[6].Replace('.', ','))
    });
}
#endregion