public class Day12 : Day
{
    public Day12() : base("inputs/input-12.txt")
    {

    }

    public override void Run()
    {
        Dictionary<string, Cave> caves = new();
        foreach (var conn in _input)
        {
            var caveNames = conn.Split('-');
            Cave cave1;
            Cave cave2;
            string cave1Name = caveNames[0];
            string cave2Name = caveNames[1];
            
            if (caves.ContainsKey(cave1Name))
                cave1 = caves[cave1Name];
            else 
            {
                cave1 = new Cave(cave1Name);
                caves.Add(cave1Name, cave1);
            }

            if (caves.ContainsKey(cave2Name))
                cave2 = caves[cave2Name];
            else 
            {
                cave2 = new Cave(cave2Name);
                caves.Add(cave2Name, cave2);
            }

            cave1.ConnectedCaves.Add(cave2);
            cave2.ConnectedCaves.Add(cave1);
        }


        var startCave = caves["start"];
        var routes = new List<string>();
        VisitCave(startCave, "", false);

        void VisitCave(Cave cave, string route, bool visitedSmall)
        {
            if (cave.Name == "start" && !string.IsNullOrEmpty(route))
                return;
            if (cave.Name.All(char.IsLower) && route.Contains(cave.Name))
            {
                if (visitedSmall)
                    return; // End of the road, visited small cave already
                else 
                {
                    visitedSmall = true;
                }
            }
            route += $",{cave.Name}";
            if (cave.Name == "end") 
            {
                routes.Add(route); // Made it to the end!
                return;
            }

                
            foreach(var c in cave.ConnectedCaves)
                VisitCave(c, route, visitedSmall);
        }

        System.Console.WriteLine($"Number of routes: {routes.Count}");

    }
}


public class Cave
{
    public HashSet<Cave> ConnectedCaves { get; } = new();

    public string Name { get; }

    public Cave(string name) => Name = name;
}