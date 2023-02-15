using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour
{
    //This script handles the actions of the pointer and the ones the pointer starts by clicking buttons
    
    public SteamVR_LaserPointer laserPointer;
    public VendorManager vendor;
    // Start is called before the first frame update

    public EndFunctionality endFunctionality;
    
    private void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    //Checks which object the pointer is clicking on and does corresponding action
    public void PointerClick (object sender, PointerEventArgs e)
    {
        if (e.target.name == "SweetFruitsButton")
        {
            vendor.FruitsSweet();
        }else if (e.target.name == "SourButton")
        {
            vendor.FruitsSour();
        }else if (e.target.name == "SweetGoodsButton")
        {
            vendor.BakedGoodsSweet();
        }else if (e.target.name == "SavoryButton")
        {
            vendor.BakedGoodsSavoury();
        }else if (e.target.name == "YesButton")
        {
            endFunctionality.endExperiment();
        }else if (e.target.name == "NoButton")
        {
            endFunctionality.continueExperiment();
        }

    }
  
}
