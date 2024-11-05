using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FactoryTutorial : MonoBehaviour
{

    public Button Continue;

    [SerializeField]
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        Continue.onClick.AddListener(() => LoadFactory());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFactory()
    {
        StartCoroutine(levelLoader.LoadLevel("Factory"));
    }

}
