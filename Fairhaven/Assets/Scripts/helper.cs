using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper 
{
    public int x;
    public int y;
    public bool visited = false;
    public bool val;
    public int flood = 0;
    public helper previous = null;

    public helper(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public bool Equals(helper node)
    {
        if (node.x == x && node.y == y)
        {
            return true;
        }

        return false;
    }
    public void setPrevious(helper node)
    {
        previous = node;
    }
}
