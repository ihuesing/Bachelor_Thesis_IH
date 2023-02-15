using System.Data.Common;
using UnityEngine;

public class VendorManager : MonoBehaviour
{
    public enum VendorType
    {
        Robot,
        Human,
        None
    }

    public GameObject robotVendor;
    public GameObject humanVendor;

    public VendorType vendorType = VendorType.None;

    private VendorScript vendor;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SpawnVendor(VendorType newVendorType)
    {
        vendorType = newVendorType;
        if (vendorType == VendorType.Human)
        {
            humanVendor.SetActive(true);
            vendor = humanVendor.GetComponent<VendorScript>();
        }

        if (vendorType == VendorType.Robot)
        {
            robotVendor.SetActive(true);
            vendor = robotVendor.GetComponent<VendorScript>();
        }
    }
    
    
    // Referrals for all functions in VendorScript

    public void FruitsSweet()
    {
        if (vendorType == VendorType.Robot)
        {
            vendor.FruitsSweet("robo_FruitsSweet");
        }

        if (vendorType == VendorType.Human)
        {
            vendor.FruitsSweet("hum_FruitsSweet");
        }
    }

    public void FruitsSour()
    {
        if (vendorType == VendorType.Robot)
        {
            vendor.FruitsSour("robo_FruitsSour");
        }

        if (vendorType == VendorType.Human)
        {
            vendor.FruitsSour("hum_FruitsSour");
        }
    }

    public void BakedGoodsSweet()
    {
        if (vendorType == VendorType.Robot)
        {
            vendor.BakedGoodsSweet("robo_SweetGoods");
        }

        if (vendorType == VendorType.Human)
        {
            vendor.BakedGoodsSweet("hum_SweetGoods");
        }
    }

    public void BakedGoodsSavoury()
    {
        if (vendorType == VendorType.Robot)
        {
            vendor.BakedGoodsSavoury("robo_SavouryGoods");
        }

        if (vendorType == VendorType.Human)
        {
            vendor.BakedGoodsSavoury("hum_SavouryGoods");
        }
    }

    public void MakeReadyToSpeak()
    {
        vendor.readyToSpeak = true;
    }

    public void SpecialVoiceLine(string sentence)
    {
        if (vendorType == VendorType.Robot)
        {
            vendor.SpecialVoiceLine("robo_" + sentence);
        }
        else
        {
            vendor.SpecialVoiceLine("hum_" + sentence);
        }
    }

    public void AreaEntered(VendorScript.Location location)
    {
        vendor.AreaEntered(location, vendorType);
    }
}