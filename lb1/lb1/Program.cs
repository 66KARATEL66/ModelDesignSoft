using lb1.Data;
using lb1.Models;
using lb1.Services;

var seriesDb = new DBItem<AnimeSeries>();
var figurineDb = new DBItem<Figurine>();
var specificationDb = new DBItem<FigurineSpecification>();
var tagDb = new DBItem<CollectionTag>();
var figurineTagDb = new DBItem<FigurineTag>();

var repository = new AnimeFigurineRepository(
    seriesDb,
    figurineDb,
    tagDb,
    figurineTagDb);

var naruto = new AnimeSeries
{
    Id = 1,
    Title = "Naruto",
    Studio = "Pierrot"
};

var onePiece = new AnimeSeries
{
    Id = 2,
    Title = "One Piece",
    Studio = "Toei Animation"
};

var narutoFigurine = new Figurine
{
    Id = 1,
    Name = "Naruto Uzumaki Sage Mode",
    Price = 4500,
    AnimeSeriesId = naruto.Id
};

var luffyFigurine = new Figurine
{
    Id = 2,
    Name = "Monkey D. Luffy Gear Fifth",
    Price = 6000,
    AnimeSeriesId = onePiece.Id
};

var zoroFigurine = new Figurine
{
    Id = 3,
    Name = "Roronoa Zoro Wano",
    Price = 5000,
    AnimeSeriesId = onePiece.Id
};

seriesDb.Add(naruto);
seriesDb.Add(onePiece);
figurineDb.Add(narutoFigurine);
figurineDb.Add(luffyFigurine);
figurineDb.Add(zoroFigurine);
naruto.Figurines.Add(narutoFigurine);
onePiece.Figurines.Add(luffyFigurine);
onePiece.Figurines.Add(zoroFigurine);
narutoFigurine.AnimeSeries = naruto;
luffyFigurine.AnimeSeries = onePiece;
zoroFigurine.AnimeSeries = onePiece;

var narutoSpecification = new FigurineSpecification
{
    Id = 1,
    FigurineId = narutoFigurine.Id,
    HeightCm = 18,
    Material = "PVC",
    LimitedEdition = false
};
var luffySpecification = new FigurineSpecification
{
    Id = 2,
    FigurineId = luffyFigurine.Id,
    HeightCm = 22,
    Material = "PVC and ABS",
    LimitedEdition = true
};
var zoroSpecification = new FigurineSpecification
{
    Id = 3,
    FigurineId = zoroFigurine.Id,
    HeightCm = 20,
    Material = "PVC",
    LimitedEdition = false
};

specificationDb.Add(narutoSpecification);
specificationDb.Add(luffySpecification);
specificationDb.Add(zoroSpecification);
narutoFigurine.Specification = narutoSpecification;
luffyFigurine.Specification = luffySpecification;
zoroFigurine.Specification = zoroSpecification;

var protagonistTag = new CollectionTag { Id = 1, Name = "Protagonist" };
var limitedTag = new CollectionTag { Id = 2, Name = "Limited Edition" };
var swordsmanTag = new CollectionTag { Id = 3, Name = "Swordsman" };

tagDb.Add(protagonistTag);
tagDb.Add(limitedTag);
tagDb.Add(swordsmanTag);

AddTag(narutoFigurine, protagonistTag);
AddTag(luffyFigurine, protagonistTag);
AddTag(luffyFigurine, limitedTag);
AddTag(zoroFigurine, swordsmanTag);

var service = new AnimeFigurineService(repository);
service.DisplayStoreOverview();
service.DisplaySeriesFigurines(onePiece.Id);
service.DisplayFigurineDetails(luffyFigurine.Id);
service.DisplayTagFigurines(protagonistTag.Id);

void AddTag(Figurine figurine, CollectionTag tag)
{
    figurineTagDb.Add(new FigurineTag
    {
        FigurineId = figurine.Id,
        TagId = tag.Id
    });
    figurine.Tags.Add(tag);
    tag.Figurines.Add(figurine);
}
