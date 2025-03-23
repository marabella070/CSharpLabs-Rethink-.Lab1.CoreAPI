using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

using CoreAPI.Core.Models;






using System.ComponentModel;

string someBrigadeString = $"1 Alpha";

var converter = TypeDescriptor.GetConverter(typeof(Brigade));

if (converter.CanConvertFrom(typeof(string)))
{
    Brigade? someBrigade = (Brigade?)converter.ConvertFrom(someBrigadeString);

    if (someBrigade is null)
    {
        Console.WriteLine("Not sucess :(");
    } else {
        Console.WriteLine(someBrigade);
    }
}


/*
var (shifts, scheduleElements) = StandardSchedules.ThreeShiftFiveBrigade;

// Creating Workshop
Workshop workshop = new Workshop(
    name: "Global AutoWorks",
    manager: "Michael Reynolds",
    workerCount: 1200,
    productList: new List<string>
    {
        "Engine Blocks",
        "Transmission Systems",
        "Brake Discs",
        "Suspension Components",
        "Electric Vehicle Batteries"
    },
    id: 1,
    new List<Brigade> {
        new Brigade(1, "Alpha"),
        new Brigade(2, "Bronson"),
        new Brigade(3, "Chuck-Norris"),
        new Brigade(4, "Vortex"),
        new Brigade(5, "Titan"),        
    },
    shifts: shifts,
    schedule: scheduleElements
);


Workshop bufferWorkshop = new Workshop(workshop);
bufferWorkshop.Name = "bla";

Console.WriteLine(Workshop.MAX_EMPLOYEES_NUMBER);


Console.WriteLine(bufferWorkshop);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

Console.WriteLine(workshop);

// workshop.ShowShortInfo(Console.Write);

*/




/*
workshop.ShowInfo(Console.Write);

Console.WriteLine();

workshop.ShowSchedule(Console.Write);
*/