using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_change : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite closeSprite;
    public Sprite openSprite;
    public bool open;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DragNDrop fruits))
        {

            renderer.sprite = openSprite;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DragNDrop fruits))
        {

            renderer.sprite = closeSprite;

        }
    }

    public void closeTrash()
    {
        renderer.sprite = closeSprite;
    }
}
