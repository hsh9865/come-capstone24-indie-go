using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Node
{
    public enum Map_type
    {
        Empty=0,
        Enemy=1,
        Enviroment=2,
        Treasure=3,
        Enterance,
        Exit
    }
    public Map_Node Up_node;
    public Map_Node Left_node;
    public Map_Node Right_node;
    public Map_Node Down_node;
    
    public int[,] tile = new int[20,20];
    public TileNode node;
    public TileNode Tile_left;
    public TileNode Tile_Right;
    public TileNode Tile_Up;
    public TileNode Tile_Down;
    public int way;
    public Map_type map_type;
    public RectInt nodeRect;
    public Map_Node(RectInt rect,int way=0)
    {
        this.way = way;
        this.nodeRect = rect;

    }
}
