using System;
using System.Collections.Generic;
using System.Linq;

public class Game : IGame
{
    private readonly IPlayer player1;
    private readonly IPlayer player2;
    public GameStates States { get; set; }
    private Grid GameGrid;
    public TurnStates TState { get; private set; }

    public Game(IPlayer p1, IPlayer p2, Grid ggrid)
    {
        player1 = p1;
        player2 = p2;
        States = GameStates.Stopped;
        GameGrid = ggrid;
        player1.Assignment = PlayerAssignment.Player1;
        player2.Assignment = PlayerAssignment.Player2;
        TState = TurnStates.None;
    }

    public void Start()
    {
        TState = TurnStates.Player1;
        States = GameStates.Running;
    }
    public void DeclareMatchWinner(IPlayer player)
    {
        States = GameStates.WinnerDeclared;
        player.IsWinner = true;
    }
    public void DeclareDraw()
    {
        States = GameStates.Draw;
    }
    public void Stop()
    {
        States = GameStates.Stopped;
    }
    public void MakeMove(IPlayer user, IGridPoint point)
    {
        if (user.Assignment.ToString() == TState.ToString())
        {
            //Move permitted, check Location
            if(GameGrid.CheckOpenLocation(point))
            {
                point.SetOwnership(user);
                GameGrid.CommitAndUpdateBoard(user);
            }
            else
            {
                throw new ArgumentException();
            }

        }
    }
}

public class Grid: IGrid
{
    public Grid()
    {
        var p11 = new GridPoint(1, 1);
        var p12 = new GridPoint(1, 2);
        var p13 = new GridPoint(1, 3);
        var p21 = new GridPoint(2, 1);
        var p22 = new GridPoint(2, 2);
        var p23 = new GridPoint(2, 3);
        var p31 = new GridPoint(3, 1);
        var p32 = new GridPoint(3, 2);
        var p33 = new GridPoint(3, 3);
        BoardLocations = new GridPoint[9] {p11, p12, p13, p21, p22, p23, p31, p32, p33};
    }
    public GridPoint[] BoardLocations { get; private set; }
    public void CommitAndUpdateBoard(IPlayer players)
    {
        List<IGridPoint> moves = new List<IGridPoint>();
        List<IGridPoint> player1positions = players.PlayerPositions();

        foreach(IGridPoint point in player1positions)
        {
            moves.Add(point);
        }
        
        foreach (GridPoint point in this.BoardLocations)
        {
            if(moves.Contains(point))
            {
                GridPoint updatePoint = BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == point.Location.horizontal && gp.Location.vertical == point.Location.vertical );
                if (updatePoint != null && updatePoint.Ownership() != point.Ownership()) updatePoint.SetOwnership(point.Ownership());
            }
        }
    }
    public void ClearAll()
    {
        foreach(GridPoint point in BoardLocations)
        {
            point.SetOwnership(null);
        }
    }
    public IPlayer CheckPointOwnerShip(GridPoint point)
    {
        if (point != null && BoardLocations.Contains(point))
        {
            GridPoint matchedPoint = BoardLocations.FirstOrDefault(gp => gp.Location.horizontal == point.Location.horizontal && gp.Location.vertical == point.Location.vertical);
            return matchedPoint.Ownership();
        }
        else return null;
    }
    public List<IGridPoint> PlayerGridArray(IPlayer player)
    {
        return player.PlayerPositions();
    }
    
    //Possibly not needed, property already does this.
    //public GridPoint[] GridArray()
    //{
    //    throw new System.NotImplementedException();
    //}

    public bool CheckOpenLocation(IGridPoint point)
    {
        return point.Ownership() == null ? true : false;     
    }

    //Not needed
    //public void CommitAndUpdateBoard(IGridPoint point)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public IPlayer CheckPointOwnerShip()
    //{
    //    throw new System.NotImplementedException();
    //}
}

public class Player : IPlayer
{
    private string PlayerName;
    public bool IsWinner { get; set; }
    public PlayerAssignment Assignment { get; set; }
    private List<IGridPoint> Positions = new List<IGridPoint>();

    public Player(string name = null)
    {
        PlayerName = name;
    }

    public List<IGridPoint> PlayerPositions()
    {
        return Positions;
    }

    public void AddPosition(IGridPoint point)
    {
        Positions.Add(point);
    }

    public void AddPosition(IGridPoint[] points)
    {
        foreach(IGridPoint point in points)
        {
            Positions.Add(point);
        }
    }
}

public class GridPoint: IGridPoint
{
    private Point location;
    private IPlayer Owner = null;
    public bool Occupied { get; private set; }
    public Point Location { get {return location; } }

    public GridPoint(int y, int x, IPlayer player = null)
    {
        location.vertical = y;
        location.horizontal = x;
        Owner = player;
    }
    public Point GetLocation(int y, int x)
    {
        return y > 0 && y <= 3 && x > 0 && x <= 3 ? new Point
        {
            vertical = y,
            horizontal = x
        } :
        throw new ArgumentException(); 
    }
    public IPlayer Ownership()
    {
        return Owner;
    }

    public void SetOwnership(IPlayer player)
    {
        Owner = player;
        Occupied = true;
        player.AddPosition(this);
    }
}

public enum GameStates
{
    Running,
    Stopped,
    Draw,
    WinnerDeclared
}

public struct Point
{
    public int vertical;
    public int horizontal;
}

public enum TurnStates
{
    Player1,
    Player2,
    None
}

public enum PlayerAssignment
{
    Player1,
    Player2
}