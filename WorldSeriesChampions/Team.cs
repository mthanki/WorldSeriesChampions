using System.Collections.Generic;

namespace WorldSeriesChampions
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> WinningYears { get; set; } = new List<int>();

        public override string ToString()
        {
            return $"{Name} {WinningYears} {Id}";
        }
    }
}
