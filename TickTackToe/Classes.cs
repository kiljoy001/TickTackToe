public class Game : IGame
{
    private readonly IPlayer player1;
    private readonly IPlayer player2;
    public GameStates States { get; set; }

    public Game(IPlayer p1, IPlayer p2)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        throw new System.NotImplementedException();
    }
    public void DeclareMatchWinner(IPlayer player)
    {
        throw new System.NotImplementedException();
    }
    public void DeclareDraw()
    {
        throw new System.NotImplementedException();
    }
    public void Stop()
    {
        throw new System.NotImplementedException();
    }
    public void MakeMove(IPlayer user, IGridPoint point)
    {
        throw new System.NotImplementedException();
    }
}

public class Grid: IGrid
{
    public GridPoint[] BoardLocations { get; set; }
    public bool CheckOpenLocation(IGridPoint point)
    {
        throw new System.NotImplementedException();
    }
    public void CommitAndUpdateBoard()
    {
        throw new System.NotImplementedException();
    }
    public void ClearAll()
    {
        throw new System.NotImplementedException();
    }
    public IPlayer CheckPointOwnerShip(IGridPoint point)
    {
        throw new System.NotImplementedException();
    }
    public IGridPoint[] PlayerGridArray()
    {
        throw new System.NotImplementedException();
    }

    public GridPoint[] GridArray()
    {
        throw new System.NotImplementedException();
    }

    public bool CheckOpenLocation()
    {
        throw new System.NotImplementedException();
    }

    public void CommitAndUpdateBoard(IGridPoint point)
    {
        throw new System.NotImplementedException();
    }

    public IPlayer CheckPointOwnerShip()
    {
        throw new System.NotImplementedException();
    }
}

public class Player : IPlayer
{
    private string PlayerName;
    public bool IsWinner { get; set; }
    private GridPoint[] Positions;

    public GridPoint[] PlayerPositions()
    {
        throw new System.NotImplementedException();
    }

    public void AddPosition(GridPoint point)
    {
        throw new System.NotImplementedException();
    }

    public void AddPosition(GridPoint[] points)
    {
        throw new System.NotImplementedException();
    }
}

public class GridPoint: IGridPoint
{
    private Point location;
    private IPlayer Owner = null;

    public GridPoint(int y, int x)
    {
        location.vertical = y;
        location.horizontal = x;
    }
    public Point GetLocation(int y, int x) { throw new System.NotImplementedException(); }
    public IPlayer Ownership() { throw new System.NotImplementedException(); }

    public void SetOwnership(IPlayer player)
    {
        throw new System.NotImplementedException();
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