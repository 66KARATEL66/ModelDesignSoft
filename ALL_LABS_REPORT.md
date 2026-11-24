# Послідовний огляд виконаних лабораторних робіт

У цьому файлі зібрано послідовний огляд лабораторних №1–9. Реальні логи виведення збережено у відповідних папках `screenshots/output.txt`; графічні скриншоти для консольного застосунку не підроблялися.

## Лабораторна №1

**Завдання:** реляційні таблиці, класові моделі, репозиторій і сервіс виведення.

Реалізовано сутності в окремих папках:

- [AnimeSeries](lb1/lb1/Labs/Lab_01/Task_02_Models/Entities/AnimeSeries.cs)
- [Figurine](lb1/lb1/Labs/Lab_01/Task_02_Models/Entities/Figurine.cs)
- [FigurineSpecification](lb1/lb1/Labs/Lab_01/Task_02_Models/Entities/FigurineSpecification.cs)
- [CollectionTag](lb1/lb1/Labs/Lab_01/Task_02_Models/Entities/CollectionTag.cs)
- [Репозиторій](lb1/lb1/Labs/Lab_01/Task_03_Repository/Data/AnimeFigurineRepository.cs)
- [Сервіс](lb1/lb1/Labs/Lab_01/Task_04_Service/Services/AnimeFigurineService.cs)

**Скриншот/лог:** замініть позначку нижче реальним скриншотом консолі після запуску `dotnet run`; поточний реальний текстовий лог — [output.txt](Lab_01/Task_04_Service/screenshots/output.txt).

`[СКРИНШОТ: консоль, рядки Anime Figurine Store та One Piece figurines]`

Звіт завдань: [реляційні таблиці](Lab_01/Task_01_Relational_Tables/report.md), [моделі](Lab_01/Task_02_Models/report.md), [репозиторій](Lab_01/Task_03_Repository/report.md), [сервіс](Lab_01/Task_04_Service/report.md).

## Лабораторна №2

**Завдання:** предметна модель і модуль предметної області.

- Сутність [Customer](lb1/lb1/Labs/Lab_02/Task_01_Domain_Model/Entities/Customer.cs).
- Завдання моделі: [звіт](Lab_02/Task_01_Domain_Model/report.md).
- Завдання модуля: [звіт](Lab_02/Task_02_Domain_Module/report.md).

`[СКРИНШОТ: вивід LAB 2 DOMAIN MODEL із загальною кількістю сутностей]`

Скрипт виведення знаходиться в [AllLabsRunner.cs](lb1/lb1/Scripts/AllLabsRunner.cs).

## Лабораторна №3

**Завдання:** діаграма прецедентів, модель прецедентів і сценаріївий модуль.

- [Діаграма](Lab_03/Task_01_Use_Cases_Diagram/diagrams/use_cases.puml)
- [Модель прецедентів](Lab_03/Task_02_Use_Case_Model/report.md)
- [Модуль сценарію](Lab_03/Task_03_Scenario_Module/report.md)

`[СКРИНШОТ: секція LAB 3: USE CASES у консолі]`

## Лабораторна №4

**Завдання:** нефункціональні вимоги, бачення, контекстна діаграма та модуль вимог.

- [Нефункціональні вимоги](Lab_04/Task_01_NFR_Specification/report.md)
- [Контекстна діаграма](Lab_04/Task_02_Vision_Context/diagrams/context.puml)
- [Модуль вимог](Lab_04/Task_03_Requirements_Module/report.md)

`[СКРИНШОТ: секція LAB 4: QUALITY AND CONTEXT і перевірені вимоги]`

## Лабораторна №5

**Завдання:** UML станів і модуль переходів замовлення.

- Сутність [Order](lb1/lb1/Labs/Lab_05/Task_02_State_Module/Entities/Order.cs).
- [Діаграма станів](Lab_05/Task_01_State_Diagram/diagrams/order_state.puml)
- [Модуль станів](Lab_05/Task_02_State_Module/report.md)

`[СКРИНШОТ: початковий стан Draft і результат недопустимого Ship до оплати]`

## Лабораторна №6

**Завдання:** послідовність оформлення замовлення та програмний сценарій.

- Сутності [OrderItem](lb1/lb1/Labs/Lab_06/Task_02_Order_Scenario/Entities/OrderItem.cs) і [Payment](lb1/lb1/Labs/Lab_06/Task_02_Order_Scenario/Entities/Payment.cs).
- [OrderService](lb1/lb1/Labs/Lab_06/Task_02_Order_Scenario/Services/OrderService.cs).
- [Діаграма послідовності](Lab_06/Task_01_Sequence_Diagram/diagrams/place_order_sequence.puml)

`[СКРИНШОТ: секція LAB 6: Place-order sequence, стани й Total]`

## Лабораторна №7

**Завдання:** UML пакетів і впорядкування структури.

- [Діаграма пакетів](Lab_07/Task_01_Package_Diagram/diagrams/packages.puml)
- [Модуль пакетів](Lab_07/Task_02_Package_Module/report.md)

`[СКРИНШОТ: секція LAB 7: PACKAGES у консолі та структура lb1.Models/lb1.Data/lb1.Services]`

## Лабораторна №8

**Завдання:** UML класів і узгодження коду з діаграмою.

- [Діаграма класів](Lab_08/Task_01_Class_Diagram/diagrams/classes.puml)
- [Модуль узгодження класів](Lab_08/Task_02_Class_Module/report.md)

`[СКРИНШОТ: секція LAB 8: CLASS ALIGNMENT]`

## Лабораторна №9

**Завдання:** артефакти, розгортання та конфігурація запуску.

- [Діаграма артефактів](Lab_09/Task_01_Artifact_Deployment/diagrams/artifacts.puml)
- [Діаграма розгортання](Lab_09/Task_01_Artifact_Deployment/diagrams/deployment.puml)
- [Модуль розгортання](Lab_09/Task_02_Deployment_Module/report.md)

`[СКРИНШОТ: секція LAB 9: ARTIFACTS AND DEPLOYMENT]`
