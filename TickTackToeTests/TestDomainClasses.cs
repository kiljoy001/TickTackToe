using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

            //Act
            Game testGame = new Game(player1, player2);
            //Assert
            Assert.AreEqual(typeof(Game), testGame);
        }

        [TestMethod]
        public void Game_Declares_Winner()
        {
            //Arrange
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2);
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
            Game testGame = new Game(player1, player2);

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
            Game testGame = new Game(player1, player2);

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
            Game testGame = new Game(player1, player2);
            //Not needed yet, but will be needed when implemented:
            //Grid gameGrid = new Grid();
            GridPoint point = new GridPoint(1,1);

            //Act
            testGame.MakeMove(player1, point);
            GridPoint[] player1points = player1.PlayerPositions();
            
            //Assert
            Assert.IsTrue(player1points.Contains(point));
        }

        /// Grid Tests

        [TestMethod]
        public void Grid_Initializes()
        {
            //Arrange & Act
            Grid gameGrid = new Grid();

            //Assert
            Assert.IsTrue(gameGrid.BoardLocations.Length == 8);
            Assert.AreEqual(typeof(Grid), gameGrid);

        }

        [TestMethod]
        public void Grid_Check_Open_Location_Returns_True()
        {
            //Arrange
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1,3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };

            //Act
            bool result = gameGrid.CheckOpenLocation(new GridPoint(1, 1));

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Grid_Commit_Move_And_Update_Board()
        {
            //Arrange
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2);
            //Act
            testGame.MakeMove(player1, new GridPoint(1, 1));

            //Assert
            Assert.IsTrue(gameGrid.BoardLocations.Contains(new GridPoint(1, 1)));
            Assert.IsTrue(gameGrid.CheckPointOwnerShip(new GridPoint(1,1)) == player1);
        }

        [TestMethod]
        public void Grid_Clear_All()
        {
            //Arrange 
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2);
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
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2);
            //Act
            testGame.MakeMove(player1, new GridPoint(1, 1));

            //Assert
            Assert.IsTrue(gameGrid.CheckPointOwnerShip(new GridPoint(1, 1)) == player1);
        }

        [TestMethod]
        public void Grid_Check_PlayerGridPoints()
        {
            //Arrange
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };
            Player player1 = new Player();
            Player player2 = new Player();
            Game testGame = new Game(player1, player2);
            //Act
            testGame.MakeMove(player1, new GridPoint(1, 1));

            //Assert
            Assert.IsTrue(gameGrid.CheckPointOwnerShip(new GridPoint(1, 1)) == player1);
        }

        [TestMethod]
        public void Grid_GridArray_Returns_GridPoint_Array()
        {
            GridPoint[] array = { new GridPoint(1, 1), new GridPoint(1, 2), new GridPoint(1, 3) };

            Grid gameGrid = new Grid()
            {
                BoardLocations = array
            };

            Assert.AreEqual(typeof(GridPoint[]), gameGrid.GridArray());
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
            Assert.AreEqual(typeof(Point), result);
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
