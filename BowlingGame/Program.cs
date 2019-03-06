using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new game and iterate through it 10 times, once for each frame
            Game bowlingGame = new Game();
            bowlingGame.Frames = new List<Frame>();
            for (int i =0; i<10; i++)
            {
                Frame frame = new Frame();
                frame.FrameRound = i + 1;
                int score;
                var isInt = false;
                //Ensure valid user input
                do
                {
                    Console.WriteLine("Score for first shot on Frame {0}", frame.FrameRound);
                    var isItAnInt = int.TryParse(Console.ReadLine(), out score);
                    isInt = isItAnInt;
                }
                while (score < 0 || score > 10 || isInt == false);
                //Check to see if previous Frame was a spare or strike and add score if it was
                if (frame.FrameRound > 1)
                {
                    var previousFrame = bowlingGame.Frames[bowlingGame.Frames.Count - 1];
                    if(previousFrame.IsSpare == true && previousFrame.Spare.HasAddedOneScore < 1)
                    {
                        frame.UpdatePreviousFrameFinalScore(score, previousFrame);
                        previousFrame.Spare.HasAddedOneScore++;
                    }
                    if (previousFrame.IsStrike == true && previousFrame.Strike.HasAddedTwoScores < 2)
                    {
                        if(previousFrame.Strike.HasAddedTwoScores == 0)
                        {
                            frame.UpdatePreviousFrameFinalScore(score, previousFrame);
                            previousFrame.Strike.HasAddedTwoScores++;
                        }
                        else
                        {
                            frame.UpdateBeforePreviousFrameFinalScore(score, previousFrame);
                            previousFrame.Strike.HasAddedTwoScores++;
                        }
                    }
                }
                //Check if frame before previous frame was a strike and add score if it was
                if (frame.FrameRound > 2)
                {
                    var beforePreviousFrame = bowlingGame.Frames[bowlingGame.Frames.Count - 2];
                    if (beforePreviousFrame.IsStrike == true && beforePreviousFrame.Strike.HasAddedTwoScores < 2)
                    {
                        frame.UpdateBeforePreviousFrameFinalScore(score, beforePreviousFrame);
                        beforePreviousFrame.Strike.HasAddedTwoScores++;
                    }

                }
                frame.GetScoreAfterFirstShot(score);
                //Check to see if you got a strike on first round, if so go on to the next frame
                if(frame.FrameScore == 10 && frame.FrameRound != 10)
                {
                    bowlingGame.Frames.Add(frame);
                    continue;
                }
                int firstScore = score;
                //Used to get valid user input on 10th frame if first roll of 10th round was a strike
                if(frame.FrameRound == 10 && frame.IsStrike == true)
                {
                    var isInteger = false;
                    do
                    {
                        Console.WriteLine("Score for second shot on Frame {0}", i + 1);
                        var isItAnInt = int.TryParse(Console.ReadLine(), out score);
                        isInteger = isItAnInt;
                    }
                    while (score < 0 || score > 10  || isInteger == false);
                }
                //Used to get valid user input on second shot
                else
                {
                    var isInteger = false;
                    do
                    {
                        Console.WriteLine("Score for second shot on Frame {0}", i + 1);
                        var isItAnInt = int.TryParse(Console.ReadLine(), out score);
                        isInteger = isItAnInt;
                    }
                    while ((score < 0 || score > 10 - firstScore || isInteger == false) && firstScore != 10);
                }
                frame.GetScoreAfterSecondShot(score, frame.FrameScore);
                int secondScore = score;
                //Check to see if second roll should be added to previous frame if previous frame was a strike
                if (frame.FrameRound > 1)
                {
                    var previousFrame = bowlingGame.Frames[bowlingGame.Frames.Count - 1];
                    if (previousFrame.IsStrike == true && previousFrame.Strike.HasAddedTwoScores < 2)
                    {
                            frame.UpdateBeforePreviousFrameFinalScore(score, previousFrame);
                            previousFrame.Strike.HasAddedTwoScores++;
                    }
                }
                //Used to get valid user input on third shot of 10th frame if 10th frame is a spare
                if(frame.FrameRound == 10 && frame.IsSpare == true)
                {
                    var isInteger = false;
                    do
                    {
                        Console.WriteLine("Score for third shot on Frame {0}", i + 1);
                        var isItAnInt = int.TryParse(Console.ReadLine(), out score);
                        isInteger = isItAnInt;
                    }
                    while (score < 0 || score > 10 || isInteger == false);
                    frame.FrameScore = frame.FrameScore + score;
                }
                //Used to get valid user input on third shot of 10th frame if 10th frame is a strike
                if (frame.FrameRound == 10 && frame.IsStrike == true)
                {
                    var isInteger = false;
                    do
                    {
                        Console.WriteLine("Score for third shot on Frame {0}", i + 1);
                        var isItAnInt = int.TryParse(Console.ReadLine(), out score);
                        isInteger = isItAnInt;
                    }
                    while (score < 0 || score > 10 - secondScore || isInteger == false);
                    frame.FrameScore = frame.FrameScore + score;
                }
                bowlingGame.Frames.Add(frame);
            }
            //Loop through every frame once game is done and add up the total score
            foreach(var frame in bowlingGame.Frames)
            {
                bowlingGame.FinalScore = bowlingGame.FinalScore + frame.FrameScore;
            }
            //Display message of score and how to exit the game
            Console.WriteLine("Congratulations your final bowling score is: {0}", bowlingGame.FinalScore);
            Console.WriteLine("Press Esc to exit application");
            bool wasEscPressed = false;
            do { var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    wasEscPressed = true;
                    Environment.Exit(0);
                }
            }
            while (wasEscPressed == false);
        }
    }
}
