List<Product> products = [];

#region file beolvasása
#nullable disable
const string FilePath = "..\\..\\..\\data\\arukeszlet.csv";
using StreamReader sr = new(FilePath);
_ = sr.ReadLine();
while (!sr.EndOfStream)
{
    var tmp = sr.ReadLine().Split(',');
    products.Add(new()
    {
        Id = int.Parse(tmp[0]),
        Category = tmp[1],
        Manufacturer = tmp[2],
        Model = tmp[3],
        Price = int.Parse(tmp[4]),
        Stock = int.Parse(tmp[5]),
        Rating = float.Parse(tmp[6].Replace('.', ','))
    });
}
#endregion

// szélsőérték meghatározás tétele ("min", "max")
// megszámlálás
// összegzés (sorozatszámítás tétele) >>> átlagszámítás

// eldöntés
// kiválasztás
// lineáris keresés


//-------------------------------------------
// kiválogatás
// "szétválogatás" (csoportosítás) :::TODO:::
// "rendezés"

// ----
// unió/metszet/különbség

//-------------------------

//megszámlálás
var count = products.Count(p => p.Manufacturer == "Cyber");
Console.WriteLine($"cyber gyártó forgalmazott termékeinek száma a készletben {count} db");

//átlagszámítás:
var average = products.Average(p => p.Rating);
Console.WriteLine($"az árukészletben lévő termékek átlagos értékelése: {average:0.00}/5.0");

//szélsőérték:
var maxby = products.MaxBy(p => p.Price);
Console.WriteLine($"legdrágább termék:\n{maxby}");
var minby = products.MinBy(p => p.Rating);
Console.WriteLine($"leggyengébb értékelésű termék:\n{minby}");

//összegzés
var sum = products.Sum(p => p.Stock * p.Price);
Console.WriteLine($"teljes árukészlet értéke: {sum:C0}");

//összes ("eldöntés")
var all = products.All(p => p.Rating >= 4);
Console.WriteLine($"{(all ? "igaz" : "nem igaz")} minden termékre, hogy legalább 4es ertékelést elért");

//legelább egy ("eldöntés")
var any = products.Any(p => p.Manufacturer == "Apex" && p.Stock > 0);
Console.WriteLine($"{(any ? "van" : "nincs")} Apex termék készleten");

//"kiválasztás"    -       "keresés"
//   first         -    first or default
//   last          -    last or default
//   single        -    single or default

Console.Write("írd be milyen termékmodellt keresel: ");
string searchString = Console.ReadLine();
var firstOrDefault = products.FirstOrDefault(p => p.Model.Contains(searchString));

Console.WriteLine(
    firstOrDefault is null 
    ? "\tnincs ilyen modell" 
    : $"a termékek közötti első ilyen modell:\n{firstOrDefault}");

//first/last:
// HA VAN a pred.-nek megfelelő eledmény,
// AKKOR visszaadja a RENDRE az első/utolsó elemet
// HA NINCS, akkor "Sequence contains no matching element" ex-et dob!

//first or default / last or default:
// HA VAN a pred.-nek megfelelő eledmény,
// AKKOR visszaadja a RENDRE az első/utolsó elemet
// HA NINCS akkor a kollekció elemeinek típusától függő ún. 'default' értékkel tér vissza:
// ha a típus class (és nekünk az lesz majdnem mindig...) akkor ez 'null'
// ha struct (ez ritkább lesz) akkor "zéró".

//TODO: ref/value

// single:
// HA EGYETLEN EGY a pred.-nek megfelelő eredmény van
// visszaadja azt az EGYETLEN EGY elemet
// HA TÖBB a pred-nek megfelelő eredmény van:
// akkor "Sequence contains more than one matching element" ex-et dob!
// HA NINCS, akkor "Sequence contains no matching element" ex-et dob!

//single or default: 
// HA EGYETLEN EGY a pred.-nek megfelelő eredmény van
// visszaadja azt az EGYETLEN EGY elemet
// HA TÖBB a pred-nek megfelelő eredmény van:
// akkor "Sequence contains more than one matching element" ex-et dob!
// ha a típus class (és nekünk az lesz majdnem mindig...) akkor ez 'null'
// ha struct (ez ritkább lesz) akkor "zéró".


// kiválogatás >>> WHERE
// rendezés >>> ORDER BY (orderByDescanding ---- order, orderDescanding)
// szétválogatás >>> GROUP BY

var where = products.Where(p => p.Manufacturer == "Apex");
Console.WriteLine("Apex termék:");
foreach (var p in where) Console.WriteLine($"\t{p.Model,-20} {p.Price,10:C0}");

var orderby = products.OrderBy(p => p.Price);
Console.WriteLine("összes termék ár szerint növekvő sorrendben: ");
Console.WriteLine($"\t{"gyártó", -10} {"termék", -20} {"ár",10}");
Console.WriteLine("\t------------------------------------------------");
foreach (var p in orderby) Console.WriteLine($"\t{p.Manufacturer,-10} {p.Model,-25} {p.Price,10:C0}");

var gropby = products.GroupBy(p => p.Manufacturer);
Console.WriteLine("termékek gyártónként:");
foreach (var group in gropby)
{
    Console.WriteLine($"\t{group.Key}:");
    foreach (var p in group)
    {
        Console.WriteLine($"\t\t{p.Model,-25} {p.Price,10:C0}");
    }
}

var stat = products
    .GroupBy(p => p.Manufacturer)
    .Select(g => new
    {
        Manufacturer = g.Key,
        ProductCount = g.Count(),
        BestRating = g.Max(p => p.Rating),
        StockValue = g.Sum(p => p.Stock * p.Price)
    })
    .OrderByDescending(s => s.StockValue);

Console.WriteLine("teljes gyártói statisztika:");
foreach (var item in stat)
{
    Console.WriteLine($"\t{item.Manufacturer}: ");
    Console.WriteLine($"\t\tforgalmazott termékek száma: {item.ProductCount} db");
    Console.WriteLine($"\t\tlegjobb értékelés: {item.BestRating}/5.00");
    Console.WriteLine($"\t\tárúkészéet értéke: {item.StockValue:C0}");
}

var whereminby = products.Where(p => p.Manufacturer == "Cyber").MinBy(p => p.Price);
Console.WriteLine($"legolcsóbb Cyber termék:\n{whereminby}");

var orderdistinct = products.Select(p => p.Manufacturer).Distinct().Order();
Console.WriteLine("ABC rendben a gyártók nevei:");
foreach (var manufacturer in orderdistinct) Console.WriteLine($"\t- {manufacturer}");

//halmazműveletek: union, iintersect, execpt, concat