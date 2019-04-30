using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //public static helper[,] ListToArray(Queue<helper> queue)

    public static helper[] Pathing(helper[,] board, helper start, helper goal)
    {


        start.visited = true;


        Queue<helper> queue = new Queue<helper>();


        Queue<helper> prev = new Queue<helper>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            helper v = queue.Dequeue();

            v.visited = true;
            if (v.Equals(goal))
            {

                prev = FindPath(v, prev);


                break;
            }


            List<helper> list = adjacencyList(board, v);

            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].visited == false)
                {
                    Debug.Log(list[i].y);
                    list[i].visited = true;
                    list[i].setPrevious(v);

                    queue.Enqueue(list[i]);

                }
            }

        }
        return prev.ToArray();

    }
    // creates list of edges for use in pathing function
    public static List<helper> adjacencyList(helper[,] G, helper node)
    {
        List<helper> edges = new List<helper>();
        if ((node.y + 1) < BoardCreator2.height && !G[node.x, node.y + 1].val)
        {
            edges.Add(G[node.x, node.y + 1]);
        }
        if ((node.x + 1) < BoardCreator2.width && !G[node.x + 1, node.y].val)
        {
            edges.Add(G[node.x + 1, node.y]);
        }
        if ((node.y - 1) >= 0 && !G[node.x, node.y - 1].val)
        {
            edges.Add(G[node.x, node.y - 1]);
        }

        if ((node.x - 1) >= 0 && !G[node.x - 1, node.y].val)
        {
            edges.Add(G[node.x - 1, node.y]);
        }

        return edges;
    }
    public static Queue<helper> FindPath(helper node, Queue<helper> queue)
    {

        bool isTrue = node.previous == null;
        helper newNode = node.previous;

        if (isTrue)
        {
            return queue;
        }
        else
        {
            queue.Enqueue(newNode);
            return FindPath(newNode, queue);
        }
    }
}
