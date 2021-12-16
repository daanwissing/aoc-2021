using System.Diagnostics;

var sw = Stopwatch.StartNew();

//await Day1.Run();
//await Day2.Run();
// await Day3.Run();
// await Day4.Run();
//  await Day5.Run();
//  await Day6.Run();
// await Day7.Run();
// new Day8().Run();
// new Day9().Run();
// new Day10().Run();
// new Day11().Run();
// new Day12().Run();
new Day13().Run();



sw.Stop();
Console.WriteLine($"Elapsed: {sw.Elapsed}");