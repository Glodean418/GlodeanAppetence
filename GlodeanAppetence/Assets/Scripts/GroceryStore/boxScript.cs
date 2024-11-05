using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer _box;
    private Vector2 orginalPosition;
    public bool isOpen;

    void Awake()
    {
        isOpen = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        orginalPosition = transform.position;
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        isOpen = true;
        _box.transform.localScale = new Vector3(36f, 33f);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
