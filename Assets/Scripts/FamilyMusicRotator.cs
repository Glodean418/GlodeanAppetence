using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMusicRotator : MonoBehaviour
{
    public AudioSource SatisfiedMusic;
    public AudioSource HungryMusic;
    public AudioSource StarvingMusic;

    private familyScript fromFamily;

    // Start is called before the first frame update
    void Start()
    {
        fromFamily = GameObject.Find("FamilyManager").GetComponent<familyScript>();

        MusicRotator(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicRotator(int state)
    {
        if (fromFamily.FamilyFoodState[state] == 0)
        {
            SatisfiedMusic.Play();
        }

        if (fromFamily.FamilyFoodState[state] == 1)
        {
            HungryMusic.Play();
        }

        if (fromFamily.FamilyFoodState[state] == 2)
        {
            StarvingMusic.Play();
        }
    }
}
