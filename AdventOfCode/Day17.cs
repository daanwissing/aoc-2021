public class Day17 : Day
{
    public Day17() : base("inputs/input-17.txt")
    {

    }

    public override void Run()
    {
        var coors = _input[0][13..^0];
        var split = coors.Split(',');
        var targetX = split[0][2..^0].Split("..");
        (var x1, var x2) = (Convert.ToInt32(targetX[0]), Convert.ToInt32(targetX[1]));
        var targety = split[1][3..^0].Split("..");
        (var y1, var y2) = (Convert.ToInt32(targety[0]), Convert.ToInt32(targety[1]));

        var target = (x1: Math.Min(x1, x2),
                      x2: Math.Max(x1, x2),
                      y1: Math.Min(y1, y2),
                      y2: Math.Max(y1, y2));

        var bestHeight = 0;
        var hits = 0;

        for (int x = 1; x < 10000; x++)
        {
            for (int y = -500; y < 10000; y++)
            {
                var maxHeight = 0;
                var projectile = (x: 0, y: 0);
                var velocity = (x: x, y: y);
                var initialVelocity = velocity;

                var targetHit = false;

                while (!targetHit && projectile.y > target.y1 && projectile.x < target.x2)
                {
                    Step();
                }

                if (targetHit)
                {
                    System.Console.WriteLine($"Target hit: {targetHit} from {initialVelocity}, height {maxHeight}");
                    bestHeight = Math.Max(bestHeight, maxHeight);
                    hits++;
                }

                void Step()
                {
                    // System.Console.WriteLine($"At {projectile} with {velocity}");
                    projectile.x += velocity.x;
                    projectile.y += velocity.y;

                    targetHit = ((projectile.x >= target.x1 && projectile.x <= target.x2) &&
                        (projectile.y >= target.y1 && projectile.y <= target.y2));

                    maxHeight = Math.Max(maxHeight, projectile.y);
                    if (velocity.x > 0) velocity.x--;
                    if (velocity.x < 0) velocity.x++;
                    velocity.y--;

                }

            }
        }

        System.Console.WriteLine($"Best height: {bestHeight}");
        System.Console.WriteLine($"Hits: {hits}");

    }
}