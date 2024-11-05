using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    private int currency = 350; //if changed, also change in Reset() function in familyScript

    public static CurrencySystem Instance { get; private set; }

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

    public void AddCurrency(int amount)
    {
        currency += amount;
        
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void SetCurrency(int cur)
    {
        currency = cur;
    }
}
