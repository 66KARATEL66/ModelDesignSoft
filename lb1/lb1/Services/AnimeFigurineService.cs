using lb1.Data;
using lb1.Models;

namespace lb1.Services;

public sealed class AnimeFigurineService
{
    private readonly AnimeFigurineRepository _repository;

    public AnimeFigurineService(AnimeFigurineRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<Figurine> GetCatalog() => _repository.GetAllFigurines();

    public IReadOnlyList<Figurine> GetSeriesFigurines(int seriesId) =>
        _repository.GetFigurinesBySeriesId(seriesId);

    public IReadOnlyList<Figurine> GetTagFigurines(int tagId) =>
        _repository.GetFigurinesByTagId(tagId);

    public Figurine? GetFigurineDetails(int figurineId) =>
        _repository.GetFigurineById(figurineId);
}
