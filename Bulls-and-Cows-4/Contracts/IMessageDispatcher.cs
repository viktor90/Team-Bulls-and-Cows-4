﻿namespace BullsAndCowsGame.Contracts
{
    /// <summary>
    /// Implementations of this interface are used to produce messages for the player.
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// Returns a formatted string displaying information about the players in the given leaderboard.
        /// </summary>
        /// <param name="leaderBoard">The leaderboard to be displayed.</param>
        /// <returns>The socreboard information formatted as a string.</returns>
        string GetScoreBoard(LeaderBoard<Player> leaderBoard);

        /// <summary>
        /// This should be invoked when the player guesses the right number. It returns a congratulations message containing information about how many attempts were made.
        /// </summary>
        /// <param name="helper">The helper used during the game.</param>
        /// <param name="attempts">How many attempts did the player make to guess the number.</param>
        /// <returns>The congratulations message.</returns>
        string GetCongatulationsMessage(Helper helper, int attempts);

        /// <summary>
        /// This is invoked when the player enters a wrong guess number. It returns a message with information about how many bulls and how many cows did the guess number have.
        /// </summary>
        /// <param name="bullsCount">How many bulls did the guess number have.</param>
        /// <param name="cowsCount">How many cows did the guess number have.</param>
        /// <returns>A message holding information if how many bulls and cows were in the guess number.</returns>
        string GetWrongNumberMessage(int bullsCount, int cowsCount);

        string GetWelcomeMessage();

        string GetGoodbyeMessage();

        string GetInvalidCommandMessage();

        string GetNoCheatersMessage();

        string GetEnterNameMessage();

        string GetEnterCommandMessage();
    }
}