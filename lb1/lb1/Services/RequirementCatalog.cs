using lb1.Models;

namespace lb1.Services;

public static class RequirementCatalog
{
    private static IReadOnlyList<string> Entities(params string[] names) =>
        Array.AsReadOnly(names);

    public static IReadOnlyList<Requirement> All { get; } = Array.AsReadOnly(new[]
    {
        new Requirement("FR-01", RequirementKind.Functional, "Browse catalog",
            "Browse figurines by series and collection tags.",
            "Matching figurines, prices, and specifications are shown.",
            Entities("AnimeSeries", "Figurine", "FigurineSpecification", "CollectionTag", "FigurineTag")),
        new Requirement("FR-02", RequirementKind.Functional, "Manage catalog",
            "Maintain series, figurines, specifications, and tags.",
            "Catalog changes appear in the updated catalog.",
            Entities("AnimeSeries", "Figurine", "FigurineSpecification", "CollectionTag", "FigurineTag")),
        new Requirement("FR-03", RequirementKind.Functional, "Create order",
            "Create an order for a customer from selected figurines and quantities.",
            "The order references its customer and contains its selected items.",
            Entities("Customer", "Order", "OrderItem", "Figurine")),
        new Requirement("FR-04", RequirementKind.Functional, "Calculate order total",
            "Calculate the total from each item's quantity and unit price.",
            "Total equals the sum of Quantity multiplied by UnitPrice for every order item.",
            Entities("Order", "OrderItem")),
        new Requirement("FR-05", RequirementKind.Functional, "Record payment",
            "Record the completed payment for an order.",
            "A successful payment creates one payment record and marks the order paid.",
            Entities("Order", "Payment")),
        new Requirement("FR-06", RequirementKind.Functional, "Track order",
            "Show customers their orders and current status.",
            "The order status and its items are available to the customer.",
            Entities("Customer", "Order", "OrderItem")),
        new Requirement("FR-07", RequirementKind.Functional, "Manage order lifecycle",
            "Allow only valid transitions through the order lifecycle.",
            "Invalid transitions are rejected without changing the order status.",
            Entities("Order", "Payment")),
        new Requirement("NFR-01", RequirementKind.NonFunctional, "Reliable transitions",
            "Reject invalid state transitions without changing order state.",
            "The operation reports failure and preserves the original state.",
            Entities("Order")),
        new Requirement("NFR-02", RequirementKind.NonFunctional, "Data integrity",
            "Preserve entity references and payment uniqueness.",
            "References target existing records and an order has at most one payment.",
            Entities("AnimeSeries", "Figurine", "FigurineSpecification", "CollectionTag", "FigurineTag",
                "Customer", "Order", "OrderItem", "Payment")),
        new Requirement("NFR-03", RequirementKind.NonFunctional, "Maintainability",
            "Keep data, business logic, and console interaction in separate layers.",
            "Models, data access, and services are separate project folders.",
            Entities()),
        new Requirement("NFR-04", RequirementKind.NonFunctional, "Clear feedback",
            "Explain why an operation was rejected.",
            "Invalid menu input and failed operations produce a clear message.",
            Entities("Order", "Payment")),
        new Requirement("NFR-05", RequirementKind.NonFunctional, ".NET compatibility",
            "Support the .NET 8 target framework.",
            "The application builds for net8.0.",
            Entities()),
        new Requirement("BR-01", RequirementKind.BusinessRule, "Non-empty order",
            "An order with no items cannot be submitted.",
            "Submitting an empty order is rejected.",
            Entities("Order", "OrderItem")),
        new Requirement("BR-02", RequirementKind.BusinessRule, "Order total",
            "The order total is the sum of quantity multiplied by unit price.",
            "The calculated total matches all order items.",
            Entities("OrderItem")),
        new Requirement("BR-03", RequirementKind.BusinessRule, "One payment per order",
            "An order may have at most one payment.",
            "A second payment record for an order is rejected.",
            Entities("Order", "Payment")),
        new Requirement("BR-04", RequirementKind.BusinessRule, "Cancellation limit",
            "An order cannot be cancelled after shipment or delivery.",
            "Cancel is rejected for Shipped and Delivered orders.",
            Entities("Order")),
        new Requirement("BR-05", RequirementKind.BusinessRule, "Catalog relationships",
            "A figurine has at most one specification; tags relate many-to-many.",
            "Specification ownership is unique and the M:N link uses FigurineTag.",
            Entities("Figurine", "FigurineSpecification", "CollectionTag", "FigurineTag"))
    });

    public static IReadOnlyList<Requirement> ByKind(RequirementKind kind) =>
        Array.AsReadOnly(All.Where(requirement => requirement.Kind == kind).ToArray());
}
