using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfSticks
{
    public class ArtificalIntelligence : ITestSticks
    {
        //Matrix the AI uses as a way to learn as games go on
        private int[,] AIDataList = new int[101, 3];
        //Matrix used for collection of values in each game
        private int[,] addingMatrixList = new int[101, 3];

        Random random = new Random();

        //Object constructor
        public ArtificalIntelligence()
        {
            //Rows - Number of sticks
            //Columns - 3 Chips for Each Cup
            AIDataList = AIDataListSetUp();
        }

        //Returns all "chips" for one stick
        public int[] CupContents(int sticksRemaining)
        {
            int[] cupContentArray = new int[3];

            for (int i = 0; i < 2; i++)
            {
                cupContentArray[i] = AIDataList[sticksRemaining, i];
            }

            return cupContentArray;
        }

        public void ArtificalIntelligenceAppereance()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine("dP'  ~YMMb           dOOOOOOOOOOOOOOOb            aMMP~  `Yb ");
            Console.WriteLine(" V      ~\"Mb          dOOOOOOOOOOOOOOOOOb          dM\"~V ");
            Console.WriteLine("       `Mb   .dOOOOOOOOOOOOOOOOOOOb      ,dM' ");
            Console.WriteLine("          `YMb._ | OOOOOOOOOOOOOOOOOOOOO | _,dMP' ");
            Console.WriteLine("      __     `YMMM | OP'~\"YOOOOOOOOOOOP\"~`YO |MMMP'    ");
            Console.WriteLine("    ,dMMMb.      ~~' OO     `YOOOOOP'     OO `~~     ,dMMMb. ");
            Console.WriteLine("       _,dP~  `YMba_ OOb      `OOO'      dOO      _aMMP'  ~Yb._ ");
            Console.WriteLine("             `YMMMM`OOOo OOO     oOOO'/MMMMP' ");
            Console.WriteLine("     ,aa.      `~YMMb `OOOb._,dOOOb._,dOOO'dMMP~'       ,aa. ");
            Console.WriteLine(" ,dMYYMba._           `OOOOOOOOOOOOOOOOO'          _,adMYYMb. ");
            Console.WriteLine("  ,MP'  `YMMba._   OOOOOOOOOOOOOOOOO        _,adMMP'   `YM. ");
            Console.WriteLine("  MP'        ~YMMMba._ YOOOOPVVVVVYOOOOP    _,adMMMMP~       `YM ");
            Console.WriteLine("    YMb          ~YMMMM`OOOOI`````'IOOOOO'/MMMMP~           dMP ");
            Console.WriteLine("   `Mb.           `YMMMb`OOOI,,,,,IOOOO'dMMMP'           ,dM' ");
            Console.WriteLine("     `'                `OObNNNNNdOO'                   `' ");
            Console.WriteLine("                            `~OOOOO~'   ");
        }

        //Handles how the AI plays the game (returns how many sticks to take)
        public int GetSticks(int numOfSticks)
        {
            float allChips = 0f;

            float oneChipAvailable = 0f;
            float twoChipsAvailable = 0f;
            float threeChipsAvailable = 0f;

            float oneChipPercentage = 0f;
            float twoChipsPercentage = 0f;
            float threeChipsPercentage = 0f;

            //Gets info about chips in the cup
            oneChipAvailable += AIDataList[numOfSticks, 0];
            twoChipsAvailable += AIDataList[numOfSticks, 1];
            threeChipsAvailable += AIDataList[numOfSticks, 2];

            //Adds all chips together
            allChips = allChips + oneChipAvailable + twoChipsAvailable + threeChipsAvailable;

            //Turns into a percentage
            oneChipPercentage = (oneChipAvailable / allChips) * 100;
            twoChipsPercentage = (twoChipsAvailable / allChips) * 100;
            threeChipsPercentage = (threeChipsAvailable / allChips) * 100;

            //Makes it within 100%
            twoChipsPercentage = oneChipPercentage + twoChipsPercentage;
            threeChipsPercentage = twoChipsPercentage + threeChipsPercentage;

            //Picks a random number between 0-99
            Random random = new Random();
            int randomPercent = random.Next(0, 100);

            if (randomPercent > 0 && randomPercent < oneChipPercentage || numOfSticks == 1)
            {
                addingMatrixList[numOfSticks, 0] += 1;
                return 1;
            }
            else
            {
                if (randomPercent >= oneChipPercentage && randomPercent < twoChipsPercentage)
                {
                    addingMatrixList[numOfSticks, 1] += 1;
                    return 2;
                }
                else
                {

                    //If there are only two sticks left but the AI chose three sticks
                    if (numOfSticks == 2)
                    {
                        addingMatrixList[numOfSticks, 1] += 1;
                        return 2;
                    }
                    else
                    {
                        addingMatrixList[numOfSticks, 2] += 1;
                        return 3;
                    }
                }
            }
        }

        //Based on if an AI has won or lost, it will either do "AddingMatrixToMainMatrix" or "SubtractingMatrix"
        public void GameOver(bool won)
        {
            if (won == true)
            {
                AddingMatrixToMainMatrix();
            } else
            {
                SubtractingMatrix();
            }               
        }

        //Sets up matrix for AI to use for sticks taken
        private int[,] AIDataListSetUp()
        {
            //Rows - Number of sticks
            //Columns - 3 Chips for Each Cup
            int[,] AIDataList = new int[101, 3];

            //Makes all values in AIDataList '1'
            for (int r = 0; r < 101; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    AIDataList[r, c] = 1;
                }
            }

            return AIDataList;
        }

        //When AI wins, adds values from addingMatrixList to AIDataList
        private void AddingMatrixToMainMatrix()
        {
            for (int r = 0; r < 101; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    AIDataList[r, c] += addingMatrixList[r, c];
                }
            }

            ClearingAddingMatrix();
        }

        //When AI loses, subtracts values from AIDataList based on addingMatrixList
        private void SubtractingMatrix()
        {
            for (int r = 0; r < 101; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (AIDataList[r, c] > 1)
                    {
                        AIDataList[r, c] -= addingMatrixList[r, c];
                    }
                }
            }

            ClearingAddingMatrix();
        }

        //Clears the addingMatrix
        private void ClearingAddingMatrix()
        {
            for (int r = 0; r < 101; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    addingMatrixList[r, c] = 0;
                }
            }
        }
    }
}
