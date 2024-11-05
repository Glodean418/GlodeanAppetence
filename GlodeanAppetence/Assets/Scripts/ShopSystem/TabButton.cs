using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    
{
    public TabGroup tabGroup;

    public Image icon;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.TabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.TabEntered(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.TabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
        tabGroup.AddToList(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
