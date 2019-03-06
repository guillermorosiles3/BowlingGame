using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Strike
    {
        public int StrikeScore { get { return 10; } }
        public int FirstShot { get; set; }
        public int SecondShot { get; set; }
        public int HasAddedTwoScores { get; set; }

    }
}
