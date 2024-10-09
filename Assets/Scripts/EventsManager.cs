using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class EventsManager : MonoBehaviour
{
    public List<Events> events = new List<Events>();
    public GameObject panel;
    public TextMeshProUGUI descriptionText;
    private Scene currentScene;

    private AudioSource eventSFX;

    [SerializeField]
    private AudioSource carBreakdown;

    [SerializeField]
    private AudioSource drought;

    [SerializeField]
    private AudioSource changeOne;

    [SerializeField]
    private AudioSource changeTwo;

    [SerializeField]
    private AudioSource changeThree;

    [SerializeField]
    private AudioSource changeFour;

    [SerializeField]
    private AudioSource changeFive;

    private FamilyMenuScript familyMenuScript;

    private int previousEventIndex = -1;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        familyMenuScript = FindObjectOfType<FamilyMenuScript>();
        AudioSource eventSFX = gameObject.AddComponent<AudioSource>();

        // Add events
        //negative
        events.Add(new Events("Car Breakdown", "Your car broke down, and you had to pay 200 coins for repairs", -200, 0, carBreakdown));
        events.Add(new Events("Speeding Ticket", "You were pulled over for speeding and got a ticket for 100 coins", -100, 0, changeFour));
        events.Add(new Events("Injury", "Work injuries, pay 100 coins", -100, 0, changeTwo));

        events.Add(new Events("Drought", "Drought caused food prices to increase to 80 coins", 80, 0, true, drought));
        events.Add(new Events("Epidemic", "An Epidemic has caused the medicine costs to increase to 300", 0, 300, changeFour));

        //positive
        events.Add(new Events("Lucky Find", "You found 40 coins on the floor when coming out of work", 40, 0, changeOne));
        events.Add(new Events("Lottery", "You won a random lottery at work and were given 100 coins", 100, 0, changeThree));
        events.Add(new Events("Welfare", "Asked government for welfare and got 75 coins", 75, 0, changeFive));
        events.Add(new Events("Charity", "Charity help gave 25 coins", 25, 0, changeOne));
        events.Add(new Events("Promotion", "Your boss notices you, you got 30 extra coins", 30, 0, changeThree));

        OnSceneLoaded();
    }
    public void TriggerRandomEvent()
    {
        if (events.Count == 0) return;

        int randomIndex = Random.Range(0, events.Count);

        while (randomIndex == previousEventIndex)
        {
            randomIndex = Random.Range(0, events.Count);
        }

        previousEventIndex = randomIndex;
        Events selectedEvent = events[randomIndex];

        OpenPanel(selectedEvent.eventDescription);
        AffectPlayer(selectedEvent);
    }

    public void OpenPanel(string description)
    {
        panel.SetActive(true);
        descriptionText.text = description;
    }

    public void AffectPlayer(Events selectedEvent)
    {
        if (selectedEvent.affectsEconomy)
        {
            Debug.Log("Affecting economy");
            if (selectedEvent.foodPrice > 0)
            {
                familyMenuScript.SetFoodCost(selectedEvent.foodPrice);
            }
            if (selectedEvent.medPrice > 0)
            {
                familyMenuScript.SetMedCost(selectedEvent.medPrice);
            }
        }
        else
        {
            CurrencySystem.Instance.AddCurrency(selectedEvent.moneyChange);
            selectedEvent.sfx.Play();
            Debug.Log("Affecting currency");
        }
    }

    private void OnSceneLoaded()
    {
        // Check if the loaded scene is the family scene
        if (currentScene.name == "Family")
        {
            // Debug.Log("Checked scene: Family");
            // Show the random event
            if (familyScript.Instance.getDay() != 0)
            {
                //Debug.Log("Triggered");
                TriggerRandomEvent();
            }
        }

        //Debug.Log()
    }

    public void ExitPanel()
    {
        panel.SetActive(false);
    }
}
