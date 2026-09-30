using lb1.Data;

namespace lb1.Services;

public class AnimeFigurineService
{
    private readonly AnimeFigurineRepository _repository;

    public AnimeFigurineService(AnimeFigurineRepository repository)
    {
        _repository = repository;
    }

    public void DisplayStoreOverview()
    {
        Console.WriteLine("Anime Figurine Store");
        Console.WriteLine($"Anime series: {_repository.GetAllSeries().Count}");
        Console.WriteLine($"Figurines: {_repository.GetAllFigurines().Count}");
        Console.WriteLine($"Collection tags: {_repository.GetAllTags().Count}");
        Console.WriteLine();
    }

    public void DisplaySeriesFigurines(int seriesId)
    {
        var series = _repository.GetSeriesById(seriesId);
        if (series is null)
        {
            Console.WriteLine("Anime series not found.");
            return;
        }

        Console.WriteLine($"{series.Title} figurines (1:N):");
        foreach (var figurine in _repository.GetFigurinesBySeriesId(seriesId))
        {
            Console.WriteLine($"- {figurine.Name} ({figurine.Price} uah)");
        }

        Console.WriteLine();
    }

    public void DisplayFigurineDetails(int figurineId)
    {
        var figurine = _repository.GetFigurineById(figurineId);
        if (figurine is null)
        {
            Console.WriteLine("Figurine not found.");
            return;
        }

        var specification = figurine.Specification;
        Console.WriteLine($"{figurine.Name} details (1:1):");
        Console.WriteLine($"- Height: {specification?.HeightCm} cm");
        Console.WriteLine($"- Material: {specification?.Material}");
        Console.WriteLine($"- Limited edition: {specification?.LimitedEdition}");
        Console.WriteLine($"- Tags (N:M): {string.Join(", ", _repository.GetTagsByFigurineId(figurineId).Select(tag => tag.Name))}");
        Console.WriteLine();
    }

    public void DisplayTagFigurines(int tagId)
    {
        var tag = _repository.GetTagById(tagId);
        if (tag is null)
        {
            Console.WriteLine("Tag not found.");
            return;
        }

        Console.WriteLine($"{tag.Name} tag figurines (N:M):");
        foreach (var figurine in _repository.GetFigurinesByTagId(tagId))
        {
            Console.WriteLine($"- {figurine.Name}");
        }
    }
}
