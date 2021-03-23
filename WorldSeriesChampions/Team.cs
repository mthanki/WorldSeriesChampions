using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSeriesChampions
{
    public class Team
    {
        public string Name { get; set; }
        public List<int> WinningYears { get; set; } = new List<int>();

        public override string ToString()
        {
            return $"{Name} {WinningYears}";
        }
    }
}
