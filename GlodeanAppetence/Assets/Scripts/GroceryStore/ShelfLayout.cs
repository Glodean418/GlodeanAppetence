using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfLayout : MonoBehaviour
{

    public Button shelfButton;
    public GameObject layout;
    public bool isShelfOpen = false;

    private bool listenerAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        layout.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!listenerAdded)
        {
            shelfButton.onClick.AddListener(ViewShelf);
            listenerAdded = true;
        }

        if (!isShelfOpen)
        {
            layout.gameObject.SetActive(false);
        }

        else
        {
            layout.gameObject.SetActive(true);
        }
    }

    public void ViewShelf()
    {
        if (isShelfOpen == false)
        {
            isShelfOpen = true;
        }

        else if (isShelfOpen == true)
        {
            isShelfOpen = false;
        }

    }

}
