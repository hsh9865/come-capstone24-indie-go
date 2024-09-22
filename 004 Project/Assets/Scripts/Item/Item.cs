using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum ItemType  // 아이템 유형
{
    /// <summary>
    /// NONE Type은 아이템을 습득하기위해 E키를 누른경우, 인벤토리에 들어오지 않는다.
    /// 특별한 상호작용이 있는 오브젝트로 취급한다.
    /// </summary>
    NONE                    = 0, //0
    SKILL                   = 1, //1

    //장비 아이템 영역
    //장비 아이템 타입에서 추가되는경우, 증가하는 값으로 추가한다.
    Equipment_HELMET        = 2, //2
    Equipment_ARMORPLATE    = 3, //4
    Equipment_GLOVE         = 4, //8
    Equipment_PANTS         = 5, //16
    Equipment_SHOES         = 6, //32

    //장비 아이템이 아닌 아이템들(소모, 기타, 재료, 퀘스트아이템 등등)
    Etc                     = 7, //64
    Consumable              = 8, //128
    Ingredient              = 9, 
    Quest                   = 10, 
    
}
[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
public class Item : ScriptableObject
{
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField] private int mItemID;

    public int ItemID
    {
        get
        {
            return mItemID;
        }
    }

    [Header("아이템의 중첩이 가능한가?")]
    [SerializeField] private bool mCanOverlap;
    /// <summary>
    /// 아이템이 중첩이 가능한가?
    /// </summary>
    /// <value></value>
    public bool CanOverlap
    {
        get
        {
            return mCanOverlap;
        }
    }

    [Header("사용(상호작용)이 가능한 아이템인가?")]
    [SerializeField] private bool mIsInteractivity;
    /// <summary>
    /// 사용(상호작용)이 가능한 아이템인가?
    /// </summary>
    /// <value></value>
    public bool IsInteractivity
    {
        get
        {
            return mIsInteractivity;
        }
    }    
    
    [Header("아이템을 사용하면 사라지는가?")]
    [SerializeField] private bool mIsConsumable;
    /// <summary>
    /// 아이템을 사용하면 한개씩 사라지는가?
    /// </summary>
    /// <value></value>
    public bool IsConsumable
    {
        get
        {
            return mIsConsumable;
        }
    }

    [Header("아이템을 사용시 쿨타임")]
    [SerializeField] private float mItemCooltime = -1;
    /// <summary>
    /// 아이템의 쿨타임
    /// </summary>
    /// <value></value>
    public float Cooltime
    {
        get
        {
            return mItemCooltime;
        }
    }

    [Header("아이템의 타입")]
    [SerializeField] private ItemType mItemType;
    /// <summary>
    /// 아이템의 유형
    /// </summary>
    /// <value></value>
    public ItemType Type
    {
        get
        {
            return mItemType;
        }
    }

    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private Sprite mItemImage;
    public Sprite Image
    {
        get
        {
            return mItemImage;
        }
    }

}
