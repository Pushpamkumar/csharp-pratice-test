using System;

// Entity class representing a sale transaction
class SaleTransaction
{
    public string InvoiceNo { get; set; } // Unique identifier for the invoice
    public string CustomerName { get; set; } // Name of the customer
    public string ItemName { get; set; } // Name of the item sold
    public int Quantity { get; set; } // Quantity of items
    public decimal PurchaseAmount { get; set; } // Total purchase cost
    public decimal SellingAmount { get; set; } // Total selling amount
    public string ProfitOrLossStatus { get; set; } // PROFIT / LOSS / BREAK-EVEN (calculated)
    public decimal ProfitOrLossAmount { get; set; } // Profit or loss amount (calculated)
    public decimal ProfitMarginPercent { get; set; } // Profit margin percentage (calculated)
}

// Main program class
class Program
{
    // Static storage for the last transaction (no collections used)
    static SaleTransaction LastTransaction;
    static bool HasLastTransaction = false;

    // Main method - entry point of the application
    static void Main(string[] args)
    {
        // Infinite loop for menu-driven interface
        while (true)
        {
            // Display the menu
            Console.WriteLine("================== QuickMart Traders ==================");
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
            Console.WriteLine("2. View Last Transaction");
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");
            string option = Console.ReadLine();

            // Handle menu options using switch
            switch (option)
            {
                case "1":
                    CreateNewTransaction(); // Call method to create a new transaction
                    break;
                case "2":
                    ViewLastTransaction(); // Call method to view the last transaction
                    break;
                case "3":
                    CalculateProfitLoss(); // Call method to calculate and print profit/loss
                    break;
                case "4":
                    Console.WriteLine("Thank you. Application closed normally.");
                    return; // Exit the program normally
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Method to create a new transaction by capturing user inputs
    static void CreateNewTransaction()
    {
        // Input Invoice No
        Console.Write("Enter Invoice No: ");
        string invoiceNo = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(invoiceNo))
        {
            Console.WriteLine("Invoice No cannot be empty.");
            return;
        }

        // Input Customer Name
        Console.Write("Enter Customer Name: ");
        string customerName = Console.ReadLine();

        // Input Item Name
        Console.Write("Enter Item Name: ");
        string itemName = Console.ReadLine();

        // Input Quantity with validation
        Console.Write("Enter Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Quantity must be a positive integer.");
            return;
        }

        // Input Purchase Amount with validation
        Console.Write("Enter Purchase Amount (total): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
        {
            Console.WriteLine("Purchase Amount must be a positive number.");
            return;
        }

        // Input Selling Amount with validation
        Console.Write("Enter Selling Amount (total): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount) || sellingAmount < 0)
        {
            Console.WriteLine("Selling Amount must be a non-negative number.");
            return;
        }

        // Compute profit/loss
        string status;
        decimal profitLossAmount;
        if (sellingAmount > purchaseAmount)
        {
            status = "PROFIT";
            profitLossAmount = sellingAmount - purchaseAmount;
        }
        else if (sellingAmount < purchaseAmount)
        {
            status = "LOSS";
            profitLossAmount = purchaseAmount - sellingAmount;
        }
        else
        {
            status = "BREAK-EVEN";
            profitLossAmount = 0;
        }

        decimal profitMarginPercent = (profitLossAmount / purchaseAmount) * 100;

        // Create and store the transaction
        LastTransaction = new SaleTransaction
        {
            InvoiceNo = invoiceNo,
            CustomerName = customerName,
            ItemName = itemName,
            Quantity = quantity,
            PurchaseAmount = purchaseAmount,
            SellingAmount = sellingAmount,
            ProfitOrLossStatus = status,
            ProfitOrLossAmount = profitLossAmount,
            ProfitMarginPercent = profitMarginPercent
        };

        HasLastTransaction = true;

        // Display success message with computed values
        Console.WriteLine("Transaction saved successfully.");
        Console.WriteLine($"Status: {status}");
        Console.WriteLine($"Profit/Loss Amount: {profitLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {profitMarginPercent:F2}");
        Console.WriteLine("------------------------------------------------------");
    }

    // Method to view the last transaction
    static void ViewLastTransaction()
    {
        if (!HasLastTransaction)
        {
            Console.WriteLine("No transaction available. Please create a new transaction first.");
            return;
        }

        // Display the transaction details
        Console.WriteLine("-------------- Last Transaction --------------");
        Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
        Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
        Console.WriteLine($"Item: {LastTransaction.ItemName}");
        Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
        Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
        Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
        Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("------------------------------------------------------");
    }

    // Method to calculate profit/loss (recompute and print)
    static void CalculateProfitLoss()
    {
        if (!HasLastTransaction)
        {
            Console.WriteLine("No transaction available. Please create a new transaction first.");
            return;
        }

        // Recompute the values (though they don't change)
        decimal purchaseAmount = LastTransaction.PurchaseAmount;
        decimal sellingAmount = LastTransaction.SellingAmount;
        string status;
        decimal profitLossAmount;
        if (sellingAmount > purchaseAmount)
        {
            status = "PROFIT";
            profitLossAmount = sellingAmount - purchaseAmount;
        }
        else if (sellingAmount < purchaseAmount)
        {
            status = "LOSS";
            profitLossAmount = purchaseAmount - sellingAmount;
        }
        else
        {
            status = "BREAK-EVEN";
            profitLossAmount = 0;
        }

        decimal profitMarginPercent = (profitLossAmount / purchaseAmount) * 100;

        // Update the object
        LastTransaction.ProfitOrLossStatus = status;
        LastTransaction.ProfitOrLossAmount = profitLossAmount;
        LastTransaction.ProfitMarginPercent = profitMarginPercent;

        // Print the computed output
        Console.WriteLine("Profit/Loss recalculated.");
        Console.WriteLine($"Status: {status}");
        Console.WriteLine($"Profit/Loss Amount: {profitLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {profitMarginPercent:F2}");
        Console.WriteLine("------------------------------------------------------");
    }
}