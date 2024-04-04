using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BSP : MonoBehaviour
{
    [SerializeField] Vector2Int mapSize; //원하는 맵의 크기
    [SerializeField] float minimumDevideRate; //공간이 나눠지는 최소 비율
    [SerializeField] float maximumDivideRate; //공간이 나눠지는 최대 비율
    [SerializeField] private GameObject line; //lineRenderer를 사용해서 공간이 나눠진걸 시작적으로 보여주기 위함
    [SerializeField] private GameObject map; //lineRenderer를 사용해서 첫 맵의 사이즈를 보여주기 위함
    [SerializeField] GameObject top_ground;
    [SerializeField] GameObject middle_ground;
    [SerializeField] GameObject bottom_ground;
    
    public GameObject count_text;
    

    [SerializeField] private int maximumDepth; //트리의 높이, 높을 수록 방을 더 자세히 나누게 됨
    int count = 0;
    void Start()
    { 
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        DrawMap(0,0);
        Divide(root, 0,0);
    }
    
    
    
       private void DrawMap(int x, int y) //x y는 화면의 중앙위치를 뜻함
    {
        //기본적으로 mapSize/2라는 값을 계속해서 빼게 될건데, 화면의 중앙에서 화면의 크기의 반을 빼줘야 좌측 하단좌표를 구할 수 있기 때문이다.
        LineRenderer lineRenderer = Instantiate(map).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(x, y) - mapSize / 2); //좌측 하단
        lineRenderer.SetPosition(1, new Vector2(x + mapSize.x, y) - mapSize / 2); //우측 하단
        lineRenderer.SetPosition(2, new Vector2(x + mapSize.x, y + mapSize.y) - mapSize / 2);//우측 상단
        lineRenderer.SetPosition(3, new Vector2(x, y + mapSize.y) - mapSize / 2); //좌측 상단
        
        for(int i = x-(mapSize.x/2)+1 ; i < x + mapSize.x/2;i++)
        {
            Instantiate(top_ground,new Vector2(i,y - mapSize.y/2),quaternion.identity);
            Quaternion angle = Quaternion.Euler(0,0,180f);
            Instantiate(top_ground,new Vector2(i,y + mapSize.y/2),angle);
        }
        for(int i = y-(mapSize.y/2)+1 ; i < y + mapSize.y/2;i++)
        {
            Quaternion angle = Quaternion.Euler(0,0,270f);
            Instantiate(top_ground,new Vector2(x - mapSize.x/2,i),angle);
            angle = Quaternion.Euler(0,0,90f);
            Instantiate(top_ground,new Vector2(x + mapSize.x/2,i),angle);
        }
    }
    void Divide(Node tree,int n,int check)
    {
        if (n == maximumDepth) // 마지막으로 나누는 node를 기준으로 랜덤한 변수를 설정하기 위해
        {
            tree.count = count++;
            GameObject count_txt = Instantiate(count_text); // 생성 순서를 알기위해서 텍스트할당
            count_txt.transform.position = new Vector2(tree.nodeRect.x+ (tree.nodeRect.width/2)-(mapSize.x/2), tree.nodeRect.y+(tree.nodeRect.height/2)-(mapSize.y/2));
            count_txt.GetComponent<Count_Text>().count = count;
            if(check == 1) 
            {
                int type  = UnityEngine.Random.Range(0,4);
                switch(type)
                {
                    case 0:
                        tree.floor_type = Node.Floor_type.Empty;
                        Instantiate(middle_ground,new Vector2(tree.nodeRect.x+ (tree.nodeRect.width/2)-(mapSize.x/2), tree.nodeRect.y+(tree.nodeRect.height/2)-(mapSize.y/2)),Quaternion.identity);
                        break;
                    case 1:
                        tree.floor_type = Node.Floor_type.Enemy;
                        break; 
                    case 2:
                        tree.floor_type = Node.Floor_type.Enviroment;
                        break;
                    case 3:
                        tree.floor_type = Node.Floor_type.Exit;
                        break;
                    default:
                        tree.floor_type = Node.Floor_type.Treasure;
                        break;
                }
                return;
            }
            else return;
        }
        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //가로와 세로중 더 긴것을 구한후, 가로가 길다면 좌, 우로 세로가 더 길다면 위, 아래로 나눠주게 될 것이다.
        int split = Mathf.RoundToInt (UnityEngine.Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
        //나올 수 있는 최대 길이와 최소 길이중에서 랜덤으로 한 값을 선택
        // if(n == maximumDepth-1)
        // {
        //     tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width,split));
        //     tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width , tree.nodeRect.height-split));
        //     DrawLine(new Vector2(tree.nodeRect.x , tree.nodeRect.y+ split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y  + split));
        //     for(int i = tree.nodeRect.x+1; i<tree.nodeRect.x + tree.nodeRect.width;i++ )
        //     {
        //         Instantiate(top_ground,new Vector2(i -(mapSize.x/2), tree.nodeRect.y+ split-(mapSize.y/2)),Quaternion.identity);
        //     }
        // }
        // else{
        if (tree.nodeRect.width >= tree.nodeRect.height) //가로가 더 길었던 경우에는 좌 우로 나누게 될 것이며, 이 경우에는 세로 길이는 변하지 않는다.
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x,tree.nodeRect.y,split,tree.nodeRect.height));
            //왼쪽 노드에 대한 정보다 
            //위치는 좌측 하단 기준이므로 변하지 않으며, 가로 길이는 위에서 구한 랜덤값을 넣어준다.
            tree.rightNode= new Node(new RectInt(tree.nodeRect.x+split, tree.nodeRect.y, tree.nodeRect.width-split, tree.nodeRect.height));
            //우측 노드에 대한 정보다 
            //위치는 좌측 하단에서 오른쪽으로 가로 길이만큼 이동한 위치이며, 가로 길이는 기존 가로길이에서 새로 구한 가로값을 뺀 나머지 부분이 된다.
            DrawLine(new Vector2(tree.nodeRect.x + split, tree.nodeRect.y), new Vector2(tree.nodeRect.x + split, tree.nodeRect.y + tree.nodeRect.height));        
            // if(n == maximumDepth-1)
            // {
            //     for(int i = tree.nodeRect.y+1; i<tree.nodeRect.y + tree.nodeRect.height ; i++)
            //     {
            //         Instantiate(middle_ground,new Vector2(tree.nodeRect.x + split-(mapSize.x/2), i-(mapSize.y/2)),Quaternion.identity); // 생성할시 맵사이즈 절반을 뺴주지 않으면 원치않은 위치에 생성
            //         // 이유는 drawline은 0,0을 중앙값으로 기준을 두고 생성하기에
            //     }
                
            // } 
            //그 후 위 두개의 노드를 나눠준 선을 그리는 함수이다.        
        }
        else
        {
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width,split));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width , tree.nodeRect.height-split));
            DrawLine(new Vector2(tree.nodeRect.x , tree.nodeRect.y+ split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y  + split));

            if(n == maximumDepth-1 || n == maximumDepth-2)
            {
                for(int i = tree.nodeRect.x+1; i<=tree.nodeRect.x + tree.nodeRect.width;i++ )
                {
                    Instantiate(top_ground,new Vector3(i -(mapSize.x/2), tree.nodeRect.y+ split-(mapSize.y/2),1),Quaternion.identity);
                }
            }
            
       //세로가 더 길었던 경우이다. 자세한 사항은 가로와 같다.
        }
        // }
            
        
        tree.leftNode.parNode = tree; //자식노드들의 부모노드를 나누기전 노드로 설정
        tree.rightNode.parNode = tree;
        if(tree.rightNode.nodeRect.x == tree.leftNode.nodeRect.x)
        {
            Divide(tree.rightNode, n + 1,1);// 위아래로 나눈 node만을 구분 짓기위해
        }
        else    Divide(tree.rightNode, n + 1,0);// 가로로 나눈 node
        Divide(tree.leftNode, n + 1,0);
        
    }
    private void DrawLine(Vector2 from, Vector2 to) //from->to로 이어지는 선을 그리게 될 것이다.
    {
        LineRenderer lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, from - mapSize / 2);
        lineRenderer.SetPosition(1, to - mapSize / 2);
    }
}
