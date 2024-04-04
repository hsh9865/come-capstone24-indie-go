using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Node
{
    public enum Map_type
    {
        Empty,
        Enterance,
        Enemy,
        Enviroment,
        Exit,
        Treasure
    }
    public Map_Node Up_node;
    public Map_Node left_node;
    public Map_Node Right_node;
    public Map_Node Down_node;
    public int way;
    public Map_type map_type;
    public RectInt nodeRect;
    public Map_Node(RectInt rect,int way=0)
    {
        this.way = way;
        this.nodeRect = rect;

    }
}
