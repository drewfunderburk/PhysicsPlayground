using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Queue with maximum limit. Automatically Dequeue's old items
/// </summary>
public class History<T> : Queue<T>
{
    private int _limit;

    public int Limit 
    { 
        get => _limit;
        set
        {
            // TODO: Expand or shrink Queue to be the exact right size
            //  Potentially deep copy
            //  Look into copying a larger queue into a smaller
            if (value >= 0)
            {
                while (Count > value)
                    Dequeue();

                if (value > _limit)
                    TrimExcess();

                _limit = value;
            }
        }
    }

    public History(int limit) : base(limit)
    {
        _limit = limit;
    }

    public new void Enqueue(T item)
    {
        base.Enqueue(item);

        while (Count > Limit)
            Dequeue();
    }
}
