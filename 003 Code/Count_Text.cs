using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Text : MonoBehaviour
{
    public static Count_Text instance; // 생성목적이 Node분할 순서를 알기위해서 생성한것이기에 추후 삭제할 예정
    TextMeshPro text;
    public int count;
    public Vector2 vec;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = count.ToString();
    }
    
}
