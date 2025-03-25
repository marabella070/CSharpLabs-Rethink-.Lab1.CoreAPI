using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

using CoreAPI.Core.Models;


Workshop emptyWorkshop = Workshop.CreateEmpty();
Console.WriteLine(emptyWorkshop);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

const int workshopsNumber = 3;
var workshops = WorkshopRandomizer.GenerateMultipleWorkshops(workshopsNumber);

foreach (var workshop in workshops)
{
    Console.WriteLine(workshop);
    Console.WriteLine();
}
