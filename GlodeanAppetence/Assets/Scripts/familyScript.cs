using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class familyScript : MonoBehaviour
{

    [SerializeField] public int[] FamilyFoodState;
    [SerializeField] public int[] FamilyHealthState;
    [SerializeField] public int[] FamilyDeathList;
    [SerializeField] public string[] FamilyNames;

    public string[] HealthValues { get; } = { "Healthy", "Sick", "Bedridden", "Dead" };
    public string[] HungerValues { get; } = { "Fine", "Hungry", "Starving", "Dead" };

    public static familyScript Instance { get; private set; }

    public int day = 0;


    void Awake()
    {
        if (Instance != null)
        {
            //Debug.LogError("There is more than one instance!");
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public bool DayUpdate(bool[] foodList, bool[] medList)
    {
        bool dead = false;
        for (int i = 0; i < foodList.Length; i++)
        {
            if (FamilyDeathList[i] != 1)
            {
                //food logic
                if (foodList[i] == true && FamilyFoodState[i] > 0)
                {
                    FamilyFoodState[i] = FamilyFoodState[i] - 1;
                }
                else if (foodList[i] == false)
                {
                    FamilyFoodState[i] = FamilyFoodState[i] + 1;
                }
                //health logic

                if (FamilyFoodState[i] > 0)
                {
                    if ((Random.Range(0f, 10.0f)) > 5f && day > 1)
                    {
                        FamilyHealthState[i] = FamilyHealthState[i] + 1;
                    }
                }
                if (medList[i] == true && FamilyHealthState[i] > 0)
                {
                    FamilyHealthState[i] = FamilyHealthState[i] - 1;
                }

                if (FamilyFoodState[i] == 3 || FamilyHealthState[i] == 3)
                {
                    FamilyDeathList[i] = 1;
                }
            }

        }
        if (FamilyDeathList[0] == 1)
        {
            dead = true;
        }
        if (FamilyMemberCheck())
        {
            dead = true;
        }
        day++;
        return (dead);
    }
    public void Reset()
    {
        for (int i = 0; i < FamilyFoodState.Length; i++)
        {
            FamilyFoodState[i] = 0;
            FamilyHealthState[i] = 0;
            FamilyDeathList[i] = 0;
        }
        day = 0;

        CurrencySystem.Instance.SetCurrency(350);
    }
    int getHealth(int i)
    {
        return FamilyHealthState[i];
    }

    private bool FamilyMemberCheck()
    {
        var familyDead = false;
        int deaths = 0;
        for (int i = 1; i < FamilyDeathList.Length; i++)
        {
            if (FamilyDeathList[i] == 1)
            {
                deaths++;
            }
            if (deaths >= 3)
            {
                familyDead = true;
            }
            else
            {
                familyDead = false;
            }
        }
        return (familyDead);
    }
    public int getDay()
    {
        return day;
    }

    public string GetFamilyMemberState(int index)
    {
        if (FamilyDeathList[index] == 1)
        {
            return "dead";
        }

        // Priority: Dead > Sick > Hungry > Satisfied
        if (FamilyHealthState[index] > 0)
        {
            return "sick";
        }
        if (FamilyFoodState[index] > 0)
        {
            return "hungry";
        }
        return "satisfied";
    }
    public int GetFamilyMemberIndex(string name)
    {
        for (int i = 0; i < FamilyNames.Length; i++)
        {
            if (FamilyNames[i] == name)
                return i;
        }
        return -1;
    }

}
