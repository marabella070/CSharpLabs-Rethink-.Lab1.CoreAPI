using System;
using System.Collections.Generic;

namespace CoreAPI.Core.Models;

public static class WorkshopRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    // Generate a random workshop with random details
    public static Workshop GenerateRandomWorkshop()
    {
        // Randomly select a workshop name
        string productionName = productionsNames[random.Next(productionsNames.Count)];

        // Randomly choose a manager
        string manager = managerNames[random.Next(managerNames.Count)];

        // Generate random product list
        var productList = GenerateRandomProductList();

        uint workerCount;
        uint workshopId;

        try
        {
            Random random = new Random();

            // Получаем минимальные и максимальные значения
            uint minWorkerCount = Production.MIN_EMPLOYEES_NUMBER;
            uint maxWorkerCount = Production.MAX_EMPLOYEES_NUMBER;

            uint minWorkshopId = Workshop.MIN_ID_NUMBER;
            uint maxWorkshopId = Workshop.MAX_ID_NUMBER;

            // Проверяем, что значения подходят для int
            if (maxWorkerCount > int.MaxValue || maxWorkshopId > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("Range exceeds valid int range.");
            }

            // Преобразуем к int только перед использованием random.Next
            workerCount = minWorkerCount == maxWorkerCount
                ? minWorkerCount
                : (uint)random.Next((int)minWorkerCount, (int)maxWorkerCount + 1);

            workshopId = minWorkshopId == maxWorkshopId
                ? minWorkshopId
                : (uint)random.Next((int)minWorkshopId, (int)maxWorkshopId + 1);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new InvalidOperationException("The provided range is out of bounds for int.", ex);
        }

        // Generate a random schedule
        var (shifts, scheduleElements, brigadeCount) = GetRandomSchedule();

        // Generate random brigades
        var brigades = GenerateRandomBrigades(brigadeCount);

        // Create and return a new Workshop object with the random values
        return new Workshop(
            name: productionName,
            manager: manager,
            workerCount: workerCount,
            productList: productList,
            id: workshopId, 
            brigades: brigades,
            shifts: shifts,
            schedule: scheduleElements
        );
    }

    // Generate a list of random workshops based on the given count
    public static List<Workshop> GenerateMultipleWorkshops(int count)
    {
        var workshops = new List<Workshop>();

        for (int i = 0; i < count; i++)
        {
            workshops.Add(GenerateRandomWorkshop()); // Add a randomly generated workshop
        }

        return workshops;
    }

    // Generate a random list of products (3 or more products, with no duplicates)
    private static List<string> GenerateRandomProductList()
    {
        var productSet = new HashSet<string>();
        int productCount = random.Next(3, availableProducts.Count + 1);
        
        while (productSet.Count < productCount)
        {
            productSet.Add(availableProducts[random.Next(availableProducts.Count)]);
        }
        
        return productSet.ToList();
    }

    // Generate a random list of brigades with unique brigade names
    private static List<Brigade> GenerateRandomBrigades(int brigadeCount)
    {
        return Enumerable.Range(0, brigadeNames.Count)
            .OrderBy(_ => random.Next())  // Перемешиваем индексы случайным образом
            .Take(brigadeCount)           // Берем нужное количество
            .Select((index, i) => new Brigade((uint)(i + 1), brigadeNames[index]))
            .ToList();
    }

    // Select a random schedule (either ThreeShiftFourBrigade or ThreeShiftFiveBrigade)
    private static (List<Shift> Shifts, List<ScheduleElement> ScheduleElements, int BrigadeCount) GetRandomSchedule()
    {
        // Randomly choose between 4 or 5 brigades
        int brigadeCount = random.Next(4, 6); // Generates either 4 or 5

        // Use switch to handle the chosen brigade count
        switch (brigadeCount)
        {
            case 4:
                return (StandardSchedules.ThreeShiftFourBrigade.Shifts, StandardSchedules.ThreeShiftFourBrigade.ScheduleElements, 4);
            case 5:
                return (StandardSchedules.ThreeShiftFiveBrigade.Shifts, StandardSchedules.ThreeShiftFiveBrigade.ScheduleElements, 5);
            default:
                throw new InvalidOperationException("Unexpected brigade count selected.");
        }
    }

    // List of possible workshop names
    private static List<string> productionsNames = new List<string>
    {
        "Global AutoWorks", "Future Motors", "EcoTech Engineering", "Titan Industries", "Mega Components",
        "AutoFusion", "Prime Parts Factory", "SpeedWorks", "Precision Systems", "EcoDrive Manufacturing"
    };

    // List of available products for generation
    private static List<string> availableProducts = new List<string>
    {
        "Engine Blocks", "Transmission Systems", "Brake Discs", "Suspension Components", "Electric Vehicle Batteries",
        "Fuel Injectors", "Turbochargers", "Hybrid Motors", "Gearbox Assemblies", "Automotive Sensors"
    };

    // List of possible brigade names
    private static List<string> brigadeNames = new List<string>
    {
        "Alpha", "Bronson", "Chuck-Norris", "Vortex", "Titan", "Omega", "Delta", "Eagle", "Phoenix", "Falcon"
    };

    // List of possible manager names
    private static List<string> managerNames = new List<string>
    {
        "Michael Reynolds", "John Doe", "Sarah Connors", "Bruce Wayne", "Tony Stark", "Clark Kent", "Steve Rogers"
    };
}