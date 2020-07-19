using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfSticks;

namespace GameOfSticksTests
{
	[TestClass]
	public class UnitTest1
	{
		private Random rand;
		const int MaxSticksInGame = 100;
		const int MaxSticksPerTurn = 3;
		private ITestSticks ai;

		// Note: a new UnitTest1 object is created for each and every test that runs
		public UnitTest1()
		{
			this.rand = new Random();
			// TODO: Add ITestSticks.cs to your GameOfSticks project, and have your AI class implement that interface
			// TODO: create a new instance of your AI class
			this.ai = new ArtificalIntelligence();
			// TODO: if you have additional setup (e.g. .StartGame() method to call), put that code here
		}

		#region GetSticks Mechanics
		// Set of tests to make sure AI follows all rules when choosing moves
		// No thought to winning/losing in these tests

		/// <summary>
		/// AI should always take exactly 1 stick when there is 1 stick remaining
		/// </summary>
		[TestMethod]
		public void CheckOneLeft()
		{
			for (int i = 0; i < 1000; i++)
			{
				Assert.AreEqual(1, this.ai.GetSticks(1));
				this.ai.GameOver(false);
			}
		}

		/// <summary>
		/// AI should take 1 or 2 sticks when there are 2 sticks remaining
		/// </summary>
		[TestMethod]
		public void CheckTwoLeft()
		{
			for (int i = 0; i < 1000; i++)
			{
				int taken = this.ai.GetSticks(2);
				Assert.IsTrue(taken > 0 && taken <= 2);
				this.ai.GameOver(false);
			}
		}

		/// <summary>
		/// AI should always take at least 1 stick, and no more than the maximum allowed per turn
		/// </summary>
		[TestMethod]
		public void CheckSticksReturned()
		{
			for (int tests = 0; tests < 1000; tests++)
			{
				for (int i = MaxSticksInGame; i > 0; i--)
				{
					int taken = this.ai.GetSticks(i);
					Assert.IsTrue(taken > 0 && taken <= Math.Min(MaxSticksPerTurn, i));
					this.ai.GameOver(false);
				}
			}
		}
		#endregion

		#region Game Play
		// Set of tests to confirm that AI learns as it should when it wins and/or loses

		/// <summary>
		/// Does AI learn as it should from winning?
		/// </summary>
		[TestMethod]
		public void TestWinning()
		{
			// with new AI, run a bunch of games, telling AI it wins each time
			for (int i = 0; i < 1000; i++)
			{
				RunFakeGame(true);
			}
		}

		/// <summary>
		/// Does AI learn as it should from losing?
		/// </summary>
		[TestMethod]
		public void TestLosing()
		{
			// with new AI, run a bunch of games, telling AI it loses each time
			for (int i = 0; i < 1000; i++)
			{
				RunFakeGame(false);
			}
		}

		/// <summary>
		/// Does AI learn as it should from a random sequence of winning and losing?
		/// </summary>
		[TestMethod]
		public void TestWithRandomResults()
		{
			// with new AI, run a bunch of games, randomly telling AI it won/lost
			for (int i = 0; i < 1000; i++)
			{
				RunFakeGame(this.rand.Next(2) == 1);
			}
		}

		/// <summary>
		/// Simulates a single game with the specified result,
		/// and verifies that the AI learned according to spec
		/// </summary>
		/// <param name="victoryToAI">AI wins if true</param>
		private void RunFakeGame(bool victoryToAI)
		{
			// initial state of AI
			int[][] startingBrain = new int[MaxSticksInGame][];
			for (int cup = 0; cup < MaxSticksInGame; cup++)
				startingBrain[cup] = GetCupContents(cup + 1);

			// Play fake game
			int[] movesByAI = PlayGame();

			// End game with pre-ordained result, regardless of actual moves
			// (Yes, we might be telling AI they won or lost when that result
			// was impossible. But that's OK, since we're just checking the
			// learning mechanism, not attempting to train a smart AI.)
			this.ai.GameOver(victoryToAI);

			// Is AI the smart student we expect it to be?
			CheckLearning(victoryToAI, startingBrain, movesByAI);
		}

		/// <summary>
		/// Plays a full game with random starting player, and with human
		/// choices being utterly random. AI Moves recorded throughout.
		/// </summary>
		/// <returns>AI moves - non-zero values indicate sticks taken</returns>
		private int[] PlayGame()
		{
			// record of AI moves - initialized to all 0's by default
			int[] movesByAI = new int[MaxSticksInGame];

			int sticksRemaining = MaxSticksInGame;
			// Other player randomly goes first half the time
			if (this.rand.Next(2) == 0)
				sticksRemaining -= this.rand.Next(1, MaxSticksPerTurn + 1);

			while (sticksRemaining > 0)
			{
				// AI move
				int taken = this.ai.GetSticks(sticksRemaining);
				movesByAI[sticksRemaining - 1] = taken;
				sticksRemaining -= taken;
				// player move
				sticksRemaining -= this.rand.Next(1, MaxSticksPerTurn + 1);
			}

			return movesByAI;
		}

		/// <summary>
		/// Verifies that AI learned as it should have, based on moves made and the
		/// result of the game. Moves should have been reinforced in the case of a
		/// win, and discouraged (when possible) in the case of a loss.
		/// </summary>
		/// <param name="victoryToAI">AI was winner if true</param>
		/// <param name="startingBrain">AI's state of mind before game began</param>
		/// <param name="movesByAI">AI's moves for the game</param>
		private void CheckLearning(bool victoryToAI, int[][] startingBrain, int[] movesByAI)
		{
			for (int i = 0; i < MaxSticksInGame; i++)
			{
				int[] cup = GetCupContents(i + 1);
				int taken = movesByAI[i];

				for (int slot = 0; slot < MaxSticksPerTurn; slot++)
				{
					if (cup[slot] == startingBrain[i][slot])
					{
						if (slot == taken - 1)
						{
							// change expected, unless AI lost and was down to its last chip for this choice
							Assert.IsTrue(!victoryToAI && cup[slot] == 1,
								"Chip count corresponding to # sticks taken didn't change when it should have");
						}
					}
					else
					{
						// change happened in right slot
						Assert.AreEqual(slot, taken - 1);
						// correct change happened
						if (victoryToAI)
							Assert.AreEqual(cup[slot], startingBrain[i][slot] + 1);
						else
							Assert.AreEqual(cup[slot], startingBrain[i][slot] - 1);
					}
				}
			}
		}

		/// <summary>
		/// Gets AI's current state of mind for the given number of sticks remaining. Makes a copy
		/// of what AI hands over, in case AI is returning reference to actual array of brain data.
		/// </summary>
		/// <param name="sticksRemaining"></param>
		/// <returns>copy of AI's cup o' chips</returns>
		private int[] GetCupContents(int sticksRemaining)
		{
			int[] aiCup = this.ai.CupContents(sticksRemaining);
			int[] copyCup = new int[aiCup.Length]; // I know, sounds like a Java variable name :-)
			aiCup.CopyTo(copyCup, 0);
			return copyCup;
		}
		#endregion

		// McT - pretend I did something important here

        // I did something
	}
}
