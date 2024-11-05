using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroceryStoreTutorial : MonoBehaviour
{

    public Button Continue;

    [SerializeField]
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        Continue.onClick.AddListener(() => LoadGrocery());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGrocery()
    {
        StartCoroutine(levelLoader.LoadLevel("GroceryStore"));
    }
}
