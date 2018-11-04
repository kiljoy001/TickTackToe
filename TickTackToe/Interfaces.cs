using System.Collections.Generic;

public interface IGame
{
    void Start();
    void DeclareMatchWinner(IPlayer player);
    void DeclareDraw();
    void Stop();
    void MakeMove(IPlayer user, IGridPoint point);
}

public interface IGrid
{
    GridPoint[] BoardLocations { get; }
    bool CheckOpenLocation(IGridPoint point);
    void CommitAndUpdateBoard(IPlayer player);
    void ClearAll();
    IPlayer CheckPointOwnerShip(GridPoint point);
    List<IGridPoint> PlayerGridArray(IPlayer player);
}

public interface IPlayer
{
    List<IGridPoint> PlayerPositions();
    bool IsWinner { get; set; }
    void AddPosition(IGridPoint point);
    void AddPosition(IGridPoint[] points);
    PlayerAssignment Assignment { get; set; }
}

public interface IGridPoint
{
    Point GetLocation(int y, int x);
    IPlayer Ownership();
    void SetOwnership(IPlayer player);
    bool Occupied { get; }
    Point Location { get; }
}

