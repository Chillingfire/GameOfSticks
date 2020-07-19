using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfSticks
{
	/// <summary>
	/// AI that implements this interface can be tested
	/// via the provided unit tests for Game of Sticks.
	/// </summary>
	public interface ITestSticks
	{
		/// <summary>
		/// Given the number of sticks still in play, tells what the AI's move will be.
		/// </summary>
		/// <param name="sticksRemaining"># of sticks remaining in the game</param>
		/// <returns># of sticks to take</returns>
		int GetSticks(int sticksRemaining);

		/// <summary>
		/// Tells AI that game has ended. AI is expected to learn based on the given result.
		/// </summary>
		/// <param name="won">AI won if true</param>
		void GameOver(bool won);

		/// <summary>
		/// Gets an array containing chip counts in the AI's cup/neuron
		/// corresponding to the given number of sticks remaining.
		/// </summary>
		/// <param name="sticksRemaining"># of sticks remaining in the game</param>
		/// <returns>chip counts</returns>
		int[] CupContents(int sticksRemaining);
	}
}
