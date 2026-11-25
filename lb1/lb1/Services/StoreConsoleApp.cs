using lb1.Data;
using lb1.Models;

namespace lb1.Services;

public sealed class StoreConsoleApp
{
    private readonly AnimeFigurineRepository _repository;
    private readonly AnimeFigurineService _catalogService;
    private readonly OrderService _orderService;

    public StoreConsoleApp(
        AnimeFigurineRepository repository,
        AnimeFigurineService catalogService,
        OrderService orderService)
    {
        _repository = repository;
        _catalogService = catalogService;
        _orderService = orderService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Anime Figurine Store ===");
            Console.WriteLine("1. Меню продавця");
            Console.WriteLine("2. Меню покупця");
            Console.WriteLine("3. Переглянути вимоги системи");
            Console.WriteLine("0. Вихід");

            var choice = ReadMenuChoice("Ваш вибір: ");
            if (choice is null or 0)
                return;

            switch (choice)
            {
                case 1:
                    RunSellerMenu();
                    break;
                case 2:
                    RunCustomerMenu();
                    break;
                case 3:
                    ShowRequirements();
                    break;
                default:
                    Console.WriteLine("Оберіть пункт зі списку.");
                    break;
            }
        }
    }

    private void RunSellerMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Меню продавця ---");
            Console.WriteLine("1. Керування каталогом");
            Console.WriteLine("2. Покупці");
            Console.WriteLine("3. Переглянути замовлення");
            Console.WriteLine("4. Обробити замовлення");
            Console.WriteLine("5. Створити замовлення покупцю");
            Console.WriteLine("0. Назад");

            var choice = ReadMenuChoice("Ваш вибір: ");
            if (choice is null or 0)
                return;

            switch (choice)
            {
                case 1:
                    RunCatalogManagement();
                    break;
                case 2:
                    ManageCustomers();
                    break;
                case 3:
                    ShowOrders();
                    break;
                case 4:
                    ProcessOrder();
                    break;
                case 5:
                    CreateOrderForCustomer();
                    break;
                default:
                    Console.WriteLine("Оберіть пункт зі списку.");
                    break;
            }
        }
    }

    private void RunCustomerMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Меню покупця ---");
            Console.WriteLine("1. Переглянути каталог");
            Console.WriteLine("2. Створити замовлення");
            Console.WriteLine("3. Переглянути власні замовлення");
            Console.WriteLine("0. Назад");

            var choice = ReadMenuChoice("Ваш вибір: ");
            if (choice is null or 0)
                return;

            switch (choice)
            {
                case 1:
                    BrowseCatalog();
                    break;
                case 2:
                    var customer = FindOrRegisterCustomer();
                    if (customer is not null)
                        CreateOrder(customer);
                    break;
                case 3:
                    ShowCustomerOrders();
                    break;
                default:
                    Console.WriteLine("Оберіть пункт зі списку.");
                    break;
            }
        }
    }

    private void RunCatalogManagement()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Керування каталогом ---");
            Console.WriteLine("1. Переглянути каталог");
            Console.WriteLine("2. Додати серію AnimeSeries");
            Console.WriteLine("3. Додати фігурку Figurine");
            Console.WriteLine("4. Додати характеристики FigurineSpecification");
            Console.WriteLine("5. Додати мітку CollectionTag");
            Console.WriteLine("6. Призначити мітку FigurineTag");
            Console.WriteLine("0. Назад");

            var choice = ReadMenuChoice("Ваш вибір: ");
            if (choice is null or 0)
                return;

            switch (choice)
            {
                case 1:
                    PrintCatalog(_catalogService.GetCatalog());
                    break;
                case 2:
                    AddSeries();
                    break;
                case 3:
                    AddFigurine();
                    break;
                case 4:
                    AddSpecification();
                    break;
                case 5:
                    AddTag();
                    break;
                case 6:
                    AssignTag();
                    break;
                default:
                    Console.WriteLine("Оберіть пункт зі списку.");
                    break;
            }
        }
    }

    private void BrowseCatalog()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Каталог ---");
            Console.WriteLine("1. Усі фігурки");
            Console.WriteLine("2. Фігурки за серією");
            Console.WriteLine("3. Фігурки за міткою");
            Console.WriteLine("0. Назад");

            var choice = ReadMenuChoice("Ваш вибір: ");
            if (choice is null or 0)
                return;

            switch (choice)
            {
                case 1:
                    PrintCatalog(_catalogService.GetCatalog());
                    break;
                case 2:
                    PrintSeries();
                    var seriesId = ReadInt("ID серії: ");
                    if (seriesId.HasValue)
                        PrintCatalog(_catalogService.GetSeriesFigurines(seriesId.Value));
                    break;
                case 3:
                    PrintTags();
                    var tagId = ReadInt("ID мітки: ");
                    if (tagId.HasValue)
                        PrintCatalog(_catalogService.GetTagFigurines(tagId.Value));
                    break;
                default:
                    Console.WriteLine("Оберіть пункт зі списку.");
                    break;
            }
        }
    }

    private void AddSeries()
    {
        var title = ReadText("Назва серії: ");
        var studio = ReadText("Студія: ");
        if (title is null || studio is null)
            return;

        RunDataOperation(() =>
        {
            var series = _repository.AddSeries(title, studio);
            Console.WriteLine($"Серію додано. ID: {series.Id}");
        });
    }

    private void AddFigurine()
    {
        PrintSeries();
        var seriesId = ReadInt("ID серії: ");
        var name = ReadText("Назва фігурки: ");
        var price = ReadInt("Ціна у гривнях: ");
        if (seriesId is null || name is null || price is null)
            return;

        RunDataOperation(() =>
        {
            var figurine = _repository.AddFigurine(name, price.Value, seriesId.Value);
            Console.WriteLine($"Фігурку додано. ID: {figurine.Id}");
        });
    }

    private void AddSpecification()
    {
        PrintCatalog(_catalogService.GetCatalog());
        var figurineId = ReadInt("ID фігурки: ");
        var height = ReadDecimal("Висота у сантиметрах: ");
        var material = ReadText("Матеріал: ");
        var limitedEdition = ReadYesNo("Лімітоване видання? (т/н): ");
        if (figurineId is null || height is null || material is null || limitedEdition is null)
            return;

        RunDataOperation(() =>
        {
            _repository.AddSpecification(figurineId.Value, height.Value, material, limitedEdition.Value);
            Console.WriteLine("Характеристики додано.");
        });
    }

    private void AddTag()
    {
        var name = ReadText("Назва мітки: ");
        if (name is null)
            return;

        RunDataOperation(() =>
        {
            var tag = _repository.AddTag(name);
            Console.WriteLine($"Мітку додано. ID: {tag.Id}");
        });
    }

    private void AssignTag()
    {
        PrintCatalog(_catalogService.GetCatalog());
        PrintTags();
        var figurineId = ReadInt("ID фігурки: ");
        var tagId = ReadInt("ID мітки: ");
        if (figurineId is null || tagId is null)
            return;

        if (_repository.AssignTag(figurineId.Value, tagId.Value))
            Console.WriteLine("Мітку призначено.");
        else
            Console.WriteLine("Не вдалося призначити мітку: перевірте ID або наявність зв'язку.");
    }

    private void ManageCustomers()
    {
        Console.WriteLine();
        Console.WriteLine("--- Покупці ---");
        foreach (var customer in _repository.GetAllCustomers())
            Console.WriteLine($"{customer.Id}. {customer.Name} | {customer.Email} | {customer.Phone}");

        if (ReadYesNo("Додати покупця? (т/н): ") != true)
            return;

        var name = ReadText("Ім'я: ");
        var phone = ReadText("Телефон: ");
        var email = ReadText("Email: ");
        if (name is null || phone is null || email is null)
            return;

        RunDataOperation(() =>
        {
            var customer = _repository.AddCustomer(name, phone, email);
            Console.WriteLine($"Покупця додано. ID: {customer.Id}");
        });
    }

    private Customer? FindOrRegisterCustomer()
    {
        var email = ReadText("Email покупця: ");
        if (email is null)
            return null;

        var existing = _repository.GetCustomerByEmail(email);
        if (existing is not null)
            return existing;

        Console.WriteLine("Покупця не знайдено; створимо новий запис.");
        var name = ReadText("Ім'я: ");
        var phone = ReadText("Телефон: ");
        if (name is null || phone is null)
            return null;

        Customer? created = null;
        RunDataOperation(() => created = _repository.AddCustomer(name, phone, email));
        return created;
    }

    private void CreateOrderForCustomer()
    {
        foreach (var existingCustomer in _repository.GetAllCustomers())
            Console.WriteLine($"{existingCustomer.Id}. {existingCustomer.Name} | {existingCustomer.Email}");

        var customerId = ReadInt("ID покупця (0 — зареєструвати нового): ");
        if (customerId is null)
            return;

        var customer = customerId == 0
            ? FindOrRegisterCustomer()
            : _repository.GetCustomerById(customerId.Value);
        if (customer is null)
        {
            Console.WriteLine("Покупця не знайдено.");
            return;
        }

        CreateOrder(customer);
    }

    private void CreateOrder(Customer customer)
    {
        var order = _orderService.CreateOrder(customer);
        Console.WriteLine($"Створено замовлення #{order.Id} у стані {order.Status}.");

        while (true)
        {
            PrintCatalog(_catalogService.GetCatalog());
            var figurineId = ReadInt("ID фігурки (0 — завершити додавання): ");
            if (figurineId is null)
                return;
            if (figurineId == 0)
                break;

            var figurine = _repository.GetFigurineById(figurineId.Value);
            var quantity = ReadInt("Кількість: ");
            if (figurine is null || quantity is null ||
                !_orderService.AddItem(order, figurine, quantity.Value))
            {
                Console.WriteLine("Не вдалося додати позицію. Перевірте ID і кількість.");
            }
            else
            {
                Console.WriteLine("Позицію додано.");
            }
        }

        if (!_orderService.Submit(order))
        {
            Console.WriteLine("Порожнє замовлення не можна надіслати; його скасовано.");
            _orderService.Cancel(order);
            return;
        }

        _orderService.Confirm(order);
        Console.WriteLine($"Сума: {order.Total} грн. Стан: {order.Status}.");
        if (ReadYesNo("Оплатити зараз? (т/н): ") == true && _orderService.Pay(order))
            Console.WriteLine($"Оплату #{order.Payment?.Id} зафіксовано. Стан: {order.Status}.");
        else
            Console.WriteLine("Замовлення очікує оплати.");
    }

    private void ShowCustomerOrders()
    {
        var email = ReadText("Email покупця: ");
        if (email is null)
            return;

        var customer = _repository.GetCustomerByEmail(email);
        if (customer is null)
        {
            Console.WriteLine("Покупця не знайдено.");
            return;
        }

        PrintOrders(_repository.GetOrdersByCustomerId(customer.Id));
    }

    private void ShowOrders() => PrintOrders(_repository.GetAllOrders());

    private void ProcessOrder()
    {
        PrintOrders(_repository.GetAllOrders());
        var orderId = ReadInt("ID замовлення: ");
        if (orderId is null)
            return;

        var order = _repository.GetOrderById(orderId.Value);
        if (order is null)
        {
            Console.WriteLine("Замовлення не знайдено.");
            return;
        }

        Console.WriteLine("1. Виконати наступний дозволений перехід");
        Console.WriteLine("2. Скасувати замовлення");
        var choice = ReadMenuChoice("Ваш вибір: ");
        if (choice == 2)
        {
            Console.WriteLine(_orderService.Cancel(order)
                ? $"Замовлення скасовано: {order.Status}."
                : "Замовлення не можна скасувати в поточному стані.");
            return;
        }

        var changed = order.Status switch
        {
            OrderStatus.Draft => _orderService.Submit(order),
            OrderStatus.Submitted => _orderService.Confirm(order),
            OrderStatus.Confirmed => _orderService.Pay(order),
            OrderStatus.Paid => _orderService.Ship(order),
            OrderStatus.Shipped => _orderService.Deliver(order),
            _ => false
        };
        Console.WriteLine(changed
            ? $"Новий стан замовлення: {order.Status}."
            : "Для цього замовлення немає дозволеного переходу.");
    }

    private void ShowRequirements()
    {
        Console.WriteLine();
        Console.WriteLine("--- Вимоги системи ---");
        foreach (var requirement in RequirementCatalog.All)
        {
            Console.WriteLine($"{requirement.Id} [{requirement.Kind}] {requirement.Title}");
            Console.WriteLine($"  {requirement.AcceptanceCriteria}");
        }
    }

    private void PrintCatalog(IEnumerable<Figurine> figurines)
    {
        var items = figurines.ToArray();
        if (items.Length == 0)
        {
            Console.WriteLine("Фігурок не знайдено.");
            return;
        }

        Console.WriteLine();
        foreach (var figurine in items)
        {
            var series = figurine.AnimeSeries?.Title ?? "Без серії";
            var tags = string.Join(", ", _repository.GetTagsByFigurineId(figurine.Id).Select(tag => tag.Name));
            var specification = figurine.Specification is null
                ? "Характеристики відсутні"
                : $"{figurine.Specification.HeightCm} см, {figurine.Specification.Material}" +
                  (figurine.Specification.LimitedEdition ? ", лімітована" : string.Empty);
            Console.WriteLine(
                $"{figurine.Id}. {figurine.Name} | {series} | {figurine.Price} грн | {specification} | {tags}");
        }
    }

    private void PrintSeries()
    {
        foreach (var series in _repository.GetAllSeries())
            Console.WriteLine($"{series.Id}. {series.Title} ({series.Studio})");
    }

    private void PrintTags()
    {
        foreach (var tag in _repository.GetAllTags())
            Console.WriteLine($"{tag.Id}. {tag.Name}");
    }

    private static void PrintOrders(IEnumerable<Order> orders)
    {
        var items = orders.ToArray();
        if (items.Length == 0)
        {
            Console.WriteLine("Замовлень немає.");
            return;
        }

        foreach (var order in items)
        {
            var payment = order.Payment is null
                ? "без оплати"
                : $"{order.Payment.Status}, {order.Payment.Amount} грн";
            Console.WriteLine(
                $"#{order.Id} | {order.Customer?.Name} | {order.Status} | {order.Total} грн | {payment}");
            foreach (var item in order.Items)
                Console.WriteLine($"  - {item.Figurine?.Name}, {item.Quantity} x {item.UnitPrice} грн");
        }
    }

    private static void RunDataOperation(Action operation)
    {
        try
        {
            operation();
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Помилка даних: {exception.Message}");
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"Операцію не виконано: {exception.Message}");
        }
    }

    private static int? ReadMenuChoice(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (input is null)
            return null;
        return int.TryParse(input, out var choice) ? choice : -1;
    }

    private static int? ReadInt(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (input is null)
            return null;
        if (int.TryParse(input, out var value))
            return value;
        Console.WriteLine("Введіть ціле число.");
        return null;
    }

    private static decimal? ReadDecimal(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (input is null)
            return null;
        if (decimal.TryParse(input, out var value))
            return value;
        Console.WriteLine("Введіть коректне число.");
        return null;
    }

    private static string? ReadText(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            if (input is not null)
                Console.WriteLine("Поле не може бути порожнім.");
            return null;
        }
        return input.Trim();
    }

    private static bool? ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (input is null)
                return null;

            switch (input.Trim().ToLowerInvariant())
            {
                case "т":
                case "так":
                case "y":
                case "yes":
                    return true;
                case "н":
                case "ні":
                case "n":
                case "no":
                    return false;
                default:
                    Console.WriteLine("Введіть «т» або «н» (так/ні).");
                    break;
            }
        }
    }
}
