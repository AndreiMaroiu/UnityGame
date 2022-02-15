using UnityEngine;

public class PositionsList
{
    private int[] positions;
    private int length;

    public PositionsList(int length)
        => (positions, this.length) = (new int[length], length);

    public int Length => length;

    public void Reset() 
        => length = 0;

    public void Add(int pos) 
        => positions[length++] = pos;

    public bool HasNext()
        => length != 0;

    public int TakeRandom()
    {
        int index = Random.Range(0, length);
        --length;

        int temp = positions[index];
        positions[index] = positions[length];
        positions[length] = temp;

        return temp;
    }
}