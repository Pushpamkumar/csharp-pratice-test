using System;

// Entity class representing a patient bill
class PatientBill
{
    public string BillId { get; set; } // Unique identifier for the bill
    public string PatientName { get; set; } // Name of the patient
    public bool HasInsurance { get; set; } // Whether the patient has insurance
    public decimal ConsultationFee { get; set; } // Fee for consultation
    public decimal LabCharges { get; set; } // Charges for lab tests
    public decimal MedicineCharges { get; set; } // Charges for medicines
    public decimal GrossAmount { get; set; } // Total amount before discount (calculated)
    public decimal DiscountAmount { get; set; } // Discount applied (calculated)
    public decimal FinalPayable { get; set; } // Final amount to pay (calculated)
}

// Main program class
class Program
{
    // Static storage for the last bill (no collections used)
    static PatientBill LastBill;
    static bool HasLastBill = false;

    // Main method - entry point of the application
    static void Main(string[] args)
    {
        // Infinite loop for menu-driven interface
        while (true)
        {
            // Display the menu
            Console.WriteLine("================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");
            string option = Console.ReadLine();

            // Handle menu options using switch
            switch (option)
            {
                case "1":
                    CreateNewBill(); // Call method to create a new bill
                    break;
                case "2":
                    ViewLastBill(); // Call method to view the last bill
                    break;
                case "3":
                    ClearLastBill(); // Call method to clear the last bill
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

    // Method to create a new bill by capturing user inputs
    static void CreateNewBill()
    {
        // Input Bill ID
        Console.Write("Enter Bill Id: ");
        string billId = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(billId))
        {
            Console.WriteLine("Bill Id cannot be empty.");
            return;
        }

        // Input Patient Name
        Console.Write("Enter Patient Name: ");
        string patientName = Console.ReadLine();

        // Input Insurance status
        Console.Write("Is the patient insured? (Y/N): ");
        string insured = Console.ReadLine().ToUpper();
        bool hasInsurance = false;
        if (insured == "Y")
        {
            hasInsurance = true;
        }
        else if (insured == "N")
        {
            hasInsurance = false;
        }
        else
        {
            Console.WriteLine("Invalid input for insurance. Please enter Y or N.");
            return;
        }

        // Input Consultation Fee with validation
        Console.Write("Enter Consultation Fee: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal consultationFee) || consultationFee <= 0)
        {
            Console.WriteLine("Consultation Fee must be a positive number.");
            return;
        }

        // Input Lab Charges with validation
        Console.Write("Enter Lab Charges: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal labCharges) || labCharges < 0)
        {
            Console.WriteLine("Lab Charges must be a non-negative number.");
            return;
        }

        // Input Medicine Charges with validation
        Console.Write("Enter Medicine Charges: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal medicineCharges) || medicineCharges < 0)
        {
            Console.WriteLine("Medicine Charges must be a non-negative number.");
            return;
        }

        // Calculate amounts
        decimal grossAmount = consultationFee + labCharges + medicineCharges;
        decimal discountAmount = hasInsurance ? grossAmount * 0.10m : 0;
        decimal finalPayable = grossAmount - discountAmount;

        // Create and store the bill
        LastBill = new PatientBill
        {
            BillId = billId,
            PatientName = patientName,
            HasInsurance = hasInsurance,
            ConsultationFee = consultationFee,
            LabCharges = labCharges,
            MedicineCharges = medicineCharges,
            GrossAmount = grossAmount,
            DiscountAmount = discountAmount,
            FinalPayable = finalPayable
        };

        HasLastBill = true;

        // Display success message with calculated amounts
        Console.WriteLine("Bill created successfully.");
        Console.WriteLine($"Gross Amount: {grossAmount:F2}");
        Console.WriteLine($"Discount Amount: {discountAmount:F2}");
        Console.WriteLine($"Final Payable: {finalPayable:F2}");
        Console.WriteLine("------------------------------------------------------------");
    }

    // Method to view the last bill
    static void ViewLastBill()
    {
        if (!HasLastBill)
        {
            Console.WriteLine("No bill available. Please create a new bill first.");
            return;
        }

        // Display the bill details
        Console.WriteLine("----------- Last Bill -----------");
        Console.WriteLine($"BillId: {LastBill.BillId}");
        Console.WriteLine($"Patient: {LastBill.PatientName}");
        Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
        Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
        Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
        Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
        Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
        Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
        Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("------------------------------------------------------------");
    }

    // Method to clear the last bill
    static void ClearLastBill()
    {
        LastBill = null;
        HasLastBill = false;
        Console.WriteLine("Last bill cleared.");
    }
}