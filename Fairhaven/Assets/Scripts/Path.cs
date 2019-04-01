using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
        public static helper[,] init()
        {
            int width = 10;
            int height = 10;
            helper[,] board = new helper[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board[i, j] = new helper(i, j);
                }
            }
            return board;
        }
        static void Main(string[] args)
        {
            helper[,] board = null;
            board = init();

            int startx = 0;
            int starty = 0;
            int goalx = 9;
            int goaly = 8;

            Queue<helper> queue = new Queue<helper>();
            board[startx, starty].visited = true;
            helper start = board[startx, starty];
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                helper v = queue.Dequeue();
                v.visited = true;
                helper[] array = new helper[4];
                if (v.y + 1 < board.GetLength(1))
                {
                    array[0] = board[v.x, v.y + 1];
                }
                else
                {
                    array[0] = null;
                }
                if (v.x - 1 >= 0)
                {
                    array[1] = board[v.x - 1, v.y];
                }
                else
                {
                    array[1] = null;
                }
                if (v.y - 1 >= 0)
                {
                    array[2] = board[v.x, v.y - 1];
                }
                else
                {
                    array[2] = null;
                }
                if (v.x + 1 < board.GetLength(0))
                {
                    array[3] = board[v.x + 1, v.y];
                }
                else
                {
                    array[3] = null;
                }


                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != null)
                    {
                        if (!array[i].visited)
                        {
                            queue.Enqueue(array[i]);
                            array[i].visited = true;

                        }

                    }
                }
            }



        }
    }

