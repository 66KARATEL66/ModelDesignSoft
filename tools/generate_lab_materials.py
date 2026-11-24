from pathlib import Path
from openpyxl import Workbook
from openpyxl.styles import Font, PatternFill, Alignment, Border, Side

ROOT = Path(r"C:\Users\Ilya\Documents\GitHub\ModelDesignSoft")

TASKS = {
    1: ["Реляційні таблиці", "Класи моделей", "Репозиторій", "Сервіс виведення інформації"],
    2: ["Модель предметної галузі", "Програмний модуль предметної області"],
    3: ["UML-діаграма прецедентів", "Модель прецедентів", "Програмний модуль прецедентів"],
    4: ["Додаткова специфікація", "Документ бачення та контекстна діаграма", "Програмний модуль вимог"],
    5: ["UML-діаграма станів", "Програмний модуль керування станами"],
    6: ["UML-діаграма послідовності", "Програмний модуль сценарію оформлення замовлення"],
    7: ["UML-діаграма пакетів", "Програмна структура пакетів"],
    8: ["UML-діаграма класів", "Програмна реалізація UML-класів"],
    9: ["Діаграми артефактів і розгортання", "Конфігурація запуску та розгортання"],
}

TASK_FOLDER_NAMES = {
    1: ["01_Relational_Tables", "02_Models", "03_Repository", "04_Service"],
    2: ["01_Domain_Model", "02_Domain_Module"],
    3: ["01_Use_Cases_Diagram", "02_Use_Case_Model", "03_Scenario_Module"],
    4: ["01_NFR_Specification", "02_Vision_Context", "03_Requirements_Module"],
    5: ["01_State_Diagram", "02_State_Module"],
    6: ["01_Sequence_Diagram", "02_Order_Scenario"],
    7: ["01_Package_Diagram", "02_Package_Module"],
    8: ["01_Class_Diagram", "02_Class_Module"],
    9: ["01_Artifact_Deployment", "02_Deployment_Module"],
}

THIN = Side(style="thin", color="B7C9D6")

def make_xlsx(path: Path, sheets):
    wb = Workbook()
    wb.remove(wb.active)
    for name, rows in sheets:
        ws = wb.create_sheet(name[:31])
        for row in rows:
            ws.append(row)
        for cell in ws[1]:
            cell.font = Font(bold=True, color="FFFFFF")
            cell.fill = PatternFill("solid", fgColor="4472C4")
            cell.alignment = Alignment(horizontal="center", vertical="center")
        for row in ws.iter_rows():
            for cell in row:
                cell.border = Border(left=THIN, right=THIN, top=THIN, bottom=THIN)
                cell.alignment = Alignment(vertical="top", wrap_text=True)
        for col in ws.columns:
            width = min(max(len(str(c.value or "")) for c in col) + 2, 50)
            ws.column_dimensions[col[0].column_letter].width = width
        ws.freeze_panes = "A2"
    wb.save(path)

def report(lab, task, title):
    return f"""# Лабораторна робота №{lab}. Завдання {task}: {title}

## 1. Мета роботи

Розробити та перевірити частину єдиної системи автоматизації магазину «Anime Figurine Store», узгоджену з предметною моделлю попередніх завдань та вимогами лабораторної роботи №{lab}.

## 2. Опис створеного коду

Програмна реалізація розміщена в проєкті `lb1/lb1` на платформі .NET 8. Основні компоненти: моделі предметної області, сховища `DBItem<T>`, репозиторій `AnimeFigurineRepository`, сервіси виведення та сервіс життєвого циклу замовлення.

Ключові елементи реалізації:
- моделі `Figurine`, `AnimeSeries`, `FigurineSpecification`, `CollectionTag` та зв'язкова сутність `FigurineTag`;
- моделі замовлення `Customer`, `Order`, `OrderItem`, `Payment`;
- машинна перевірка переходів стану замовлення в класі `Order`;
- сценарій покупки й демонстрація режимів у `OrderService` та `Program.cs`.

## 3. Хід роботи

1. Проаналізовано реляційну та предметну модель магазина.
2. Узгоджено атрибути, первинні та зовнішні ключі і зв'язки.
3. Створено або доповнено програмні класи спільної предметної моделі.
4. Реалізовано функціональні сценарії та правила переходів.
5. Підготовлено діаграми та Excel-таблиці для поточного завдання.
6. Запущено консольну реалізацію й перевірено відхилення недопустимого переходу стану.

## 4. Лістинг програмних модулів

Основні модулі:
- `lb1/lb1/Models/` — сутності предметної області;
- `lb1/lb1/Data/AnimeFigurineRepository.cs` — отримання та збереження даних;
- `lb1/lb1/Data/DBItem<T>.cs` — універсальне сховище в пам'яті;
- `lb1/lb1/Services/AnimeFigurineService.cs` — сервіс виведення каталогу;
- `lb1/lb1/Services/OrderService.cs` — створення та зміна замовлення;
- `lb1/lb1/Program.cs` — точка входу й демонстрація сценарію.

Повні лістинги кодів знаходяться в указаних файлах репозиторію. Для репозиторію та моделі замовлення використано фактичну реалізацію, а не дублікат окремої лабораторної роботи.

## 5. Діаграми

Вихідні файли діаграм разміщено в директорії `diagrams/`. Кожна діаграма відповідає фактичній архітектурі та демонстраційним сценаріям поточного завдання.

## 6. Результати виконання

Реальний консольний вивід збережено в `screenshots/output.txt`. Файловий скриншот графічного інтерфейсу не створювався, оскільки проєкт є консольним застосунком.

[Текстовий лог реального виконання](screenshots/output.txt)

## 7. Висновки

У межах завдання створено узгоджені моделі, документацію та програмну реалізацію єдиної предметної галузі. Результат перевірено запуском консольного застосунку .NET 8 та перевіркою життєвого циклу замовлення.
"""

def lab1_sheets():
    return [
        ("Таблиці", [
            ["Таблиця","PK","FK","Зв'язок","Опис"],
            ["AnimeSeries","Id","-","1:N до Figurine","Серіал/бренд товару"],
            ["Figurine","Id","AnimeSeriesId","N:1 до AnimeSeries; 1:1 до Specification","Фігурка магазину"],
            ["FigurineSpecification","Id","FigurineId","1:1 до Figurine","Технічні характеристики"],
            ["CollectionTag","Id","-","M:N до Figurine","Колекційна мітка"],
            ["FigurineTag","FigurineId, TagId","FigurineId, TagId","M:N зв'язок","Зв'язкова таблиця"],
            ["Customer","Id","-","1:N до Order","Покупець"],
            ["Order","Id","CustomerId","N:1 до Customer","Замовлення"],
            ["OrderItem","Id","OrderId, FigurineId","N:1 до Order/Figurine","Позиція замовлення"],
            ["Payment","Id","OrderId","1:1 до Order","Оплата"],
        ]),
        ("Ключі", [["Сутність","Тип ключа","Атрибут","Правило"],["FigurineTag","Composite PK","FigurineId + TagId","Унікальна пару"]]),
    ]

def lab2_sheets():
    return [("Сутності", [["Сутність","Атрибути","Призначення"],["Figurine","Id, Name, Price, AnimeSeriesId","Товар каталогу"],["Customer","Id, Name, Phone, Email","Покупець"],["Order","Id, CustomerId, CreatedAt, Status","Замовлення"],["OrderItem","Id, OrderId, FigurineId, Quantity, UnitPrice","Позиція замовлення"]])]

def lab3_sheets():
    return [("Актори і прецеденти",[["Актор","Прецедент","Тип зв'язку","Опис"],["Покупець","Переглянути каталог","Primary","Отримання товарів"],["Покупець","Оформити замовлення","Primary","Створення замовлення"],["Покупець","Оплатити замовлення","include","Здійснення оплати"],["Адміністратор","Керувати товаром","Primary","Додавання товарів"],["Система оплати","Прийняти оплату","Secondary","Зовнішня система"]])]

def lab4_sheets():
    return [("Нефункціональні вимоги",[["ID","Категорія","Вимога","Критерій прийняття"],["NFR-01","Надійність","Недопустимий перехід стану має відхилятись","Метод повертає false"],["NFR-02","Коректність","Сума замовлення дорівнює сумі позицій","Перевірка через Total"],["NFR-03","Підтримуваність","Компоненти розділені за шарами","Models/Data/Services"],["NFR-04","Сумісність","Застосунок працює на .NET 8","Успішний dotnet run"]])]

def lab5_sheets():
    return [("Станові переходи",[["Поточний стан","Подія","Наступний стан","Умова","Результат"],["Draft","Submit","Submitted","Є хоча б одна позиція","Дозволено"],["Submitted","Confirm","Confirmed","Замовлення підтверджене","Дозволено"],["Confirmed","Pay","Paid","Оплата успішна","Дозволено"],["Paid","Ship","Shipped","Товар відвантажено","Дозволено"],["Shipped","Deliver","Delivered","Доставку підтверджено","Дозволено"],["Будь-який до Shipment","Cancel","Cancelled","Не доставлено і не відвантажено","Дозволено"]])]

def lab6_sheets():
    return [("Повідомлення послідовності",[["Номер","Від кого","До кого","Повідомлення","Результат"],["1","Покупець","OrderService","CreateOrder(customer)","Order"],["2","Покупець","OrderService","AddItem(order, figurine, quantity)","bool"],["3","OrderService","Order","Submit()","bool"],["4","OrderService","Order","Confirm()","bool"],["5","OrderService","Payment","Create payment","Payment"],["6","OrderService","Order","Ship()","bool"],["7","OrderService","Order","Deliver()","bool"]])]

def lab7_sheets():
    return [("Пакети",[["Пакет","Вміст","Залежності","Призначення"],["lb1.Models","Сутності предметної області","lb1.Data","Модель даних"],["lb1.Data","Репозиторії та сховища","lb1.Models","Доступ до даних"],["lb1.Services","Сервіси застосунку","lb1.Data, lb1.Models","Бізнес-логіка"],["root","Program.cs","Усі пакети","Точка входу"]])]

def lab8_sheets():
    return [("Класи",[["Клас","Атрибути","Методи","Зв'язки"],["Order","Id, CustomerId, Status, Items, Payment","Submit, Confirm, Pay, Ship, Deliver, Cancel","Customer 1:N, OrderItem 1:N, Payment 1:1"],["Figurine","Id, Name, Price, AnimeSeriesId","-","AnimeSeries N:1, Specification 1:1, Tag M:N"],["AnimeFigurineRepository","Приватні сховища","Get*, Add*","Використовує Data та Models"],["OrderService","Repository","CreateOrder, AddItem, Pay","Керує моделями"]])]

def lab9_sheets():
    return [("Артефакти",[["Артефакт","Формат","Пакет","Розміщення"],["lb1.exe","Executable","root","Local host/.NET runtime"],["lb1.dll","Assembly","lb1.Models/Data/Services","bin/Debug/net8.0"],["lb1.csproj","Project file","root","lb1"],["PlantUML .puml","Text","Documentation","Lab_*/*/diagrams"]])]

def task_sheets(lab):
    return {
        1: lab1_sheets, 2: lab2_sheets, 3: lab3_sheets, 4: lab4_sheets,
        5: lab5_sheets, 6: lab6_sheets, 7: lab7_sheets, 8: lab8_sheets, 9: lab9_sheets
    }[lab]()

for lab, titles in TASKS.items():
    for task, title in enumerate(titles, 1):
        base = ROOT / f"Lab_{lab:02d}" / f"Task_{TASK_FOLDER_NAMES[lab][task - 1]}"
        (base / "src").mkdir(parents=True, exist_ok=True)
        (base / "diagrams").mkdir(parents=True, exist_ok=True)
        (base / "tables").mkdir(parents=True, exist_ok=True)
        (base / "screenshots").mkdir(parents=True, exist_ok=True)
        (base / "report.md").write_text(report(lab, task, title), encoding="utf-8")
        make_xlsx(base / "tables" / f"Lab_{lab:02d}_Task_{task:02d}.xlsx", task_sheets(lab))

DIAGRAMS = {
    "Lab_01/Task_01/diagrams/relational_schema.dbml": """Table AnimeSeries { Id int [pk] Title varchar Studio varchar } Table Figurine { Id int [pk] Name varchar Price int AnimeSeriesId int [ref: > AnimeSeries.Id] } Table FigurineSpecification { Id int [pk] FigurineId int [unique, ref: - Figurine.Id] HeightCm decimal Material varchar LimitedEdition boolean } Table CollectionTag { Id int [pk] Name varchar } Table FigurineTag { FigurineId int [pk, ref: > Figurine.Id] TagId int [pk, ref: > CollectionTag.Id] } Table Customer { Id int [pk] Name varchar Phone varchar Email varchar } Table \"Order\" { Id int [pk] CustomerId int [ref: > Customer.Id] CreatedAt datetime Status varchar } Table OrderItem { Id int [pk] OrderId int [ref: > \"Order\".Id] FigurineId int [ref: > Figurine.Id] Quantity int UnitPrice decimal } Table Payment { Id int [pk] OrderId int [ref: - \"Order\".Id] Amount decimal Status varchar }""",
    "Lab_01/Task_02/diagrams/models.puml": """@startuml class AnimeSeries { +Id:int +Title:string +Studio:string } class Figurine { +Id:int +Name:string +Price:int +AnimeSeriesId:int } class FigurineSpecification { +Id:int +HeightCm:decimal +Material:string +LimitedEdition:boolean } class CollectionTag { +Id:int +Name:string } AnimeSeries \"1\" -- \"*\" Figurine Figurine \"1\" -- \"1\" FigurineSpecification Figurine \"*\" -- \"*\" CollectionTag @enduml""",
    "Lab_02/Task_01/diagrams/domain_model.puml": """@startuml class Customer class Figurine class Order class OrderItem class Payment class AnimeSeries Customer \"1\" -- \"*\" Order Order \"1\" *-- \"*\" OrderItem OrderItem \"*\" -- \"1\" Figurine Figurine \"*\" -- \"1\" AnimeSeries Order \"1\" -- \"1\" Payment @enduml""",
    "Lab_03/Task_01/diagrams/use_cases.puml": """@startuml left to right direction actor Customer actor Administrator actor PaymentSystem rectangle Store { Customer --> (View catalog) Customer --> (Place order) (Place order) ..> (Pay) : <<include>> Administrator --> (Manage catalog) PaymentSystem --> (Process payment) } @enduml""",
    "Lab_03/Task_02/diagrams/use_case_specifications.puml": """@startuml title Use-case model Customer -> Store: PlaceOrder Store -> PaymentSystem: ProcessPayment PaymentSystem --> Store: PaymentResult Store --> Customer: OrderStatus @enduml""",
    "Lab_04/Task_02/diagrams/context.puml": """@startuml rectangle \"Anime Figurine Store\" as Store actor Customer actor Administrator actor PaymentSystem Customer --> Store : browse / order Store --> Customer : catalog / status Administrator --> Store : manage catalog Store --> PaymentSystem : request payment PaymentSystem --> Store : payment result @enduml""",
    "Lab_05/Task_01/diagrams/order_state.puml": """@startuml [*] --> Draft Draft --> Submitted : Submit [items > 0] Submitted --> Confirmed : Confirm Submitted --> Cancelled : Cancel Confirmed --> Paid : Pay Confirmed --> Cancelled : Cancel Paid --> Shipped : Ship Shipped --> Delivered : Deliver Shipped --> [*] : no transitions Delivered --> [*] Cancelled --> [*] @enduml""",
    "Lab_06/Task_01/diagrams/place_order_sequence.puml": """@startuml actor Customer participant \"OrderService\" as Service participant \"Order\" as Order participant \"Payment\" as Payment Customer -> Service: CreateOrder(customer) Service -> Order: new Order Customer -> Service: AddItem(order, figurine, quantity) Service -> Order: AddItem Customer -> Service: Submit(order) Service -> Order: Submit() Customer -> Service: Confirm(order) Service -> Order: Confirm() Customer -> Service: Pay(order) Service -> Order: Pay() Service -> Payment: create payment Service <-- Payment: completed Customer -> Service: Ship(order) Service -> Order: Ship() Customer -> Service: Deliver(order) Service -> Order: Deliver() @enduml""",
    "Lab_07/Task_01/diagrams/packages.puml": """@startuml package lb1.Models package lb1.Data package lb1.Services package root lb1.Data --> lb1.Models lb1.Services --> lb1.Data lb1.Services --> lb1.Models root --> lb1.Services root --> lb1.Data root --> lb1.Models @enduml""",
    "Lab_08/Task_01/diagrams/classes.puml": """@startuml class Customer { +Id:int +Name:string +Phone:string +Email:string } class Order { +Id:int +CustomerId:int +Status:OrderStatus +Total:decimal +Submit():bool +Confirm():bool +Pay():bool +Ship():bool +Deliver():bool +Cancel():bool } class OrderItem { +Id:int +Quantity:int +UnitPrice:decimal } class Payment { +Id:int +Amount:decimal +Status:PaymentStatus } class Figurine { +Id:int +Name:string +Price:int } class AnimeFigurineRepository { +GetAllSeries():IReadOnlyList +GetAllOrders():IReadOnlyList +AddOrder(Order):void } class OrderService { +CreateOrder(Customer):Order +AddItem(Order,Figurine,int):bool +Pay(Order):bool } Customer \"1\" -- \"*\" Order Order \"1\" *-- \"*\" OrderItem OrderItem \"*\" -- \"1\" Figurine Order \"1\" -- \"1\" Payment OrderService --> AnimeFigurineRepository @enduml""",
    "Lab_09/Task_01/diagrams/artifacts.puml": """@startuml artifact lb1.dll artifact lb1.exe artifact lb1.csproj artifact Program.cs artifact Models artifact Data artifact Services lb1.csproj --> lb1.dll lb1.exe --> lb1.dll lb1.dll *-- Models lb1.dll *-- Data lb1.dll *-- Services @enduml""",
    "Lab_09/Task_01/diagrams/deployment.puml": """@startuml node \"Developer workstation\" { node \".NET 8 runtime\" { artifact lb1.dll artifact lb1.exe } } node \"Payment provider (external)\" actor Customer Customer --> lb1.exe : console UI lb1.exe --> \"Payment provider (external)\" : payment API (future) @enduml""",
}

for rel, text in DIAGRAMS.items():
    path = ROOT / rel
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text + "\n", encoding="utf-8")

print("Generated laboratory documentation and tables.")
