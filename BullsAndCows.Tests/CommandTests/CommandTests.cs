﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullsAndCowsGame;
using System.IO;

namespace BullsAndCows.Tests.CommandTests
{
    [TestClass]
    public class CommandTests
    {
        private GameEngine engine = GameEngine.Instance;

        [TestMethod]
        public void TopCommandTest()
        {
            CommandFactoryMethod factory = new CommandCreator();

            Assert.IsInstanceOfType(factory.Create("top", GameEngine.Instance), typeof(CommandTop));
        }

        [TestMethod]
        public void StartNewGameCommandTest()
        {
            CommandFactoryMethod factory = new CommandCreator();

            Assert.IsInstanceOfType(factory.Create("restart", GameEngine.Instance), typeof(CommandRestart));
        }

        [TestMethod]
        public void HelpCommandTest()
        {
            CommandFactoryMethod factory = new CommandCreator();

            Assert.IsInstanceOfType(factory.Create("help", GameEngine.Instance), typeof(CommandHelp));
        }

        [TestMethod]
        public void HelpCommandShouldReturnCorrectHelp()
        {
            CommandFactoryMethod factory = new CommandCreator();
            string helpCommandName = "help";

            Command helpCommand = factory.Create(helpCommandName, GameEngine.Instance);

            string expectedHelp = GameEngine.Instance.GetHelp() + "\r\n";
            GameEngine.Instance.Helper.Cheats -= 1;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                helpCommand.Execute(GameEngine.Instance.InputOutput);

                Assert.AreEqual(expectedHelp, sw.ToString());

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }

        [TestMethod]
        public void ExitCommandTest()
        {
            CommandFactoryMethod factory = new CommandCreator();

            Assert.IsInstanceOfType(factory.Create("exit", GameEngine.Instance), typeof(CommandExit));
        }

        [TestMethod]
        public void OtherCommandTest()
        {
            CommandFactoryMethod factory = new CommandCreator();

            Assert.IsInstanceOfType(factory.Create("asdasf", GameEngine.Instance), typeof(CommandOther));
        }

        [TestMethod]
        public void ShouldGetInvalidCommandMessage()
        {
            CommandFactoryMethod factory = new CommandCreator();

            string invalidCommandName = "invalid command string";
            Command otherCommand = factory.Create(invalidCommandName, GameEngine.Instance);
            string expectedMessage = "Invalid guess or command!\r\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                otherCommand.Execute(GameEngine.Instance.InputOutput);
                Assert.AreEqual<string>(expectedMessage, sw.ToString());

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }

        [TestMethod]
        public void ShouldGetGoodByeMessage()
        {
            CommandFactoryMethod factory = new CommandCreator();

            string exitCommandName = "exit";
            Command otherCommand = factory.Create(exitCommandName, GameEngine.Instance);
            string expectedMessage = "Good bye!\r\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                otherCommand.Execute(GameEngine.Instance.InputOutput);
                Assert.AreEqual<string>(expectedMessage, sw.ToString());

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }

        [TestMethod]
        public void ShouldGetOnePlayerInLeaderBoardMessage()
        {
            CommandFactoryMethod factory = new CommandCreator();

            string topCommandName = "top";
            Command topCommand = factory.Create(topCommandName, GameEngine.Instance);
            string expectedMessage = "Scoreboard:\n1. Gosho --> 1 guess\n2. Hristo --> 3 guesses\n3. Pesho --> 5 guesses\n\r\n";
            
            Player player1 = new Player("Pesho", 5);
            Player player2 = new Player("Gosho", 1);
            Player player3 = new Player("Hristo", 3);

            GameEngine.Instance.LeaderBoard.Add(player1);
            GameEngine.Instance.LeaderBoard.Add(player2);
            GameEngine.Instance.LeaderBoard.Add(player3);
            
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                topCommand.Execute(GameEngine.Instance.InputOutput);
                Assert.AreEqual<string>(expectedMessage, sw.ToString());

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }


        [TestMethod]
        public void CommandRestartShouldResetAttempts()
        {
            CommandFactoryMethod factory = new CommandCreator();

            string restartCommandName = "restart";
            Command restartCommand = factory.Create(restartCommandName, GameEngine.Instance);

            GameEngine.Instance.Attempts += 1;

            restartCommand.Execute(GameEngine.Instance.InputOutput);

            Assert.AreEqual<int>(0, GameEngine.Instance.Attempts);
        }
    }
}