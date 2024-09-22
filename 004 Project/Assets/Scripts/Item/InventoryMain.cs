using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMain : Inventory_Base
{
    // Start is called before the first frame update
    public static bool IsInventoryActive = false;
    void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!IsInventoryActive)
                OpenInventory();
            else
                CloseInventory();
        }
    }
    private void OpenInventory()
    {
        mInventoryBase.SetActive(true);
        IsInventoryActive = true;
    }

    public void CloseInventory()
    {
        mInventoryBase.SetActive(false);
        IsInventoryActive = false;
    }

}
