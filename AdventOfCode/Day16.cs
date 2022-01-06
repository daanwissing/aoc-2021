using System.Text;

public class Day16 : Day
{
    public Day16() : base("inputs/input-16.txt")
    {

    }

    public override void Run()
    {
        var p = new Packet(_input[0]);
        System.Console.WriteLine($"Version: {p.Version}");
        System.Console.WriteLine($"Type: {p.Type}");
        System.Console.WriteLine($"SubLength: {p.SubPacketLength}");
        System.Console.WriteLine($"SubPackets: {p.SubPacketTotal}");
    }


    public class Packet
    {
        public byte[] PacketBytes { get; }
        public string bitString { get; }

        public int Version { get; }

        public int Type { get; }

        public int? SubPacketLength { get; }

        public int? SubPacketTotal { get; }

        public List<Packet> SubPackets { get; } = new();

        public int startPayload { get; }

        public char lengthType {get;}

        public Packet(string packetString)
        {
            PacketBytes = Convert.FromHexString(packetString);
            bitString = string.Concat(PacketBytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            Version = PacketBytes[0] >> 5;
            Type = (PacketBytes[0] >> 3) % 16;

            if (Version != 4)
            {
                lengthType = bitString[6];
                var start = 7;
                if (lengthType == '0')
                {
                    var end = start + 15;
                    var lengthString = bitString[start..end];
                    SubPacketLength = getIntFromString(lengthString);
                    startPayload = end;
                }
                if (lengthType == '1')
                {
                    var end = start + 11;
                    var totalString = bitString[start..end];
                    SubPacketTotal = getIntFromString(totalString);
                    startPayload = end;
                }
            } else 
            {
                startPayload = 7;
            }
        }

        private void ScanSubPackets()
        {
            if (Version == 1)
            {
                var start = startPayload;

                if (lengthType == '0') // Go until SubPacketLength is reached
                {
                    string subPacketString = bitString[start..(SubPacketLength.Value + start)];

                    
                }
            }
        }

        private int getIntFromString(string s)
        {
            var result = 0;
            for (int index = 0; index < s.Length; index++)
            {
                result *= 2;
                if (s[index] == '1') result++;
            }
            return result;
        }

        private byte[] getBytesFromString(string s)
        {
            var l = s.Length;
            // Multiple of 4
            var formS = s.PadLeft(l + (4 - (l % 4)), '0');
            var result = new List<byte>();
            byte currentByte = 0;
            for (int index = 0; index < formS.Length; index++)
            {
                currentByte *= 2;
                if (formS[index] == '1')
                    currentByte += 1;
                if (index % 16 == 3)
                {
                    result.Add(currentByte);
                    currentByte = 0;
                }
            }
            System.Console.WriteLine();

            return result.ToArray();
        }

    }
}

