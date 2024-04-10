using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tile_Map_Create : MonoBehaviour
{
    public static Tile_Map_Create instance = null;
    public int[,] way_list = new int[4, 4];
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
        for(int i =0;i<4;i++)
        {
            for(int j = 0 ;j <4 ;j++)
            {
                way_list[i,j] =0;
            }
        }
    }
    public void generate_tile()
    {
        for(int i =0;i < 4;i++)
        {
            for(int j =0;j<4;j++)
            {
                int count =0;
                if(Map_generate.instance.map_list[i,j].way ==0) way_list[i,j]=0;
                else{
                    if(Map_generate.instance.map_list[i,j].way ==1||Map_generate.instance.map_list[i,j].way ==9){
                        if(Map_generate.instance.map_list[i,j].Right_node != null) count+=1;
                        if(Map_generate.instance.map_list[i,j].Left_node != null) count+=2;
                        way_list[i,j] = count;
                    }
                    else if(Map_generate.instance.map_list[i,j].way ==2){
                        if(Map_generate.instance.map_list[i,j].Right_node != null) count+=1;
                        if(Map_generate.instance.map_list[i,j].Left_node != null) count+=2;
                        way_list[i,j] = count+3;
                    }
                    else if(Map_generate.instance.map_list[i,j].way ==3){
                        if(Map_generate.instance.map_list[i,j].Right_node != null) count+=1;
                        if(Map_generate.instance.map_list[i,j].Left_node != null) count+=2;
                        way_list[i,j] = count+6;
                    }
                }
                Debug.Log($"{i},{j} : way_list{way_list[i,j]}");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
