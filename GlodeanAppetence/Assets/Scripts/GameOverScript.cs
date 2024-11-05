using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text daysLasted;
    [SerializeField]
    TMP_Text[] familyList;

    [SerializeField]
    private Image[] tombstones;

    [SerializeField]
    private Image[] people;

    // Start is called before the first frame update
    void Start()
    {
        daysLasted.text = "You Lasted " + familyScript.Instance.day.ToString() + " Days";

        //SetNames and States
        var i = 0;
        foreach (TMP_Text member in familyList)
        {
            member.text = familyScript.Instance.FamilyNames[i] + " - " + familyScript.Instance.HungerValues[familyScript.Instance.FamilyFoodState[i]] + " - " + familyScript.Instance.HealthValues[familyScript.Instance.FamilyHealthState[i]];
            i++;

            if (familyScript.Instance.FamilyDeathList[i] == 1)
            {
                tombstones[i].enabled = true;
                /*member.text = familyScript.Instance.FamilyNames[i] + " - " + familyScript.Instance.HungerValues[familyScript.Instance.FamilyFoodState[i]];
                i++;*/
            }

            else if (familyScript.Instance.FamilyDeathList[i] == 0)
            {
                people[i].enabled = true;
            }
        }
    }


    public void RestartButton()
    {
        familyScript.Instance.Reset();
        SceneManager.LoadScene("Family");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
