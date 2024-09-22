using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Inventory_Base : MonoBehaviour
{
    [SerializeField] protected GameObject mInventoryBase;
    [SerializeField] protected GameObject mInventorySlotsParent;
    // Start is called before the first frame update
    protected void Awake()
    {
        if (mInventoryBase.activeSelf)
        {
            mInventoryBase.SetActive(false);
        }
        
        // mSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
