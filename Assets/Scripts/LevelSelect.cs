using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public Button FactoryButton;
    public Button GroceryButton;

    // Start is called before the first frame update
    void Start()
    {

        FactoryButton.onClick.AddListener(() => LoadFactory());
        GroceryButton.onClick.AddListener(() => LoadGrocery());
    }

    // Update is called once per frame
    void Update()
    {
        GroceryButton.interactable = false;
    }

    public void LoadFactory()
    {
        SceneManager.LoadScene("FactoryTutorial");
    }

    public void LoadGrocery()
    {
        
    }
}
