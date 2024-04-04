using UnityEngine;
using System;
using Unity.VisualScripting;

public class Map_generate : MonoBehaviour
{
    public const int max = 4;
    private Map_Node[,] map_list = new Map_Node[max, max];
    int way,
        next_num;
    void Start()
    {
        for(int i =0;i<max;i++)
        {
            for(int j = 0 ;j<max;j++)
            {
                map_list[i,j] = new Map_Node(new RectInt(i*10,j*10,10,10));
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
                    if(num+count <max)
                    {
                        if(test) way =UnityEngine.Random.Range(0,2);
                        else way = UnityEngine.Random.Range(1,3);
                        if(map_list[i,num+count-1].way!=0)
                        {
                            if(way==2) next_num = num +count;
                            map_list[i,num+count].way = way;
                        }
                    }

                }
                count++;
            }
            num = next_num;
        }
        SetNode();
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
    void SetNode()
    {
        for(int i =0;i <max;i++)
        {
            if(map_list[0,i].way == 9) map_list[0,i].map_type = Map_Node.Map_type.Enterance;
            if(map_list[3,i].way == 2) map_list[3,i].map_type = Map_Node.Map_type.Exit;
        }
    }
    void print()
    {
        for(int i =0;i <max;i++)
        {
            for(int j = 0; j<max ; j++)
            {
                Debug.Log($"{map_list[i,j].way}'s type : {map_list[i,j].map_type}, x : {map_list[i,j].nodeRect.x}, y : {map_list[i,j].nodeRect.y}");
            }
        }

    }
}
