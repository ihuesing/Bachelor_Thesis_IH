using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShoppingList : MonoBehaviour
{

    public SteamVR_Action_Boolean ShoppingListOnOff;
    public SteamVR_Input_Sources handType;
    private MeshRenderer shoppingListMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        ShoppingListOnOff.AddOnStateDownListener(ButtonDown, handType);
        shoppingListMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Button is pressed");
        if (shoppingListMesh.enabled)
        {
            shoppingListMesh.enabled = false;
        }
        else
        {
            shoppingListMesh.enabled = true;
        }
        
    }
}
