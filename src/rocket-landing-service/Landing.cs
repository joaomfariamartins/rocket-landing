using System;

namespace RocketLanding
{
    public class Landing
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();

        public int X { get; set; }

        public int Y { get; set; }
    }
}
