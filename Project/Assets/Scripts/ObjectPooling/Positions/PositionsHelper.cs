
public static class PositionsHelper
{
    private static PositionsList positionsList = new PositionsList(3);
    private static readonly Positions[] flags = { Positions.Left, Positions.Middle, Positions.Right };

    public static Positions GetRandomPosition()
    {
        const int max = (int)Positions.All + 1;

        return (Positions)UnityEngine.Random.Range(1, max);
    }

    public static Positions GeneratorPositions()
    {
        Positions positions = GetRandomPosition();
        positionsList.Reset();

        if (HasFlag(positions, Positions.Left))
        {
            positionsList.Add(-1);
        }

        if (HasFlag(positions, Positions.Middle))
        {
            positionsList.Add(0);
        }

        if(HasFlag(positions, Positions.Right))
        {
            positionsList.Add(1);
        }

        return positions;
    }

    public static bool HasFlag(Positions positions, Positions flag) 
        => (positions & flag) == flag;

    public static PositionsList List
        => positionsList;
}
