using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour
{
    public int buttonSequenceNumber;
    public Button button; // Reference to the button component

    void Awake()
    {
        if (button == null)
        {
            button = transform.AddComponent<Button>();
        }
    }

    public int GetNumber()
    {
        return buttonSequenceNumber;
    }
}
