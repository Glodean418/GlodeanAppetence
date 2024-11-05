using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FamilyMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playUI;
    [SerializeField]
    private GameObject settingsUI;
    private bool SettingsMenu = false;

    [SerializeField] 
    private GameObject shopUI;
    private bool shopMenu = false;

    [SerializeField]
    private GameObject tutorialBackground;

    [SerializeField]
    private TMP_Text tutorialText;


    [SerializeField]
    bool[] foodList;
    [SerializeField]
    bool[] medList;

    [SerializeField]
    Text[] familyList;

    [SerializeField]
    private TMP_Text currency;

    [SerializeField]
    private TMP_Text Food;
    [SerializeField]
    private TMP_Text Medicine;
    [SerializeField]
    private TMP_Text totalCost;

    [SerializeField]
    private Button nextDayBtn;

    [SerializeField]
    private LevelLoader levelLoader;

    [SerializeField]
    private GameObject[] FoodTogList;

    [SerializeField]
    private GameObject[] MedTogList;

    [SerializeField]
    private int daysToWin = 10;

    [SerializeField]
    private string gameWonScene = "TODO";

    [SerializeField]
    private AudioSource purchaseSFX;

    [SerializeField]
    private AudioClip foodPurchase;

	[SerializeField]
	private AudioClip medPurchase;


	[SerializeField]
    private TMP_Text dayDisplay;

    private int totalCostVal;

    private int foodCost = 60;

    private int medCost = 200;

    private familyScript theFamilyScript;

    private bool NextDayIsClicked = false;

    public void Start()
    {
        nextDayBtn.gameObject.SetActive(true);
        nextDayBtn.onClick.AddListener(OnNextDayButtonClick);
        
        dayDisplay.text = "Day " + familyScript.Instance.day.ToString();
        
        if (familyScript.Instance.day >= daysToWin)
        {
            SceneManager.LoadScene(gameWonScene);
        }

        if(familyScript.Instance.day > 0){
            tutorialBackground.SetActive(false);
            tutorialText.enabled = false;
        }
        
        //SetNames and States
        var i = 0;
        foreach (Text member in familyList)
        {
            member.text = familyScript.Instance.FamilyNames[i] + " - " + familyScript.Instance.HungerValues[familyScript.Instance.FamilyFoodState[i]] + " - " + familyScript.Instance.HealthValues[familyScript.Instance.FamilyHealthState[i]];
            if(familyScript.Instance.FamilyHealthState[i] == 1 || familyScript.Instance.FamilyHealthState[i] == 2){
                MedTogList[i].SetActive(true);
            }
            if(familyScript.Instance.FamilyHealthState[i] == 3 || familyScript.Instance.FamilyFoodState[i] == 3){
                FoodTogList[i].SetActive(false);
                MedTogList[i].SetActive(false);
            }
            i++;
        }
    }

    private void OnDestroy()
    {
        nextDayBtn.onClick.RemoveListener(OnNextDayButtonClick);
    }

    public void Update()
    {
        currency.text = CurrencySystem.Instance.GetCurrency().ToString();

        totalCost.text = CalcTotal().ToString();

        if (CurrencySystem.Instance.GetCurrency() < CalcTotal() && CalcTotal() != 0)
        {
            //totalCost.text = "TOO MUCH!";
            nextDayBtn.gameObject.SetActive(false);
        }
        else
        {
            nextDayBtn.gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            MenuChange();
        }
        if (Input.GetMouseButtonDown(0)){
            tutorialBackground.SetActive(false);
            tutorialText.enabled = false;
        }
    }
    public void MenuChange()
    {
        if(SettingsMenu){
            playUI.SetActive(true);
            settingsUI.SetActive(false);
            SettingsMenu = false;
        }
        else{
            playUI.SetActive(false);
            settingsUI.SetActive(true);
            SettingsMenu = true;
        }
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        familyScript.Instance.Reset();

        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateButton()
    {
        bool dead = familyScript.Instance.DayUpdate(foodList, medList);
        CurrencySystem.Instance.AddCurrency(-CalcTotal());
        if(dead){
            SceneManager.LoadScene("GameOver");
        }
        else{
            StartCoroutine(levelLoader.LoadLevel("LevelSelect"));
        }
    }

    private void OnNextDayButtonClick()
    {
        NextDayIsClicked = true;
        nextDayBtn.interactable = false;
        UpdateButton();
    }

    public void ShopButton()
    {
        if (shopMenu)
        {
            shopUI.SetActive(false);
            shopMenu = false;
        }
        else
        {
            shopUI.SetActive(true);
            shopMenu = true;
        }
    }

    public void FoodButtons(int index)
    {
        if(foodList[index] == true){
            foodList[index] = false;
        }
        else if(foodList[index] == false){
			purchaseSFX.clip = foodPurchase;
			purchaseSFX.Play();
			foodList[index] = true;
        }
    }
    public void MedButtons(int index)
    {
        if(medList[index] == true){
            medList[index] = false;
        }
        else if(medList[index] == false){
            purchaseSFX.clip = medPurchase;
            purchaseSFX.Play();
            medList[index] = true;
        }
    }

    private int CalcTotal()
    {
        totalCostVal = 0;
        foreach (var item in foodList)
        {
            if (item)
            {
                totalCostVal += GetFoodCost();
            }
        }
        foreach (var item in medList)
        {
            if (item)
            {
                totalCostVal += GetMedCost();
            }
        }

        if (CurrencySystem.Instance.GetCurrency() < 0 && totalCostVal <= 0)
        {
            return 0;
        }

        return totalCostVal;
    }

    public void SetFoodCost(int newFoodCost)
    {
        foodCost = newFoodCost;
        Food.text = "Food - " + newFoodCost.ToString();
    }

    public int GetFoodCost()
    {
        return foodCost;
    }

    public void SetMedCost(int newMedCost)
    {
        medCost = newMedCost;
        Medicine.text = "Medicine - " + newMedCost.ToString();
    }

    public int GetMedCost()
    {
        return medCost;
    }

}
