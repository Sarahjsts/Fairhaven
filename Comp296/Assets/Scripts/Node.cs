using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int height;
    public int width;
    public int xPos;
    public int yPos;
    public int xMin = -1;
    public int xMax;
    public int yMin;
    public int yMax;
    public Rect rect;
    public Node right;
    Node left;


    public Node(int height, int width, int xPos, int yPos)
    {
        this.height = height;
        this.width = width;
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public void MakeRect()
    {
        xMin = xPos + 2;
        yMin = yPos + 2;
        xMax = xPos + width - 2;
        yMax = yPos + height - 2;
    }
    public Node GetRight()
    {
        return this.right;
    }

    public void SetRight(Node node)
    {
        this.right = node;
    }
    public Node GetLeft()
    {
        return this.left;
    }
    public void SetLeft(Node node)
    {
        this.left = node;
    }
}
