using System;
using System.Linq;
//OSOY
using System.Globalization;
//using BudgetRulesTests;
//OSOY

namespace ExpenseTracker3
{
    class Program
    {
        static void Main(string[] args)
        {





                decimal totalSpent = 0m;
                decimal highestExpense = 0m;

                decimal foodTotal = 0m;
                decimal transportTotal = 0m;
                decimal utilitiesTotal = 0m;
                decimal entertainmentTotal = 0m;
                decimal otherTotal = 0m;
         
                
                int expenseCount = 0;
                bool addAnother;
     ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                decimal MaximumRealisticAmount = 100000m;
                decimal budget = 0m;
                string category ="";
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

                                if (amount <= 0m || amount > MaximumRealisticAmount)
                                {
                                    Console.WriteLine(
                                        $"Amount must be greater than zero and " +
                                        $"no more than {MaximumRealisticAmount:N2}.");
                                    continue;
                                }

                                break;
                            }

                            // Category
                            

                            while (true)
                            {
                                Console.Write(
                                    "Category (Food, Transport, Utilities, " +
                                    "Entertainment, Other): ");

                                string categoryInput =
                                    Console.ReadLine()?.Trim() ?? "";

                                category = categoryInput.ToLowerInvariant();
                                //switch
                                //                {
                                //            "food" => "Food",
                                //            "transport" => "Transport",
                                //            "utilities" => "Utilities",
                                //            "entertainment" => "Entertainment",
                                //            "other" => "Other",
                                //            _ => ""
                                //                };

                            if (category != "")
                                    break;

                                Console.WriteLine(
                                    "Invalid category. Please try again.");
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

                            //// Classify the expense using the supplied rules
                            //try
                            //{
                            //    string sizeBand = BudgetRules.ClassifyAmount(amount);

                            //    Console.WriteLine("\nExpense recorded:");
                            //    Console.WriteLine($"Description: {description}");
                            //    Console.WriteLine($"Amount: {amount:C}");
                            //    Console.WriteLine($"Category: {category}");
                            //    Console.WriteLine(
                            //        $"Date: {expenseDate:yyyy-MM-dd}");
                            //    Console.WriteLine($"Size band: {sizeBand}");

                            //    if (note is not null)
                            //                            Console.WriteLine($"Note: {note}");
                            //}
                            //catch (BudgetRuleException ex)
                            //{
                            //    Console.WriteLine(
                            //        $"Budget rule error: {ex.Message}");
                            //    Console.WriteLine(
                            //        "Expense was not added. Please try again.");
                            //    continue;
                            //}

                            // Update running totals
                            totalSpent += amount;
                            expenseCount++;

                            if (amount > highestExpense)
                                highestExpense = amount;

                            switch (category)
                            {
                                case "Food":
                                    foodTotal += amount;
                                    break;

                                case "Transport":
                                    transportTotal += amount;
                                    break;

                                case "Utilities":
                                    utilitiesTotal += amount;
                                    break;

                                case "Entertainment":
                                    entertainmentTotal += amount;
                                    break;

                                case "Other":
                                    otherTotal += amount;
                                    break;
                            }

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

                        } while (addAnother) ;                
       


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
                        }

                        Console.WriteLine("\n=== Spending by Category ===");
                        Console.WriteLine($"Food: {foodTotal:C}");
                        Console.WriteLine($"Transport: {transportTotal:C}");
                        Console.WriteLine($"Utilities: {utilitiesTotal:C}");
                        Console.WriteLine($"Entertainment: {entertainmentTotal:C}");
                        Console.WriteLine($"Other: {otherTotal:C}");

                        Console.WriteLine("\n=== Budget Report ===");
                        Console.WriteLine($"Budget: {budget:C}");
                        Console.WriteLine($"Total spent: {totalSpent:C}");

                        decimal remaining = budget - totalSpent;

                        if (remaining >= 0m)
                            Console.WriteLine($"Remaining budget: {remaining:C}");
                        else
                            Console.WriteLine($"Over budget by: {-remaining:C}");
                        // predifine check
                        //try
                        //{
                        //    string status = BudgetRules.BudgetStatus(
                        //        totalSpent, budget);

                        //    Console.WriteLine($"Budget status: {status}");
                        //}
                        //catch (BudgetRuleException ex)
                        //{
                        //    Console.WriteLine(
                        //        $"Could not calculate budget status: {ex.Message}");
                        //}

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
