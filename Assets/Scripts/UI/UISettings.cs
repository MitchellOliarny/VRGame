using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UISettings : MonoBehaviour
{
    [Header("STEAM VR")]
    // BINDS OPEN UI TO BOOLEAN
    [Header("UI")]
    [SerializeField] private GameObject buyCanvas; // UI CANVAS
    [SerializeField] private bool isActive; // TOGGLES ON AND OFF
    [SerializeField] private List<GameObject> buttonArr; // Array of Shop Buttons


    void Start()
    {
        // Sets hand to the left hand of player
       

        // Sets isActive to false on start
        isActive = false;
        
        // For children count of UI
        for (int i = 0; i < buyCanvas.transform.childCount; i++)
        {
            // If child object is a button
            if (buyCanvas.transform.GetChild(i).tag == "shopbutton")
            {
                // Add the button to buttonArr
                buttonArr.Add(buyCanvas.transform.GetChild(i).gameObject);
            }
        }
    }

    // On Enable Listen for OPENUI Action


    // Calls when OPENUI is Pressed
    // fromAction is previous action
    // fromSource is source that pressed OPENUI

    }

    //-- BUTTON STATES --\\
