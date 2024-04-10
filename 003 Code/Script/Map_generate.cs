using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class Map_generate : MonoBehaviour
{
    public static Map_generate instance;
    public const int max = 4;
    public Map_Node[,] map_list = new Map_Node[max, max];
    public GameObject ct;
    public GameObject nt;
    int way,
        next_num;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                map_list[i, j] = new Map_Node(new RectInt(j * 10, -i * 10, 10, 10));
            }
        }
        map_generate();

    }
    void map_generate()
    {

        int num = UnityEngine.Random.Range(0, max);
        map_list[0, num].way = 9;
        for (int i = 0; i < max; i++)
        {
            if (i != 0) map_list[i, num].way = 3;
            int count = 1;
            while (true)
            {
                bool test = find(i);
                if (num - count < 0 && num + count > max - 1)
                {
                    if (!test)
                    {
                        if (map_list[i, 0].way == 1)
                        {
                            next_num = 0;
                            map_list[i, 0].way = 2;
                        }
                        else
                        {
                            next_num = max - 1;
                            map_list[i, max - 1].way = 2;
                        }
                    }
                    break;
                }
                if (num == 0 || num == max - 1)
                {
                    if (count == 1)
                    {
                        way = UnityEngine.Random.Range(1, 3);
                    }
                    else
                    {
                        if (!test)
                        {
                            if (!test)
                            {
                                if (count == max - 1) way = 2;
                                else way = UnityEngine.Random.Range(1, 3);
                            }
                        }
                        else way = UnityEngine.Random.Range(0, 2);
                    }
                    if (num == 0 && map_list[i, num + count - 1].way != 0)
                    {
                        if (way == 2) next_num = count;
                        map_list[i, count].way = way;
                    }
                    else if (num == max - 1 && map_list[i, num - count + 1].way != 0)
                    {
                        if (way == 2) next_num = max - count - 1;
                        map_list[i, max - count - 1].way = way;
                    }
                }
                else
                {
                    if (num - count > -1)
                    {
                        if (test) way = UnityEngine.Random.Range(0, 2);
                        else way = UnityEngine.Random.Range(0, 3);
                        if (map_list[i, num - count + 1].way != 0)
                        {
                            if (way == 2) next_num = num - count;
                            map_list[i, num - count].way = way;
                        }
                    }
                    test = find(i);
                    if (num + count < max)
                    {
                        if (test) way = UnityEngine.Random.Range(0, 2);
                        else way = UnityEngine.Random.Range(1, 3);
                        if (map_list[i, num + count - 1].way != 0)
                        {
                            if (way == 2) next_num = num + count;
                            map_list[i, num + count].way = way;
                        }
                    }

                }
                count++;
            }
            num = next_num;
        }
        SetNode();
        Set_Type();
        print();
    }

    bool find(int i)
    {
        for (int j = 0; j < max; j++)
        {
            if (map_list[i, j].way == 2) return true;
        }
        return false;
    }
    void SetNode() // node를 할당해서 추후 맵생성에 있어서 원할하게할 목적
    {
        for (int i = 0; i < max; i++)
        {
            if (map_list[i, 3].way == 3)
            {
                map_list[i - 1, 3].Down_node = map_list[i, 3];
                map_list[i, 3].Up_node = map_list[i - 1, 3];
            }
            for (int j = 1; j < max - 1; j++)
            {
                if (map_list[i, j].way != 0)
                {
                    if (map_list[i, j].way == 3)
                    {
                        map_list[i - 1, j].Down_node = map_list[i, j];
                        map_list[i, j].Up_node = map_list[i - 1, j];
                    }
                    if (map_list[i, j - 1].way != 0)
                    {
                        map_list[i, j].Left_node = map_list[i, j - 1];
                        map_list[i, j - 1].Right_node = map_list[i, j];
                    }
                    if (map_list[i, j + 1].way != 0)
                    {
                        map_list[i, j].Right_node = map_list[i, j + 1];
                        map_list[i, j + 1].Left_node = map_list[i, j];
                    }
                }
            }
        }
    }
    void Set_Type()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int rand = UnityEngine.Random.Range(0, 4);
                if (map_list[0, j].way == 9) map_list[0, j].map_type = Map_Node.Map_type.Enterance;
                else if (map_list[3, j].way == 2){
                    map_list[3, j].map_type = Map_Node.Map_type.Exit;
                    map_list[3,j].way = 9;
                }
                else
                {
                    map_list[i, j].map_type = (Map_Node.Map_type)rand;
                }

            }
        }
        Tile_Map_Create.instance.generate_tile();
    }
    void print() // 문자열을 출력해서 debug를 위한 목적
    {
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                GameObject count_txt = Instantiate(ct); // 생성 순서를 알기위해서 텍스트할당
                // GameObject node_txt = Instantiate(nt);
                count_txt.transform.position = new Vector2(map_list[i, j].nodeRect.x + 5, map_list[i, j].nodeRect.y - 5);
                count_txt.GetComponent<Count_Text>().count = map_list[i, j].way;
                // node_txt.transform.position = new Vector2(map_list[i, j].nodeRect.x + 5, map_list[i, j].nodeRect.y - 6);
                // node_txt.GetComponent<Count_Text>().type = map_list[i, j].map_type.ToString();
                // Debug.Log($"{i},{j} {map_list[i,j].map_type}");
            }
        }

    }
}
