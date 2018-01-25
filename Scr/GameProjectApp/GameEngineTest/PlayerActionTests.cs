using System;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineTest
{
    [TestClass]
    public class PlayerActionTests
    {
        [TestMethod]
        public void BuildRoad_BuildOneRoad_UpdatesGameModel()
        {
            // Arrange
            GameInstruction gameInstruction = new GameInstruction();
            GameStateModel game = new GameStateModel(gameInstruction);
            //game.ActivePlayer = new Player("player1", "id1");

            game.ActivePlayer.Inventory.Brick = 3;
            PlayerAction pa = new PlayerAction(game);

            // Act
            pa.BuildRoad(23);

            // Assert
            Assert.AreEqual(2, game.ActivePlayer.Inventory.Brick);
        }

        [TestMethod]
        public void LongestRoadLength_()
        {
            // Arrange
            GameInstruction gameInstruction = new GameInstruction();
            GameStateModel game = new GameStateModel(gameInstruction);
            //game.ActivePlayer = new Player("player1", "id1");

            game.ActivePlayer.Inventory.Brick = 3;
            PlayerAction pa = new PlayerAction(game);

            // Act
            pa.BuildRoad(23);

            // Assert
            Assert.AreEqual(2, game.ActivePlayer.Inventory.Brick);
        }
    }
}
