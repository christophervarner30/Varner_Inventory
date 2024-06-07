using System;
using System.Collections.Generic;

// Interface for shippable items
public interface IShippable
{
    decimal ShipCost { get; }
    string Product { get; }
}

// Base class for shippable products
public abstract class ShippableProduct : IShippable
{
    public abstract decimal ShipCost { get; }
    public abstract string Product { get; }
}

// Concrete product classes
public class Bicycle : ShippableProduct
{
    public override decimal ShipCost => 9.50m;
    public override string Product => "Bicycle";
}

public class LawnMower : ShippableProduct
{
    public override decimal ShipCost => 24.00m;
    public override string Product => "Lawn Mower";
}

public class CellPhone : ShippableProduct
{
    public override decimal ShipCost => 5.95m;
    public override string Product => "Cell Phone";
}

public class BaseballGlove : ShippableProduct
{
    public override decimal ShipCost => 3.23m;
    public override string Product => "Baseball Glove";
}

public class Crackers : ShippableProduct
{
    public override decimal ShipCost => 0.57m;
    public override string Product => "Crackers";
}

// Shipper class to manage shipments
public class Shipper
{
    private List<IShippable> _items = new List<IShippable>();

    public void Add(IShippable item)
    {
        if (_items.Count < 10)
        {
            _items.Add(item);
            Console.WriteLine($"1 {item.Product} has been added.");
        }
        else
        {
            Console.WriteLine("Shipment is full. Cannot add more items.");
        }
    }

    public void ListItems()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("Shipment is empty.");
            return;
        }

        Console.WriteLine("Shipment manifest:");

        var itemCounts = new Dictionary<string, int>();
        foreach (var item in _items)
        {
            if (itemCounts.ContainsKey(item.Product))
            {
                itemCounts[item.Product]++;
            }
            else
            {
                itemCounts[item.Product] = 1;
            }
        }

        foreach (var kvp in itemCounts)
        {
            Console.WriteLine($"{kvp.Value} {kvp.Key}{(kvp.Value > 1 ? "s" : "")}");
        }
    }

    public decimal ComputeShippingCharges()
    {
        decimal totalCost = 0;
        foreach (var item in _items)
        {
            totalCost += item.ShipCost;
        }
        return totalCost;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var shipper = new Shipper();

        while (true)
        {
            Console.WriteLine("\nChoose from the following options:");
            Console.WriteLine("1. Add a Bicycle to the shipment");
            Console.WriteLine("2. Add a Lawn Mower to the shipment");
            Console.WriteLine("3. Add a Baseball Glove to the shipment");
            Console.WriteLine("4. Add Crackers to the shipment");
            Console.WriteLine("5. List Shipment Items");
            Console.WriteLine("6. Compute Shipping Charges");
            Console.WriteLine("7. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    shipper.Add(new Bicycle());
                    break;
                case "2":
                    shipper.Add(new LawnMower());
                    break;
                case "3":
                    shipper.Add(new BaseballGlove());
                    break;
                case "4":
                    shipper.Add(new Crackers());
                    break;
                case "5":
                    shipper.ListItems();
                    break;
                case "6":
                    decimal totalCost = shipper.ComputeShippingCharges();
                    Console.WriteLine($"Total shipping cost for this order is: {totalCost:C}");
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}