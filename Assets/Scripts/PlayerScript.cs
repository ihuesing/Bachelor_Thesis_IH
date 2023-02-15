using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject fruitscanvas;
    public GameObject bakedgoodscanvas;
    public VendorManager vscript;
    private bool drinksdialogue = false;
    private bool bakedgoodsdialogue = false;
    private bool fruitsdialogue = false;
    private bool veggiedialogue = false;

    private bool juiceVoiceLine = false;
    private bool zucchiniVoiceLine = false;

    public Basket shoppingBasket;
    // Start is called before the first frame update
    void Start()
    {
        //Calls Vendor Script to get access to Speak Function
        //vscript = (VendorScript)vendor.GetComponent(typeof(VendorScript));
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Drinks":
                vscript.AreaEntered(VendorScript.Location.Beverages);
                //vscript.Speak("hum_VeggieSection");
                break;
            case "Baked Goods":
                vscript.AreaEntered(VendorScript.Location.Baked);
                
                //vscript.Speak("hum_VeggieSection");
                break;
            case "Fruits":
                vscript.AreaEntered(VendorScript.Location.Fruit);
                if (!fruitsdialogue)
                {
                    //vscript.Speak("robo_FruitsQuestion");
                    //fruitscanvas.SetActive(true);
                    fruitsdialogue = true;
                }
                //vscript.Speak("hum_VeggieSection");
                break;
            case "Vegetables":
                vscript.AreaEntered(VendorScript.Location.Veggies);
                //checks if dialogue has been played
                if (!veggiedialogue)
                {
                    //plays dialogue and prevents it from being played repeatedly
                    //vscript.Speak("robo_VeggieSection");
                    veggiedialogue = true;
                }
                
                break;
            case "Zucchini":
                // play zucchini voiceline
                if (!zucchiniVoiceLine)
                {
                    vscript.SpecialVoiceLine("Zucchini");
                    // enable zucchini offer
                    shoppingBasket.zucchiniOffer = true;
                    zucchiniVoiceLine = true;
                }
                break;
            case "Juice":
                // play juice voiceline
                if (!juiceVoiceLine)
                {
                    vscript.SpecialVoiceLine("Juice");
                    juiceVoiceLine = true;
                }
                break;
        }
        


    }

}

