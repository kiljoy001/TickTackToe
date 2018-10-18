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
    bool CheckOpenLocation();
    void CommitAndUpdateBoard(IGridPoint point);
    void ClearAll();
    IPlayer CheckPointOwnerShip();
    GridPoint[] GridArray();
}

public interface IPlayer
{
    GridPoint[] PlayerPositions();
}

public interface IGridPoint
{
    Point GetLocation(int y, int x);
    IPlayer Ownership();
    void SetOwnership(IPlayer player);
}

