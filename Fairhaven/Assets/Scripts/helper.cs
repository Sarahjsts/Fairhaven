using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper : MonoBehaviour
{
    public int x;
    public int y;
    public bool visited = false;

    public helper(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
