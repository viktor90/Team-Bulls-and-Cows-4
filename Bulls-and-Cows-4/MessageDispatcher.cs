﻿namespace BullsAndCowsGame
{
    using System;
    using System.Text;
    using Contracts;

    /// <summary>
    /// An instance of this object is used to return messages for the player.
    /// </summary>
    public class MessageDispatcher : IMessageDispatcher
    {
        /// <summary>
        /// A good bye message to be sent out to a player leaving the game.
        /// </summary>
        public const string GoodByeMessage = "Good bye!";

        /// <summary>
        /// A message to be sent out to the player when he enters an invalid command.
        /// </summary>
        public const string InvalidCommandMessage = "Invalid guess or command!";

        /// <summary>
        /// A message to be sent out to the player if he used cheats during the game. Cheaters are not accepted in the scoreboard.
        /// </summary>
        public const string NoCheaterMessage = "Cheaters are not allowed to enter the top scoreboard.";

        /// <summary>
        /// A message to be displayed when it's time for the player to enter his name for the scoreboard.
        /// </summary>
        public const string EnterNameMessage = "Please enter your name for the top scoreboard: ";

        /// <summary>
        /// A message to be displayed to the player when it's time for him to choose a command or enter a guess number.
        /// </summary>
        public const string EnterCommandMessage = "Enter your guess or command: ";

        /// <summary>
        /// A welcome message to be displayed to the player when he starts a new game.
        /// </summary>
        public const string WelcomeMessage = "Welcome to “Bulls and Cows” game.\n\nPlease try to guess my secret 4-digit number.\nUse:\n'top' to view the top scoreboard\n'restart' to start a new game\n'help' to cheat\n'exit' to quit the game.\n";
        
        /// <summary>
        /// Initializes a new instance of the BullsAndCows.MessageDispatcher class.
        /// </summary>
        public MessageDispatcher()
        {
        }

        /// <summary>
        /// Returns a goodbye message to be displayed to the player.
        /// </summary>
        /// <returns>The goodbye message.</returns>
        public string GetGoodbyeMessage()
        {
            return GoodByeMessage;
        }

        /// <summary>
        /// Returns a message notifying the player he entered an invalid command.
        /// </summary>
        /// <returns>The invalid command message.</returns>
        public string GetInvalidCommandMessage()
        {
            return InvalidCommandMessage;
        }

        /// <summary>
        /// Returns a message notifying the player he is not allowed to cheat anymore.
        /// </summary>
        /// <returns>The "not allowed to cheat anymore" message.</returns>
        public string GetNoCheatersMessage()
        {
            return NoCheaterMessage;
        }

        /// <summary>
        /// Returns a message asking the player to enter his name.
        /// </summary>
        /// <returns>The "enter name" message.</returns>
        public string GetEnterNameMessage()
        {
            return EnterNameMessage;
        }

        /// <summary>
        /// Returns a message asking the player to enter command.
        /// </summary>
        /// <returns>The "enter command" message.</returns>
        public string GetEnterCommandMessage()
        {
            return EnterCommandMessage;
        }

        /// <summary>
        /// Returns a message, welcoming the player to the game.
        /// </summary>
        /// <returns>The "welcome" message.</returns>
        public string GetWelcomeMessage()
        {
            return WelcomeMessage;
        }

        /// <summary>
        /// Returns a message with information about how many bulls and how many cows did the guess number have.
        /// </summary>
        /// <param name="bullsCount">How many bulls did the guess number have.</param>
        /// <param name="cowsCount">How many cows did the guess number have.</param>
        /// <returns>A message holding information if how many bulls and cows were in the guess number.</returns>
        public string GetWrongNumberMessage(int bullsCount, int cowsCount)
        {
            return string.Format("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
        }

        /// <summary>
        /// Returns a formatted string displaying information about the players in the given leaderboard.
        /// </summary>
        /// <param name="leaderBoard">The leaderboard to be displayed.</param>
        /// <returns>The socreboard information formatted as a string.</returns>
        public string GetScoreBoard(LeaderBoard<Player> leaderBoard)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (leaderBoard.Count == 0)
            {
                stringBuilder.Append("Top scoreboard is empty.");
            }
            else
            {
                stringBuilder.Append("Scoreboard:\n");
                int i = 1;
                string currentAttemptsMessage;

                foreach (Player player in leaderBoard)
                {
                    currentAttemptsMessage = string.Format("{0}. {1} --> {2} guess" + ((player.Attempts == 1) ? string.Empty : "es") + "\n", i++, player.Name, player.Attempts);
                    stringBuilder.Append(currentAttemptsMessage);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// This should be invoked when the player guesses the right number. It returns a congratulations message containing information about how many attempts were made.
        /// </summary>
        /// <param name="helper">The helper used during the game.</param>
        /// <param name="attempts">How many attempts did the player make to guess the number.</param>
        /// <returns>The congratulations message.</returns>
        public string GetCongatulationsMessage(Helper helper, int attempts)
        {
            if (attempts <= 0)
            {
                throw new ArgumentException("The number of attemps to win the game cannot be zero or negative integer");
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("Congratulations!\nYou guessed the secret number in {0} attempt" + (attempts > 1 ? "s" : string.Empty), attempts));
            
            if (helper.Cheats == 0)
            {
                stringBuilder.Append(".");
            }
            else
            {
                stringBuilder.Append(string.Format(" and {0} cheat" + (helper.Cheats > 1 ? "s." : "."), helper.Cheats));
            }

            return stringBuilder.ToString();
        }
    }
}