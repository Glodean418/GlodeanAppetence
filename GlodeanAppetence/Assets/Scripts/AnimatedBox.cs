using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimatedBox : MonoBehaviour
{
    public Transform[] desiredPoistions;
    public int currentDesiredPoistions;
    public float distanceThreshold;
    public float speed;
    public bool canMove;
    public Rigidbody2D rb;
    public List<ImagesAndNames> imagesAndNames;
    public Image myRend;
    public string itemName;
    private void Start()
    {
        currentDesiredPoistions = 0;
    }
    void Update()
    {
        if (canMove)
        {
            Move();
        }

    }

    public void Move()
    {

        transform.position += Vector3.right * Time.deltaTime * speed;

    }

    public void InitializeBox(Transform[] wayPoints, string itemName)
    {
        desiredPoistions = wayPoints;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            canMove = true;
        }
    }

}

[System.Serializable]
public struct ImagesAndNames
{
    public Sprite image;
    public string name;
    
}
