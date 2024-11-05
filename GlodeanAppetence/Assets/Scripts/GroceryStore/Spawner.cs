using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gs_pfb;
    public GameObject Spawn_position;
    public boxScript _box;
    private bool nextFruit;
    public static Spawner Instance;
    public Trash_change trash;

    void Start()
    {
        Instance = this;
    }

    public void change_fruit()
    {
        GameObject gameObject = Instantiate(gs_pfb[Random.Range(0, gs_pfb.Length)]);
        gameObject.transform.position = Spawn_position.gameObject.transform.position;
    }

    void Update()
    {
        if (_box.isOpen)
        {
            change_fruit();
            _box.isOpen = false;
        }
    }
}
