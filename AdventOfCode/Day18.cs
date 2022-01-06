public class Day18 : Day
{
    public Day18() : base("inputs/input-18.txt")
    {
    }

    public override void Run()
    {
        var numbers = new List<SnailNumber>();
        var depth = 0;
        foreach (var item in _input)
        {
            var num = ParseNumber(depth, item);
            numbers.Add(num);
        }

        static SnailNumber ParseNumber(int depth, string item)
        {
            if (item[0] == '[')
            {
                depth++;
                // In pair

                var localDepth = 0;
                // Find matching comma
                var commaIndex = -1;
                for (int i = 0; i < item.Length; i++)
                {
                    char c = item[i];
                    switch (c)
                    {
                        case '[':
                            localDepth++;
                            break;
                        case ']':
                            localDepth--;
                            break;
                        case ',' when localDepth == 1:
                            commaIndex = i;
                            break;
                        default:
                            break;
                    }

                }

                var left = item[1..commaIndex];
                var right = item[(commaIndex + 1)..^1];
                var nLeft = ParseNumber(depth, left);
                var nRight = ParseNumber(depth, right);
                var num = new SnailPair(nLeft, nRight);
                nLeft.Side = SIDE.LEFT;
                nRight.Side = SIDE.RIGHT;

                return num;
            }
            else
            {
                var value = Convert.ToInt32(char.GetNumericValue(item[0]));
                return new SnailLiteral { Value = value };
            }
        }

        var maxMagnitude = 0;
        SnailNumber? bestLeft = null;
        SnailNumber? bestRight = null;
        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = 0; j < numbers.Count; j++)
            {
                if (i == j) continue;

                var left = ParseNumber(0, _input[i]);
                var right = ParseNumber(0, _input[j]);

                var sn = left.Add(right);
                while (Reduce(sn)) ;
                if (sn.Magnitude() > maxMagnitude)
                {
                    bestLeft = numbers[i];
                    bestRight = numbers[j];
                    maxMagnitude = sn.Magnitude();
                }
            }
        }
        System.Console.WriteLine($"{maxMagnitude} with {bestLeft} + {bestRight}");
    }


    public bool Reduce(SnailNumber num)
    {
        return ReduceExplosions(num, 0) || ReduceSplit(num);
    }

    public bool ReduceExplosions(SnailNumber num, int depth)
    {
        bool reduced = false;
        if (num is SnailPair pair)
        {
            if (depth >= 4)
            {
                // Do the explode
                var valueLeft = ((SnailLiteral)pair.Left).Value;
                var valueRight = ((SnailLiteral)pair.Right).Value;

                if (pair.Parent is not null)
                {
                    pair.Parent.ExplodeLeft(valueLeft, pair.Side);
                    pair.Parent.ExplodeRight(valueRight, pair.Side);
                    if (pair.Side == SIDE.LEFT)
                    {
                        pair.Parent.Left = new SnailLiteral { Value = 0, Side = SIDE.LEFT };
                    }
                    if (pair.Side == SIDE.RIGHT)
                    {
                        pair.Parent.Right = new SnailLiteral { Value = 0, Side = SIDE.RIGHT };
                    }
                }
                reduced = true;
            }
            if (!reduced)
                reduced |= ReduceExplosions(pair.Left, depth + 1);
            if (!reduced)
                reduced |= ReduceExplosions(pair.Right, depth + 1);

        }

        return reduced;
    }

    public bool ReduceSplit(SnailNumber num)
    {
        bool reduced = false;
        if (num is SnailLiteral lit)
        {
            if (lit.Value >= 10)
            {
                // SPLIT!!!
                var leftLit = Math.Floor((double)lit.Value / 2);
                var rightLit = Math.Ceiling((double)lit.Value / 2);
                var leftSnail = new SnailLiteral { Value = Convert.ToInt32(leftLit), Side = SIDE.LEFT };
                var rightSnail = new SnailLiteral { Value = Convert.ToInt32(rightLit), Side = SIDE.RIGHT, Parent = lit.Parent };
                var newPair = new SnailPair(leftSnail, rightSnail)
                {
                    Side = lit.Side,
                };
                if (lit.Side == SIDE.LEFT)
                    lit.Parent.Left = newPair;
                if (lit.Side == SIDE.RIGHT)
                    lit.Parent.Right = newPair;
                reduced = true;
            }
        }
        else if (num is SnailPair pair)
        {
            if (!reduced) reduced |= ReduceSplit(pair.Left);
            if (!reduced) reduced |= ReduceSplit(pair.Right);
        }

        return reduced;
    }

    public abstract class SnailNumber
    {

        public SnailPair? Parent;

        public abstract int Magnitude();

        public SIDE Side = SIDE.NONE;

        public abstract void AddNumber(int value, SIDE side);

        public SnailNumber Add(SnailNumber newNumber)
        {
            var newTop = new SnailPair(this, newNumber);
            this.Parent = newTop;
            newNumber.Parent = newTop;
            this.Side = SIDE.LEFT;
            newNumber.Side = SIDE.RIGHT;

            return newTop;
        }
    }

    public enum SIDE
    {
        NONE,
        LEFT,
        RIGHT,
    }

    public class SnailPair : SnailNumber
    {
        private SnailNumber right;
        private SnailNumber left;

        public SnailNumber Right
        {
            get => right;
            set
            {
                right = value;
                right.Parent = this;
            }
        }
        public SnailNumber Left
        {
            get => left;
            set
            {
                left = value;
                left.Parent = this;
            }
        }

        public SnailPair(SnailNumber left, SnailNumber right)
        {
            Right = right;
            Left = left;
        }

        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }

        public override int Magnitude() => 3 * Left.Magnitude() + 2 * Right.Magnitude();

        public void ExplodeRight(int value, SIDE fromSide)
        {
            // System.Console.WriteLine($"Exploding {this} with {value} from {fromSide} to Right");
            if (fromSide == SIDE.LEFT)
            {
                Right.AddNumber(value, SIDE.LEFT);
            }
            if (fromSide == SIDE.RIGHT)
            {
                if (Parent is not null)
                    // Add to left side
                    Parent.ExplodeRight(value, Side);
            }
        }

        public void ExplodeLeft(int value, SIDE fromSide)
        {
            // System.Console.WriteLine($"Exploding {this} with {value} from {fromSide} to Left");
            if (fromSide == SIDE.RIGHT)
            {
                // Add to left side
                Left.AddNumber(value, SIDE.RIGHT);
            }
            if (fromSide == SIDE.LEFT)
            {
                // Add to left side
                if (Parent is not null)
                    Parent.ExplodeLeft(value, Side);
            }
        }

        public override void AddNumber(int value, SIDE side)
        {
            // System.Console.WriteLine($"{this}: Adding value {value} to side {side}");
            if (side == SIDE.RIGHT) Right.AddNumber(value, side);
            if (side == SIDE.LEFT) Left.AddNumber(value, side);
        }
    }

    public class SnailLiteral : SnailNumber
    {
        public int Value { get; set; }

        public override void AddNumber(int value, SIDE side) => Value += value;

        public override int Magnitude() => Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}