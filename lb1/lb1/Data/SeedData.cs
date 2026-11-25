using lb1.Models;
using lb1.Services;

namespace lb1.Data;

public static class SeedData
{
    public static AnimeFigurineRepository CreateSample()
    {
        var repository = new AnimeFigurineRepository();
        var naruto = repository.AddSeries("Naruto", "Pierrot");
        var onePiece = repository.AddSeries("One Piece", "Toei Animation");

        var narutoFigure = repository.AddFigurine("Naruto Uzumaki Sage Mode", 4500, naruto.Id);
        var luffyFigure = repository.AddFigurine("Monkey D. Luffy Gear Fifth", 6000, onePiece.Id);
        var zoroFigure = repository.AddFigurine("Roronoa Zoro Wano", 5000, onePiece.Id);

        repository.AddSpecification(narutoFigure.Id, 18, "PVC", false);
        repository.AddSpecification(luffyFigure.Id, 22, "PVC and ABS", true);
        repository.AddSpecification(zoroFigure.Id, 20, "PVC", false);

        var protagonist = repository.AddTag("Protagonist");
        var limitedEdition = repository.AddTag("Limited Edition");
        var swordsman = repository.AddTag("Swordsman");

        repository.AssignTag(narutoFigure.Id, protagonist.Id);
        repository.AssignTag(luffyFigure.Id, protagonist.Id);
        repository.AssignTag(luffyFigure.Id, limitedEdition.Id);
        repository.AssignTag(zoroFigure.Id, swordsman.Id);

        var demoCustomer = repository.AddCustomer("Демо-покупець", "0000000000", "demo@example.com");
        var orderService = new OrderService(repository);
        var demoOrder = orderService.CreateOrder(demoCustomer);
        orderService.AddItem(demoOrder, narutoFigure, 1);
        orderService.AddItem(demoOrder, luffyFigure, 1);
        orderService.Submit(demoOrder);
        orderService.Confirm(demoOrder);
        orderService.Pay(demoOrder);

        return repository;
    }
}
