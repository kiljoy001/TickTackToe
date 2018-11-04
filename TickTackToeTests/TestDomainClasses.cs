using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace TickTackToeTests
{
    [TestClass]
    public class TestDomainClasses
    {
        [TestMethod]
        public void Game_Initializes()
        {
            //Arrange
            //make players and game
            Player player1 = new Player();
            Player player2 = new Player();
            Grid grid = new Grid();

            //Act
            Game testGame = new Game(player1, player2, grid);
            //Assert
            Assert.IsTrue(testGame is Game);
        }

        [TestMethod]
        public void Game_Declares_Winner()
        {
            //Arrange
            Player player1 = new Player();
            Player player2 = new Player();
            Grid grid = new Grid();
            Game testGame = new Game(player1, player2, grid);
            //Act
            testGame.DeclareMatchWinner(player1);
            //Assert
            Assert.IsTrue(player1.IsWinner);
        }

        [TestMethod]
        public void Game_Declares_Draw()
        {
            //Arrange
            Player player1 = new Player();
            Player player2 = new Player();
            Grid grid = new Grid();
            Game testGame = new Game(player1, player2, grid);

            //Act
            testGame.DeclareDraw();

            //Assert
            Assert.AreEqual(GameStates.Draw, testGame.States);
        }

        [TestMethod]
        public void Game_Stops()
        {
            //Arrange
            Player player1 = new Player();
            Player player2 = new Player();
            Grid grid = new Grid();
            Game testGame = new Game(player1, player2, grid);

            //Act
            testGame.Stop();

            //Assert
            Assert.AreEqual(GameStates.Stopped, testGame.States);
        }

        [TestMethod]
        public void Player_Can_Make_Move()
        {
            //Arrange
            Player player1 = new Player();
            Player player2 = new Player();
            Grid grid = new Grid();
            Game testGame = new Game(player1, player2, grid);
            testGame.Start();
            //Not needed yet, but will be needed when implemented:
            //Grid gameGrid = new Grid();
            

            //Act
            testGame.MakeMove(player1, grid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1));
            List<IGridPoint> player1points = player1.PlayerPositions();
            
            //Assert
            Assert.IsTrue(player1points.Count > 0);
        }

        /// Grid Tests

        [TestMethod]
        public void Grid_Initializes()
        {
            //Arrange & Act
            Grid gameGrid = new Grid();

            //Assert
            Assert.IsTrue(gameGrid.BoardLocations.Length == 9);
            Assert.IsTrue(gameGrid is Grid);

        }

        [TestMethod]
        public void Grid_Check_Open_Location_Returns_True()
        {
            //Arrange
            Grid gameGrid = new Grid();

            //Act
            bool result = gameGrid.CheckOpenLocation(gameGrid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1));

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Grid_Commit_Move_And_Update_Board()
        {
            //Arrange
            Grid gameGrid = new Grid();
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2, gameGrid);
            testGame.Start();
            //Act
            testGame.MakeMove(player1, gameGrid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1));

            //Assert
            Assert.IsTrue(player1.PlayerPositions().Count > 0);
            Assert.IsTrue(gameGrid.CheckPointOwnerShip(gameGrid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1)) == player1);
        }

        [TestMethod]
        public void Grid_Clear_All()
        {
            //Arrange 
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid();
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2, gameGrid);
            //Act
            testGame.MakeMove(player1, new GridPoint(1, 1));
            
            //Assert
            foreach (GridPoint point in gameGrid.BoardLocations)
            {
                Assert.IsTrue(gameGrid.CheckPointOwnerShip(point) == null);
            }
        }

        [TestMethod]
        public void Grid_Check_CheckPointOwnerShip()
        {
            //Arrange
  
            Grid gameGrid = new Grid();
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2, gameGrid);
            testGame.Start();
            //Act
            testGame.MakeMove(player1, gameGrid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1));
            foreach(GridPoint point in gameGrid.BoardLocations)
            {
                //Assert
                if (point.Location.horizontal ==1 && point.Location.vertical==1)
                {
                    Assert.IsTrue(point.Ownership() == player1);
                }
            }
        }

        [TestMethod]
        public void Grid_Check_PlayerGridPoints()
        {
            //Arrange
           
            Grid gameGrid = new Grid();
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2,gameGrid);
            testGame.Start();

            //Act
            testGame.MakeMove(player1, gameGrid.BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == 1 && gp.Location.vertical == 1));

            //Assert
            Assert.IsTrue(player1.PlayerPositions().Count == 1);
        }

        [TestMethod]
        public void Grid_GridArray_Returns_GridPoint_Array()
        {
            Player player1 = new Player();
            Grid gameGrid = new Grid();

            Assert.IsTrue(gameGrid.PlayerGridArray(player1) is List<IGridPoint>);
        }

        [TestMethod]
        public void Player_Positions_Returns_GridPoint_Array()
        {
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };
            Player player = new Player();
            player.AddPosition(array);
            var result = player.PlayerPositions();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GridPoint_Get_Point()
        {
            GridPoint gp = new GridPoint(1, 1);
            var result = gp.GetLocation(1, 1);
            Assert.IsTrue(result is Point);
        }

        [TestMethod]
        public void GridPoint_Set_Ownership()
        {
            GridPoint gp = new GridPoint(1, 1);
            Player player = new Player();
            gp.SetOwnership(player);
            Assert.IsTrue(gp.Ownership() == player);
        }

        
    }
}
