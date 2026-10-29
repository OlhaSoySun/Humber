using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//OSOY
using System.Globalization;


//OSOY

namespace ExpenseTracker
{
    class Program
    {
        private static bool cat;
        private static readonly object categores;

        static void Main(string[] args)
        {





            decimal totalSpent = 0m;
            decimal highestExpense = 0m;

            decimal foodTotal = 0m;
            decimal transportTotal = 0m;
            decimal utilitiesTotal = 0m;
            decimal entertainmentTotal = 0m;
            decimal otherTotal = 0m;

            string categoris = "Food, Transport, Utilities, " +
                           "Entertainment, Other:";
            string categoryInput;
            int expenseCount = 0;
            bool addAnother;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
           decimal MaximumRealisticAmount = 100000m;
            decimal budget = 0m;
           // string category = "";
            string categoryInput_l;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            bool running = true;
            while (running)
            {
                Console.WriteLine("=== Expense Tracker ===");
                Console.WriteLine("1. Add an expense");
                Console.WriteLine("2. View summary");
                Console.WriteLine("3. Set monthly budget");
                Console.WriteLine("0. Exit");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        addAnother = false;
                        do
                        {
                            Console.WriteLine("\nEnter expense details:");

                            // Description
                            string description;

                            while (true)
                            {
                                Console.Write("Description: ");
                                description = Console.ReadLine()?.Trim() ?? "";

                                if (!string.IsNullOrWhiteSpace(description))
                                    break;

                                Console.WriteLine("Description cannot be empty.");
                            }

                            // Amount
                            decimal amount;

                            while (true)
                            {
                                Console.Write("Amount: ");
                                string amountInput = Console.ReadLine() ?? "";

                                if (!decimal.TryParse(
                                        amountInput,
                                        NumberStyles.Number,
                                        CultureInfo.CurrentCulture,
                                        out amount))
                                {
                                    Console.WriteLine("Enter a valid numeric amount.");
                                    continue;
                                }
                                
                                decimal MaxAmaunt= BudgetRules.MaxAmount;
                                if (amount <= 0m || amount > MaxAmaunt)
                                {
                                    Console.WriteLine(
                                        $"Amount must be greater than zero and " +
                                        $"no more than {MaxAmaunt:N2}.");
                                    continue;
                                }

                                break;
                            }

                            // Category


                            while (true)
                            {
                                Console.Write($"Category: " + categoris);

                                categoryInput = Console.ReadLine()?.Trim() ?? "";
                                string categoris_l = categoris.ToLower();
                                categoryInput_l = categoryInput.ToLower();
                                if (categoris_l.Contains(categoryInput_l)
                                       && (categoryInput != ""))
                                {
                                    break;
                                }


                                else
                                {
                                    Console.WriteLine(
                                    "Invalid category. Please try again.");

                                }


                            }

                            DateTime expenseDate;

                            while (true)
                            {
                                Console.Write("Date (yyyy-MM-dd, Enter for today): ");
                                string dateInput = Console.ReadLine()?.Trim() ?? "";

                                if (dateInput == "")
                                {
                                    expenseDate = DateTime.Today;
                                    break;
                                }

                                if (!DateTime.TryParseExact(
                                        dateInput,
                                        "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out expenseDate))
                                {
                                    Console.WriteLine(
                                        "Invalid date. Use yyyy-MM-dd.");
                                    continue;
                                }

                                if (expenseDate.Date > DateTime.Today)
                                {
                                    Console.WriteLine(
                                        "Future dates are not allowed.");
                                    continue;
                                }

                                break;
                            }

                            //// Optional nullable note
                            string note;
                            Console.Write("Optional note (press Enter to skip): ");
                            note = Console.ReadLine()?.Trim();

                            if (string.IsNullOrWhiteSpace(note))
                                note = null;

                            //// Classify the amount into a size band using BudgetRules.ClassifyAmount and display it.

                            try
                            {
                                string sizeBand = BudgetRules.ClassifyAmount(amount);

                                Console.WriteLine("\nExpense recorded:");
                                Console.WriteLine($"Description: {description}");
                                Console.WriteLine($"Amount: {amount:C}");
                                string categoryInput_name = char.ToUpper(categoryInput_l[0]) + categoryInput_l.Substring(1);

                                Console.WriteLine($"Category:{categoryInput_name} ");
                                Console.WriteLine($"Date: {expenseDate:yyyy-MM-dd}");
                                Console.WriteLine($"Size band: {sizeBand}");

                              //  if (note is notenull)
                               //     Console.WriteLine($"Note: {note}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    $"Budget rule error: {ex.Message}");
                                Console.WriteLine(
                                    "Expense was not added. Please try again.");
                                continue;
                            }

                            // Update running totals
                            totalSpent += amount;
                            expenseCount++;

                            if (amount > highestExpense)
                                highestExpense = amount;

                            


                            if (categoryInput_l == "food")

                            { foodTotal += amount; }



                            if (categoryInput_l == "transport")
                                transportTotal += amount;



                            if (categoryInput_l == "utilities")
                                utilitiesTotal += amount;



                            if (categoryInput_l == "entertainment")
                                entertainmentTotal += amount;



                            if (categoryInput_l == "other")
                                otherTotal += amount;

                            // Ask whether to add another expense
                            while (true)
                            {
                                Console.Write("\nAdd another expense? (y/n): ");
                                string answer =
                                    Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";

                                if (answer == "y")
                                {
                                    addAnother = true;
                                    break;
                                }

                                if (answer == "n")
                                {
                                    addAnother = false;
                                    break;
                                }

                                Console.WriteLine("Please enter y or n.");
                            }

                        } while (addAnother);



                        break;
                    case "2":
                        Console.WriteLine("\n=== Expense Summary ===");




                        if (expenseCount == 0)
                        {
                            Console.WriteLine("No expenses have been recorded.");
                            Console.WriteLine("Total spent: 0.00");
                            Console.WriteLine("Average expense: N/A");
                            Console.WriteLine("Highest expense: N/A");
                        }
                        else
                        {
                            decimal averageExpense = totalSpent / expenseCount;

                            Console.WriteLine($"Expense count: {expenseCount}");
                            Console.WriteLine($"Total spent: {totalSpent:C}");
                            Console.WriteLine($"Average expense: {averageExpense:C}");
                            Console.WriteLine($"Highest expense: {highestExpense:C}");
                            if (budget != 0)
                            {
                                Console.WriteLine("\n=== Spending by Category ===");
                                Console.WriteLine($"Food: {foodTotal:C}");
                                Console.WriteLine($"Transport: {transportTotal:C}");
                                Console.WriteLine($"Utilities: {utilitiesTotal:C}");
                                Console.WriteLine($"Entertainment: {entertainmentTotal:C}");
                                Console.WriteLine($"Other: {otherTotal:C}");

                                Console.WriteLine("\n=== Budget Report ===");
                                Console.WriteLine($"Budget: {budget:C}");
                                Console.WriteLine($"Total spent: {totalSpent:C}");
                                //decimal remaining = budget - totalSpent;

                                //if (remaining >= 0m)
                                //    Console.WriteLine($"Remaining budget: {remaining:C}");
                                //else if (remaining < 0m)
                                //    Console.WriteLine($"Over budget by: {-remaining:C}");
                                //// 3-Report status with BudgetRules.BudgetStatus: over budget, almost out (less than 10% remaining), or on track
                                try
                                {
                                    string status = BudgetRules.BudgetStatus(
                                        totalSpent, budget);

                                    //      Console.WriteLine($"Budget status: {status}");
                                    Console.WriteLine(status);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(
                                        $"Could not calculate budget status: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Budget is not set. Spending by category is not showing");

                            }

                            break;

                        }

                        Console.WriteLine("\nThank you for using Expense Tracker!");

                        break;
                    case "3":




                        while (true)
                        {
                            Console.Write("\nEnter your total budget: ");
                            string budgetInput = Console.ReadLine() ?? "";

                            if (!decimal.TryParse(
                                    budgetInput,
                                    NumberStyles.Number,
                                    CultureInfo.CurrentCulture,
                                    out budget))
                            {
                                Console.WriteLine("Enter a valid numeric budget.");
                                continue;
                            }

                            if (budget <= 0m || budget > MaximumRealisticAmount)
                            {
                                Console.WriteLine(
                                    $"Budget must be greater than zero and " +
                                    $"no more than {MaximumRealisticAmount:N2}.");
                                continue;
                            }

                            break;
                        }
                        break;
                    case "0":
                        running = false; // condition of the entering from sikle ==18
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;

                }


                //       Console.ReadKey();

            }



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
