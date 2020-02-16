using System;
using System.Collections.Generic;
using fantasy_hoops.Models;

namespace fantasy_hoops.Dtos
{
    public class BestLineupDto
    {
        public DateTime Date { get; set; }
        public List<LineupPlayerDto> Lineup { get; set; }
        public double FP { get; set; }
        public int Price { get; set; }
    }
}