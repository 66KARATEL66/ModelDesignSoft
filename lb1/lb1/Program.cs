using lb1.Data;
using lb1.Services;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var repository = SeedData.CreateSample();
var application = new StoreConsoleApp(
    repository,
    new AnimeFigurineService(repository),
    new OrderService(repository));

application.Run();
