using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Frame
    {
        public int FrameScore { get; set; }
        public int FrameRound { get; set; }

        public bool IsStrike { get; set; }
        public bool IsSpare { get; set; }
        public bool IsOpenFrame { get; set; }
        public Spare Spare {get; set;}
        public Strike Strike { get; set; }
        public OpenFrame OpenFrame { get; set; }
        public int FirstScore { get; set; }
        public int SecondScore { get; set; }
        public int ThirdScore { get; set; }
        //Returns current frame score after first roll
        public int GetScoreAfterFirstShot(int firstShot)
        {
            if(firstShot == 10)
            {
                IsStrike = true;
                Strike strike = new Strike();
                Strike = strike;
                FrameScore = Strike.StrikeScore;
                FirstScore = firstShot;
                return FrameScore;
            }
            else
            {
                FrameScore = firstShot;
                FirstScore = firstShot;
                return FrameScore;
            }
        }
        //Returns current frame score after second roll
        public int GetScoreAfterSecondShot(int secondShot, int totalScoreSoFar)
        {
            if (secondShot + totalScoreSoFar == 10)
            {
                IsSpare = true;
                Spare spare = new Spare();
                Spare = spare;
                FrameScore = Spare.SpareScore;
                SecondScore = secondShot;
                return FrameScore;
            }
            else
            {
                IsOpenFrame = true;
                OpenFrame openFrame = new OpenFrame();
                OpenFrame = openFrame;
                FrameScore = secondShot + totalScoreSoFar;
                SecondScore = secondShot;
                return FrameScore;
            }
        }
        //Updates the score of previous frame if frame was a strike or spare and meets certain conditions
        public int UpdatePreviousFrameFinalScore(int firstShotAfter, Frame frame)
        {
            if (frame.IsStrike == true)
            {
                frame.FrameScore = frame.FrameScore + firstShotAfter;
                return frame.FrameScore;
            }
            else if(frame.IsSpare == true)
            {
                frame.FrameScore = frame.FrameScore + firstShotAfter;
                return frame.FrameScore;
            }
            else
            {
                return frame.FrameScore;
            }
        }
        //Updates the score of the frame prior to the previous frame if that frame was a strike and meets certain conditions
        public int UpdateBeforePreviousFrameFinalScore(int secondShotAfter, Frame frame)
        {
                frame.FrameScore = frame.FrameScore + secondShotAfter;
                return frame.FrameScore;
        }
    }
}
