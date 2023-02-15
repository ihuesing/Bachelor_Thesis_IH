using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class VendorScript : MonoBehaviour
{
    public enum Location
    {
        Beverages,
        Baked,
        Fruit,
        Veggies
    };

    private Location vendorLocation = Location.Beverages;

    private bool readyToMove = true;
    public bool readyToSpeak = true;

    public Transform beverageLoc;
    public Transform bakedLoc;
    public Transform FruitLoc;
    public Transform VeggiesLoc;

    private NavMeshAgent agent;
    
    private Vector3 bufferLocation;
    private string bufferAudio = "";
    
    private AudioManager audioManager;
    
    public GameObject fruitscanvas;
    public GameObject bakedgoodscanvas;
    
    private bool drinksdialogue = false;
    private bool bakedgoodsdialogue = false;
    private bool fruitsdialogue = false;
    private bool veggiedialogue = false;

    public PointerHandler pointer;

    private Animator thisAnim;

    public VendorManager vendorManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting vendor up");
        
        thisAnim = this.GetComponent<Animator>();

        bufferLocation = new Vector3(-0.785000026f,0,-6.08699989f);

        agent = GetComponent<NavMeshAgent>();

        audioManager = FindObjectOfType<AudioManager>();
        if (vendorManager.vendorType == VendorManager.VendorType.Robot)
        {
            Speak("robo_Intro");
        }
        if (vendorManager.vendorType == VendorManager.VendorType.Human)
        {
            Speak("hum_Intro");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!readyToMove)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 0.6)
            {
                readyToMove = true;
                thisAnim.SetBool("Walking", false);
            }
        }

        if (readyToMove && readyToSpeak)
        {
            
            if (Vector3.Distance(bufferLocation, agent.destination) > 0.6)
            {
                agent.destination = bufferLocation;
                thisAnim.SetBool("Walking", true);
                readyToMove = false;
            } else if (!bufferAudio.Equals(""))
            {
                audioManager.Play(bufferAudio);
                readyToSpeak = false;
                StartCoroutine(StartDialog(bufferAudio));
                bufferAudio = "";
            }
        }
        
        
        
        
    }
    public void Speak(string sentence)
    {
        readyToSpeak = false;
        audioManager.Play(sentence);
    }


    IEnumerator StartDialog(string audioName)
    {
        switch (audioName)
        {
            case "robo_Wine":
                drinksdialogue = true;
                break;
            case "robo_BakedGoods":
                bakedgoodsdialogue = true;
                while (!readyToSpeak)
                {
                    yield return null;
                }
                bakedgoodscanvas.SetActive(true);
                break;
            case "robo_FruitsQuestion":
                fruitsdialogue = true;
                while (!readyToSpeak)
                {
                    yield return null;
                }
                fruitscanvas.SetActive(true);
                break;
            case "robo_VeggieSection":
                veggiedialogue = true; 
                break;
            case "hum_Wine":
                drinksdialogue = true;
                break;
            case "hum_BakedGoods":
                bakedgoodsdialogue = true;
                while (!readyToSpeak)
                {
                    yield return null;
                }
                bakedgoodscanvas.SetActive(true);
                break;
            case "hum_FruitsQuestion":
                fruitsdialogue = true;
                while (!readyToSpeak)
                {
                    yield return null;
                }
                fruitscanvas.SetActive(true);
                break;
            case "hum_VeggieSection":
                veggiedialogue = true; 
                break;
            default:
                break;
        }
    }

    public void SpecialVoiceLine(string name)
    {
        StartCoroutine(PlayVoiceLine(name));
    }

    IEnumerator PlayVoiceLine(string name)
    {
        while(!readyToSpeak | !readyToMove)
        {
            yield return null;
        }
        audioManager.Play(name);
        thisAnim.SetTrigger("Pointing");
        readyToSpeak = false;
        yield break;
    }
    
    public void AreaEntered(Location locationEntered, VendorManager.VendorType vendorType)
    {
        // switch case depending on which location is entered
        switch (locationEntered)
        {
            case Location.Beverages:
                bufferLocation = beverageLoc.position;
                if (!drinksdialogue)
                {
                    if (vendorType == VendorManager.VendorType.Robot)
                    {
                        bufferAudio = "robo_Wine";
                    }
                    else
                    {
                        bufferAudio = "hum_Wine";
                    }
                }
                break;
            case Location.Baked:
                bufferLocation = bakedLoc.position;
                if (!bakedgoodsdialogue)
                {
                    if (vendorType == VendorManager.VendorType.Robot)
                    {
                        bufferAudio = "robo_BakedGoods";
                    }
                    else
                    {
                        bufferAudio = "hum_BakedGoods";
                    }
                    //pointer.enabled = true;
                }
                break;
            case Location.Fruit:
                bufferLocation = FruitLoc.position;
                if (!fruitsdialogue)
                {
                    if (vendorType == VendorManager.VendorType.Robot)
                    {
                        bufferAudio = "robo_FruitsQuestion";
                    }
                    else
                    {
                        bufferAudio = "hum_FruitsQuestion";
                    }
                    //pointer.enabled = true;
                }
                break;
            case Location.Veggies:
                bufferLocation = VeggiesLoc.position;
                if (!veggiedialogue)
                {
                    if (vendorType == VendorManager.VendorType.Robot)
                    {
                        bufferAudio = "robo_VeggieSection";
                    }
                    else
                    {
                        bufferAudio = "hum_VeggieSection";
                    }
                }
                break;
        }
    }
    //Functions for Dialogue progression called in PointerHandler Script
    public void FruitsSour(string sentence)
    {
        Speak(sentence);
        fruitscanvas.SetActive(false);
        //pointer.enabled = false;
    }
    public void FruitsSweet(string sentence)
    {
        Speak(sentence);
        fruitscanvas.SetActive(false);
        //pointer.enabled = false;
    }

    public void BakedGoodsSavoury(string sentence)
    {
        Speak(sentence);
        bakedgoodscanvas.SetActive(false);
        //pointer.enabled = false;
    }

    public void BakedGoodsSweet(string sentence)
    {
        Speak(sentence);
        bakedgoodscanvas.SetActive(false);
        //pointer.enabled = false;
    }
}
