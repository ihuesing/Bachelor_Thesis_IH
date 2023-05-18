using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    public string participantID;
    public VendorManager.VendorType condition;
    public float moneySpent = 0f;

    


    private MenuState menuState = MenuState.IDSelection;
    private enum MenuState
    {
        IDSelection,
        Condition,
        Starting,
        Running
    }

    public VendorManager vManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnGUI()
    {
        var x = 100;
        var y = 100;
        var w = 200;

        GUI.backgroundColor = Color.cyan;
        GUI.skin.textField.fontSize = 30;

        var boxStyle = new GUIStyle(GUI.skin.box)
        {
            
            fontSize = 30,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        var buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 28,
            alignment = TextAnchor.MiddleCenter
        };

        
        var valX = x;
        var valY = y;
        
        switch (menuState)
        {
            case MenuState.IDSelection:
                GUI.Box(new Rect(750, 490, 200, 40), "Participant ID", boxStyle);
                participantID = GUI.TextField(new Rect(750, 530, 200, 40), participantID);

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return"))
                {
                    menuState = MenuState.Condition;
                }

                break;
            
            case MenuState.Condition:
                valX = x;
                GUI.Box(new Rect(valX, 100, w, 80), new GUIContent("ID: "+ participantID), boxStyle);
                valX += w + 2;
                GUI.backgroundColor = Color.cyan;

                if (GUI.Button(new Rect(x, Screen.height/2, w, 80), "Human Vendor", buttonStyle))
                {
                    condition = VendorManager.VendorType.Human;
                    menuState = MenuState.Starting;
                }
                
                if (GUI.Button(new Rect(x*3.5f, Screen.height/2, w, 80), "Robot Vendor", buttonStyle))
                {
                    condition = VendorManager.VendorType.Robot;
                    menuState = MenuState.Starting;
                }

                break;
            
            case MenuState.Starting:
                valX = x;
                GUI.Box(new Rect(valX, 100, w, 80), new GUIContent("ID: "+ participantID), boxStyle);
                
                GUI.backgroundColor = Color.red;
                
                valX += w;

                valX += w+2;
                GUI.Box(new Rect(valX, 100, w, 80), new GUIContent(condition.ToString()), boxStyle);
                
                valX += w+2;
                
                GUI.backgroundColor = Color.cyan;
                valX = x;
                
                valX += w + 2;
                
                valX += w + 2 ;
                
                if (GUI.Button(new Rect(valX, Screen.height/2, w*1.25f, 80), "Start Experiment", buttonStyle))
                {
                    menuState = MenuState.Running;
                    StartExperiment();
                }
                
                break;

                    
            case MenuState.Running:
                    
                valX = x;
               
                int buttonwidth =(int) (w * 2);
                GUI.Box(new Rect(valX, valY, buttonwidth, 80), new GUIContent("ID: "+ participantID), boxStyle);

                valX += buttonwidth +2;
                GUI.Box(new Rect(valX , valY, buttonwidth, 80), new GUIContent("Condition: "+ condition), boxStyle);


                valX += buttonwidth+ 2;
                GUI.Box(new Rect(valX , valY, buttonwidth, 80), new GUIContent("Money Spent: "+ moneySpent), boxStyle);



                break;
                
        }
    }

    public void StartExperiment()
    {

        vManagerScript.SpawnVendor(condition);
        
    }
    
    // Save data to some directory, this should be called when basket is placed on bike
    public void SaveData()
    {
        Debug.Log("Should save now");
        string data = "ID: " + participantID + " | Condition: " + condition + " | Money spent: " + moneySpent; 
        File.WriteAllText("C:/Users/imoge/Documents/Bachelor_Thesis_IH/RobotVendorData" + participantID +".txt", data);

        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
