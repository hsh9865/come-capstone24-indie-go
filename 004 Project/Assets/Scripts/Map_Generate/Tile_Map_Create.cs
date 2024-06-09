using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Tile_Map_Create : MonoBehaviour
{
    public static Tile_Map_Create instance = null;
    public GameObject Tile;
    public Tilemap Tilemap;
    public TileBase groundTile;
    public int[,] horizontal_arr = new int[40,20];
    public int[,] vertical_arr = new int[20,40];
    
    [SerializeField] float minimumDevideRate =0.3f; //공간이 나눠지는 최소 비율
    [SerializeField] float maximumDivideRate = 0.7f; //공간이 나눠지는 최대 비율
    bool left_open,
         right_open,
         up_open,
         down_open;
    int Max_Depth=2;
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Tilemap.ClearAllTiles();
    }
    public void Tile_Node(Map_Node root)
    {

        root.node = new TileNode(0,0);
        Divide_Tile(root,root.node,0);   
        ConnectRooms(root, root.node);
        // Make_Tile(root);
    }

    public void Divide_Tile(Map_Node parent, TileNode root,int n)
    {
        
        if (n == Max_Depth)
        {
            GenerateRoom(parent,root);
            return; //내가 원하는 높이에 도달하면 더 나눠주지 않는다.
        }
        //그 외의 경우에는
        
        int maxLength = Mathf.Max(root.height,root.width);
        //가로와 세로중 더 긴것을 구한후, 가로가 길다면 위 좌, 우로 세로가 더 길다면 위, 아래로 나눠주게 될 것이다.
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
        //나올 수 있는 최대 길이와 최소 길이중에서 랜덤으로 한 값을 선택
        if (root.width >= root.height) //가로가 더 길었던 경우에는 좌 우로 나누게 될 것이며, 이 경우에는 세로 길이는 변하지 않는다.
        {
            
            root.leftNode = new TileNode(root.x,root.y,split,root.height);
            root.rightNode= new TileNode(root.x+split,root.y,root.width-split,root.height);
            // for(int i = 0 ; i < root.height;i++) parent.tile[root.x+split,root.y+i] = 1;
        }
        else
        {
            
            root.leftNode = new TileNode(root.x,root.y,root.width,split);
            root.rightNode= new TileNode(root.x,root.y+split,root.width,root.height-split);
            // for(int i = 0 ; i < root.width;i++) parent.tile[root.x+i,root.y+split] = 1;
        }

        root.leftNode.parNode = root;
        root.rightNode.parNode = root;
        Divide_Tile(parent, root.leftNode, n + 1);
        Divide_Tile(parent, root.rightNode, n + 1);
    }

    public void Make_Tile(Map_Node parent)
    {
        
        for(int i =0;i<20;i++)
        {
            for(int j =0 ; j <20 ; j++)
            {
                if(parent.tile[i,j]==0)
                {
                    Tilemap.SetTile(new Vector3Int(parent.nodeRect.x+i, parent.nodeRect.y- j,1), groundTile);
                    // Instantiate(Tile,new Vector3(parent.nodeRect.x+i, parent.nodeRect.y- j,1),Quaternion.identity);
                }
            }
        }
    }
    private void GenerateRoom(Map_Node parent, TileNode tree)
    {  
        int width = Random.Range(tree.width/2,tree.width-1); 
        //방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
        int height=Random.Range(tree.height / 2,tree.height - 1);  
        //높이도 위와 같다.
        tree.x = tree.x + Random.Range(1, tree.width - width);
        //방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
        tree.y = tree.y + Random.Range(1, tree.height - height);   
        if(parent.Tile_left == null || parent.Tile_left.x > tree.x) parent.Tile_left = tree;
        if(parent.Tile_Right == null || parent.Tile_Right.x < tree.x) parent.Tile_Right = tree;
        if(parent.Tile_Up == null || parent.Tile_Up.y > tree.y) parent.Tile_Up = tree;
        if(parent.Tile_Down == null || parent.Tile_Down.y < tree.y) parent.Tile_Down = tree;
        //y좌표도 위와 같다.
        tree.width = width;
        tree.height= height;
        FillRoom(parent,tree.x,tree.y,tree.width,tree.height); 
    }
    private void FillRoom(Map_Node parent,int x,int y, int width,int height) { //room의 rect정보를 받아서 tile을 set해주는 함수
    for(int i = x; i< x + width; i++)
        {
            for(int j = y; j < y + height; j++)
            {
                parent.tile[i,j] = 1;
            }
        }
    }
    private void ConnectRooms(Map_Node parent, TileNode root)
    {
        if (root.leftNode == null || root.rightNode == null)
        {
            return;
        }

        ConnectRooms(parent, root.leftNode);
        ConnectRooms(parent, root.rightNode);

        TileNode left = root.leftNode;
        TileNode right = root.rightNode;

        int leftCenterX = left.x + left.width / 2;
        int leftCenterY = left.y + left.height / 2;
        int rightCenterX = right.x + right.width / 2;
        int rightCenterY = right.y + right.height / 2;

        if (leftCenterX < rightCenterX)
        {
            for (int i = leftCenterX; i <= rightCenterX; i++)
            {
                parent.tile[i, leftCenterY] = 1;
            }
        }
        else
        {
            for (int i = rightCenterX; i <= leftCenterX; i++)
            {
                parent.tile[i, rightCenterY] = 1;
            }
        }

        if (leftCenterY < rightCenterY)
        {
            for (int j = leftCenterY; j <= rightCenterY; j++)
            {
                parent.tile[rightCenterX, j] = 1;
            }
        }
        else
        {
            for (int j = rightCenterY; j <= leftCenterY; j++)
            {
                parent.tile[leftCenterX, j] = 1;
            }
        }
    }

    
    private void AddTilesInRange(int[,] tileArray, int startX, int endX, int startY, int endY, int value)
{
    for (int i = startX; i <= endX; i++)
    {
        for (int j = startY; j <= endY; j++)
        {
            tileArray[i, j] = value;
        }
    }
}

public void Horiontal_add(Map_Node parent_Left, Map_Node parent_Rigt, TileNode Child_Left, TileNode Child_Right)
{
    int leftNodeCenterX = Child_Left.x + Child_Left.width / 2;
    int leftNodeCenterY = Child_Left.y + Child_Left.height / 2;
    int rightNodeCenterX = Child_Right.x + Child_Right.width / 2;
    int rightNodeCenterY = Child_Right.y + Child_Right.height / 2;
    int upperY = Mathf.Max(leftNodeCenterY, rightNodeCenterY);
    int lowerY = Mathf.Min(leftNodeCenterY, rightNodeCenterY);
    
    AddTilesInRange(parent_Left.tile, leftNodeCenterX, 19, leftNodeCenterY, leftNodeCenterY, 1);
    AddTilesInRange(parent_Rigt.tile, 0, rightNodeCenterX, rightNodeCenterY, rightNodeCenterY, 1);
    AddTilesInRange(parent_Left.tile, 19, 19, lowerY, upperY, 1);
}

public void Vertical_add(Map_Node parent_Up, Map_Node parent_Down, TileNode Child_Up, TileNode Child_Down)
{
    int upNodeCenterX = Child_Up.x + Child_Up.width / 2;
    int upNodeCenterY = Child_Up.y + Child_Up.height / 2;
    int downNodeCenterX = Child_Down.x + Child_Down.width / 2;
    int downNodeCenterY = Child_Down.y + Child_Down.height / 2;
    int upperX = Mathf.Max(upNodeCenterX, downNodeCenterX);
    int lowerX = Mathf.Min(upNodeCenterX, downNodeCenterX);
    
    AddTilesInRange(parent_Up.tile, upNodeCenterX, upNodeCenterX, upNodeCenterY, 19, 1);
    AddTilesInRange(parent_Down.tile, downNodeCenterX, downNodeCenterX, 0, downNodeCenterY, 1);
    AddTilesInRange(parent_Down.tile, lowerX, upperX, 0, 0, 1);
}
}