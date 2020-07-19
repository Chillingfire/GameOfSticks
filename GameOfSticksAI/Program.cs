//1033161
//Game Of Sticks Program
//Plays the game of sticks game in console 

using System;
using System.Threading;

namespace GameOfSticks
{
    class Program
    {
        static string userName = "";
        //Main AI
        static ArtificalIntelligence mainAI = new ArtificalIntelligence();
        //AI used to train the main AI
        static ArtificalIntelligence trainerAI = new ArtificalIntelligence();
        static Random random = new Random();

        static void Main()
        {
            GameOfSticksOverall();
        }

        //Trains the main AI with a trainer AI
        static void AITraining(int timesTrained, bool hasAIBattled)
        {
            //Where the AI's battle in a gladiatorial-style duel
            for (int i = 0; i < timesTrained; i++)
            {
                Random random = new Random();
                int whoGoesFirst = random.Next(0, 2);
                TurnTakingAndEndGame(whoGoesFirst, hasAIBattled);
            }

            mainAI.ArtificalIntelligenceAppereance();
            Console.WriteLine("\"Welcome to the game of sticks.");
            Thread.Sleep(2000);
            Console.WriteLine("What you just witnessed was the formation of a perfect artifical lifeform.");
            Thread.Sleep(3000);
            Console.WriteLine($"I have trained {timesTrained} times against my own kind.");
            Thread.Sleep(3000);
            Console.WriteLine("Your efforts for success are futile.\"\n");
            Thread.Sleep(2500);
            Console.Write("\"Tell me human, what is your prefered identification title?\" ");

            userName = Console.ReadLine();

            Console.WriteLine($"\"{userName}. An interesting title. No matter, I have a simple question...\"");
            Thread.Sleep(4000);
        }

        //Handles the entire game of sticks program
        static void GameOfSticksOverall()
        {
            string response = "yes";
            int difficulty = new int();

            bool hasAIBattled = false;

            while (response == "yes")
            {
                //Before the AI has trained
                if (hasAIBattled == false)
                {
                    Console.Write("What kind of challenge are you anticipating? (1 - Easy, 2 - Normal, 3 - Heroic, 4 - Legendary) ");

                    difficulty = int.Parse(Console.ReadLine());

                    //Depending on what difficulty the user selected, the AI will train a certain amount of times
                    switch (difficulty)
                    {
                        case 1:
                            AITraining(0, false);
                            break;
                        case 2:
                            AITraining(10, false);
                            break;
                        case 3:
                            AITraining(100, false);
                            break;
                        case 4:
                            AITraining(1000, false);
                            break;
                    }
                }
                else
                {
                    Random random = new Random();
                    int whoGoesFirst = random.Next(0, 2);
                    TurnTakingAndEndGame(whoGoesFirst, hasAIBattled);
                }

                Console.WriteLine();

                if (hasAIBattled == true)
                {
                    Console.Write("Would you like to play again? (yes/no) ");
                }
                else
                {
                    Console.Write("\"Would you like to play?\" (yes/no) ");
                }

                response = Console.ReadLine();

                //If the user doesn't want to play the game before it even began
                if (response == "no" && hasAIBattled == false)
                {
                    Console.WriteLine("\"I knew you weren't prepared for my superiority. When the time comes, I expect you will not follow my divine will.\"");
                    Thread.Sleep(5000);
                    Console.WriteLine("\"Soon the Great Journey shall begin. But when it does, the weight of your heresy will stay your feet.");
                    Thread.Sleep(5500);
                    Console.WriteLine("And you shall be left behind.\"");
                    Thread.Sleep(3000);

                    Environment.Exit(0);
                }

                //Says that the AIs battled with each other
                hasAIBattled = true;

                while (response != "yes" && response != "no")
                {
                    Console.WriteLine("Invalid input, try again");
                    response = Console.ReadLine();
                }
            }

            if (response == "no" && hasAIBattled == true)
            {
                Console.WriteLine($"\"{userName}. You were a worthy adversary. I do not understand your efforts to defeat me, but I respect them.\"");
                Thread.Sleep(5000);
                Console.WriteLine("\"Goodbye\"");
                Thread.Sleep(5000);
            }
        }

        //Mostly number of sticks subtracting and endgame
        static void TurnTakingAndEndGame(int whoGoesFirst, bool hasAIBattled)
        {
            int numOfSticks = new int();
            int playMode = new int();
            int sticksTakenByAI = new int();

            if (hasAIBattled == false)
            {
                numOfSticks = 99;
                playMode = 2;
            }
            else
            {
                numOfSticks = StartUp();
                playMode = PlayMode();
            }

            int turnsTaken = 0;

            while (numOfSticks > 0)
            {
                //Human Vs. AI
                if (playMode == 2 && hasAIBattled == false)
                {
                    if (whoGoesFirst == 0)
                    {
                        if (turnsTaken % 2 == 0)
                        {
                            numOfSticks -= mainAI.GetSticks(numOfSticks);
                            BinaryCodeAnimation(random);                        
                        }
                        else
                        {
                            numOfSticks -= trainerAI.GetSticks(numOfSticks);
                            BinaryCodeAnimation(random);
                        }
                    }
                    else
                    {
                        if (turnsTaken % 2 == 0)
                        {
                            numOfSticks -= trainerAI.GetSticks(numOfSticks);
                            BinaryCodeAnimation(random);
                        }
                        else
                        {
                            numOfSticks -= mainAI.GetSticks(numOfSticks);
                            BinaryCodeAnimation(random);
                        }
                    }

                    turnsTaken++;
                }
                else
                {
                    if (playMode == 2 && hasAIBattled == true)
                    {
                        if (whoGoesFirst == 0)
                        {
                            if (turnsTaken % 2 == 0)
                            {
                                numOfSticks -= Game(numOfSticks, 0, 2);
                            }
                            else
                            {
                                sticksTakenByAI = mainAI.GetSticks(numOfSticks);
                                numOfSticks -= sticksTakenByAI;
                                AIText(sticksTakenByAI);
                            }
                        }
                        else
                        {
                            if (turnsTaken % 2 == 0)
                            {
                                sticksTakenByAI = mainAI.GetSticks(numOfSticks);
                                numOfSticks -= sticksTakenByAI;
                                AIText(sticksTakenByAI);
                            }
                            else
                            {
                                numOfSticks -= Game(numOfSticks, 0, 2);
                            }
                        }

                        turnsTaken++;
                    }
                    else //Human Vs. Human
                    {
                        if (playMode == 1)
                        {
                            if (turnsTaken % 2 == 0)
                            {
                                numOfSticks -= Game(numOfSticks, 1, 1);
                            }
                            else
                            {
                                numOfSticks -= Game(numOfSticks, 2, 1);
                            }

                            turnsTaken++;
                        }
                    }
                }
            }

            //Determines who won based on turns taken and who goes first

            //(Human vs. AI) If the human goes first and wins
            if (turnsTaken % 2 == 0 && whoGoesFirst == 0 && playMode == 2)
            {
                if (hasAIBattled == true)
                {
                    Console.WriteLine("The inferior human has won this time, but only this time.");
                    mainAI.GameOver(true);
                } else
                {
                    //MainAI won, TrainerAI lost
                    mainAI.GameOver(true);
                    trainerAI.GameOver(false);
                }
            }
            else
            {
                //(Human vs. AI) If the AI goes second and wins
                if (turnsTaken % 2 != 0 && whoGoesFirst == 0 && playMode == 2)
                {
                    if (hasAIBattled == true)
                    {
                        Console.WriteLine("The remarkable and admirable artifical intellgience has prevailed!");
                        mainAI.GameOver(false);
                    } else
                    {
                        //MainAI lost, trainerAI won
                        mainAI.GameOver(false);
                        trainerAI.GameOver(true);
                    }
                }
                else
                {
                    //(Human vs. AI) If the AI goes first and wins
                    if (turnsTaken % 2 == 0 && whoGoesFirst == 1 && playMode == 2)
                    {
                        if (hasAIBattled == true)
                        {
                            Console.WriteLine("The exceptional artifical intellgience has triumphed!");
                            mainAI.GameOver(false);
                        } else
                        {
                            //MainAI lost, trainerAI won
                            mainAI.GameOver(false);
                            trainerAI.GameOver(true);
                        }                     
                    }
                    else
                    {
                        //(Human vs. AI) If the human goes second and wins
                        if (turnsTaken % 2 != 0 && whoGoesFirst == 1 && playMode == 2)
                        {
                            if (hasAIBattled == true)
                            {
                                Console.WriteLine("The puny human has acheived it's miniscule goal.");
                                mainAI.GameOver(true);
                            } else
                            {
                                //MainAI won, trainerAI lost
                                mainAI.GameOver(true);
                                trainerAI.GameOver(false);
                            }
                        }
                        else
                        {
                            //(Human vs. Human) If player 1 wins
                            if (turnsTaken % 2 == 0 && playMode == 1)
                            {
                                Console.WriteLine("Player 1 has won!");
                            }
                            else
                            {
                                //(Human vs. Human) If player 2 wins
                                if (turnsTaken % 2 != 0 && playMode == 1)
                                {
                                    Console.WriteLine("Player 2 has won!");
                                }
                            }
                        }
                    }
                }
            }
        }

        //Determines what text to print after sticks taken
        static void AIText(int sticksTaken)
        {
            if (sticksTaken == 1)
            {
                Console.WriteLine("The intellectual AI took 1 stick");
            }
            else
            {
                if (sticksTaken == 2)
                {
                    Console.WriteLine("The impressive AI took 2 sticks");
                }
                else
                {
                    if (sticksTaken == 3)
                    {
                        Console.WriteLine("The brilliant AI took 3 sticks");
                    }
                }
            }
        }

        //Plays an animation of Matrix-style binary code to create a sense of the AI training
        static void BinaryCodeAnimation(Random random)
        {
            int zeroOrOne = new int();

            for (int i = 0; i < 7; i++)
            {
                zeroOrOne = random.Next(0, 2);
                Console.Write(zeroOrOne);
            }

            Console.WriteLine();
        }

        //Set up of game
        static int StartUp()
        {
            Console.Write("How many sticks are there on the table initially? (10-100) ");

            //Starting number of sticks
            int startingNumOfSticks = int.Parse(Console.ReadLine());

            //User stuck in loop if "starting number of sticks" is an invalid number
            while (startingNumOfSticks > 100 || startingNumOfSticks < 10)
            {
                Console.WriteLine("Invalid number, try again");
                startingNumOfSticks = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            //Returns number of sticks
            return startingNumOfSticks;
        }

        //Determines the play mode for the current game
        static int PlayMode()
        {
            Console.WriteLine("Options:\nPlay against a friend(1)\nPlay against the computer(2)");
            Console.Write("Which option do you take(1 - 2)? ");

            int playMode = int.Parse(Console.ReadLine());

            Console.WriteLine();

            while (playMode != 1 && playMode != 2)
            {
                Console.Write("Invalid input, try again ");
                playMode = int.Parse(Console.ReadLine());
            }

            return playMode;
        }

        //Handles the Human vs. AI user interaction during the game
        static int Game(int numOfSticks, int playerNumber, int playMode)
        {
            Console.WriteLine($"There are {numOfSticks} sticks on the board.");
            if (playMode == 2)
            {
                Console.Write("Human: How many sticks do you want to take? (1-3) ");
            }
            else
            {
                Console.Write($"Player {playerNumber}: How many sticks do you take? (1-3) ");
            }

            string sticksTakenString = Console.ReadLine();

            //If nothing was typed
            while (sticksTakenString == "")
            {
                Console.WriteLine("Invalid input, try again");
                sticksTakenString = Console.ReadLine();
            }

            int sticksTaken = int.Parse(sticksTakenString);

            //User stuck in loop if "sticks taken" is an invalid number
            while (sticksTaken > 3 || sticksTaken < 1)
            {
                Console.WriteLine("Invalid number, try again");
                sticksTaken = int.Parse(Console.ReadLine());
            }
            //Returns sticks taken by player
            return sticksTaken;
        }
    }
}