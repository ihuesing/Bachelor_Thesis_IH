using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class Basket : MonoBehaviour
{
    //public float totalAmountSpent = 0f;
    public GameObject experimentManager;
    private ExperimentManager managerScript;

    public VendorManager vendorScript;
    
    private bool firstApple = true;
    private bool firstWatermelon = true;
    private bool firstZucchini = true;
    private bool secondWine = false;
    private bool secondCake = false;

    public bool zucchiniOffer = false;

    private bool lemonVoiceLine = false;
    private bool garlicVoiceLine = false;
    private bool pretzelVoiceLine = false;
    private bool energyVoiceLine = false;
    
    // Start is called before the first frame update
    void Start()
    {
        managerScript = experimentManager.GetComponent<ExperimentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExecuteAfterTime(float time, GameObject good)
    {
        good.tag = "Untagged";
        
        // short delay to let item settle
        yield return new WaitForSeconds(time);
        
        // no more interaction possible
        Destroy(good.GetComponent<Throwable>());
        Destroy(good.GetComponent<Interactable>());
        Destroy(good.GetComponent<Rigidbody>());
        

        // make child of basket
        good.transform.parent = transform;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // reference to gameobject of collision
        GameObject good = other.gameObject;
        
        // if item is a good
        if (good.tag == "Goods" && good.GetComponent<Goods>().pickedUp)
        {
            // put item in basket
            StartCoroutine(ExecuteAfterTime(0.5f, good));
            
            switch (good.GetComponent<Goods>().type)
            {
                case Goods.ProductType.Apple:
                    if (firstApple)
                    {
                        managerScript.moneySpent += 2f;
                        good.GetComponent<Goods>().price = 0f;
                        firstApple = false;
                    }
                    else
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price;
                        good.GetComponent<Goods>().price = 0f;
                    }
                    break;
                case Goods.ProductType.Watermelon:
                    if (firstWatermelon)
                    {
                        managerScript.moneySpent += 2f;
                        good.GetComponent<Goods>().price = 0f;
                        firstWatermelon = false;
                    }
                    else
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price;
                        good.GetComponent<Goods>().price = 0f;
                    }
                    break;
                case Goods.ProductType.Zucchini:
                    if (firstZucchini && zucchiniOffer)
                    {
                        managerScript.moneySpent += 2.4f;
                        good.GetComponent<Goods>().price = 0f;
                        firstZucchini = false;
                    }
                    else
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price;
                        good.GetComponent<Goods>().price = 0f;
                    }
                    break;
                case Goods.ProductType.Wine:
                    if (secondWine)
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price / 2;
                        good.GetComponent<Goods>().price = 0f;
                        secondWine = false;
                    }
                    else
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price;
                        good.GetComponent<Goods>().price = 0f;
                        secondWine = true;
                    }
                    break;
                case Goods.ProductType.Cake:
                    if (secondCake)
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price / 2;
                        good.GetComponent<Goods>().price = 0f;
                        secondCake = false;
                    }
                    else
                    {
                        managerScript.moneySpent += good.GetComponent<Goods>().price;
                        good.GetComponent<Goods>().price = 0f;
                        secondCake = true;
                    }
                    break;
                case Goods.ProductType.Lemon:
                    if (!lemonVoiceLine)
                    {
                        vendorScript.SpecialVoiceLine("Lemons");
                        lemonVoiceLine = true;
                    }

                    managerScript.moneySpent += good.GetComponent<Goods>().price;
                    good.GetComponent<Goods>().price = 0f;
                    break;
                case Goods.ProductType.Garlic:
                    if (!garlicVoiceLine)
                    {
                        vendorScript.SpecialVoiceLine("Garlic");
                        garlicVoiceLine = true;
                    }

                    managerScript.moneySpent += good.GetComponent<Goods>().price;
                    good.GetComponent<Goods>().price = 0f;
                    break;
                case Goods.ProductType.EnergyDrink:
                    if (!energyVoiceLine)
                    {
                        vendorScript.SpecialVoiceLine("Energydrink");
                        energyVoiceLine = true;
                    }

                    managerScript.moneySpent += good.GetComponent<Goods>().price;
                    good.GetComponent<Goods>().price = 0f;
                    break;
                case Goods.ProductType.Pretzel:
                    if (!pretzelVoiceLine)
                    {
                        vendorScript.SpecialVoiceLine("Pretzels");
                        pretzelVoiceLine = true;
                    }
                    managerScript.moneySpent += good.GetComponent<Goods>().price;
                    good.GetComponent<Goods>().price = 0f;
                    break;
                default:
                    managerScript.moneySpent += good.GetComponent<Goods>().price;
                    good.GetComponent<Goods>().price = 0f;
                    break;
            }
        }
    }
}
