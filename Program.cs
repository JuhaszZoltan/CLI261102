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
        Brand = tmp[2],
        Model = tmp[3],
        Price = int.Parse(tmp[4]),
        StockQTY = int.Parse(tmp[5]),
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
var count = products.Count(p => p.Brand == "Cyber");
Console.WriteLine($"cyber gyártó forgalmazott termékeinek száma a készletben {count} db");

//átlagszámítás:
var average = products.Average(p => p.Rating);
Console.WriteLine($"az árukészletben lévő termékek átlagos értékelése: {average:0.00}/5.0");

//szélsőérték:
var maxby = products.MaxBy(p => p.Price);
Console.WriteLine($"legdrágább termék:\n{maxby}");
var minby = products.MinBy(p => p.Rating);
Console.WriteLine($"leggyengébb értékelésű termék:\n{minby}");

// min/max -> delegate selektorában kiválaszotott tulajdonsághoz tartozó megfelelő ÉRTÉKET adja vissza
// minby/maxby -> a selektorban kiválasztott tulajdonságnak megfelelő értékhez tartozó PÉLDÁNY referenciáját adja vissza

//összegzés
var sum = products.Sum(p => p.StockQTY * p.Price);
Console.WriteLine($"teljes árukészlet értéke: {sum:C0}");

//összes ("eldöntés")
var all = products.All(p => p.Rating >= 4);
Console.WriteLine($"{(all ? "igaz" : "nem igaz")} minden termékre, hogy legalább 4es ertékelést elért");

//legelább egy ("eldöntés")
var any = products.Any(p => p.Brand == "Apex" && p.StockQTY > 0);
Console.WriteLine($"{(any ? "van" : "nincs")} Apex termék készleten");

//"kiválasztás"    -       "keresés"
//   first         -    first or default
//   last          -    last or default
//   single        -    single or default

Console.Write("írd be milyen termékmodellt keresel: ");
//string searchString = Console.ReadLine();
string searchString = "fluxuskondenzátor";
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

// 'másolás'
// kiválogatás
// rendezés
// szétválogatás (csoportosítás)

//"másolás"
var select = products.Select(p => $"{p.Brand} {p.Model}");
Console.WriteLine("csak a termékek márkája+megnevezései a többi adat nélkül: ");
foreach (var productName in select) Console.WriteLine($"\t- {productName}");
// distinkció - (ismétlődések szűrése)
var selectDistinct = products.Select(p => p.Category).Distinct();
// distinctby: az a selektor általtal megjelölt tulajdonság szerint minden első az eddigiektő különböző félnyok kollekcióját adja vissza (gyakorlati haszna elég speciális...)
Console.WriteLine("termékkategóriák:");
foreach (var categoryName in selectDistinct) Console.WriteLine($"\t- {categoryName}");

//kiválogatás
var where = products.Where(p => p.StockQTY > 300);
Console.WriteLine("az össze olyan termék, amiből több, mint 300 db van raktáron:");
foreach (var product in where) Console.WriteLine($"{product}\n");

//rendezés
var orderby = products.OrderByDescending(p => p.Price).ThenBy(p => p.Brand);
//orderby -> növekvő
//orderbydescanding -> csökkenő
//thenby, thenbydescending -> ún. 'másodlagos rendezési szempont', csa IOrderedEnumerable interfacet-teljesítőre hívható (tehát order, orderdescending, orderby vagy ordrerbydescending után pl.)

Console.WriteLine("a termékek ár szerint növekvő sorrendben:");
foreach (var product in orderby)
    Console.WriteLine($"\t{product.Brand + " " + product.Model, -30} {product.Price,10:C0}");

var brands = products.Select(p => p.Brand).Distinct().Order();
Console.WriteLine("distinct márkák ABC sorrendben:");
Console.WriteLine(string.Join(", ", brands));

//csoportosítás
var groupby = products.GroupBy(p => p.Brand);
Console.WriteLine("termékek márkánként:");
foreach (var grp in groupby)
{
    Console.WriteLine($"\t{grp.Key} ({grp.Count()} db):");
    foreach (var product in grp)
    {
        Console.WriteLine($"\t\t{product.Model,-24} {product.Price,10:C0}");
    }
}


// mindent egybe....

//termékkategóriánként:
// - termék száma
// - adott termékkategóriában termékeket forgalmazó gyártók listája
// - a kategóriában lévő termékek átlagára
// - kategórián belül a legjobb modell + értékelése
//rendezve termékek száma szerint rendezve

var stat = products.GroupBy(p => p.Category).Select(g => new
{
    CategoryName = g.Key,
    NuberOfProducts = g.Count(),
    Brands = string.Join(", ", g.Select(p => p.Brand).Distinct().Order()),
    AvgCategoryPrice = g.Average(p => p.Price),
    BestModel = $"{g.MaxBy(p => p.Rating).Model} ({g.Max(p => p.Rating):0.0}/5.0)",
}).OrderBy(g => g.NuberOfProducts);
Console.WriteLine("árúkészlet kategóriáinak statisztikája");
foreach (var item in stat)
{
    Console.WriteLine($"{item.CategoryName}\n" +
        $"\ttermékek száma: {item.NuberOfProducts} db\n" +
        $"\térintett forgalmazók: {item.Brands}\n" +
        $"\tátlagár: {item.AvgCategoryPrice:C2}\n" +
        $"\tlegjobb termék: {item.BestModel}");
}

//halmazműveletek: union, intersect, execpt, concat
//"top" vagy "limit" >>> takre(x) / skip(x).take(y)
