using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum Floor_type
    {
        Empty,
        Enemy,
        Enviroment,
        Exit,
        Treasure
    }

    public Node leftNode;
    public Node rightNode;
    public Node parNode;
    public Floor_type floor_type;
    public RectInt nodeRect; //분리된 공간의 rect정보
    public int count;

    public Node(RectInt rect)
    {
        this.nodeRect = rect;
    }
}
