using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject satisfiedState;
    [SerializeField]
    private GameObject hungryState;
    [SerializeField]
    private GameObject sickState;

    private Dictionary<string, Dictionary<string, GameObject[]>> clothingOptions;
    private List<string> familyMembers;

    // Start is called before the first frame update
    void Start()
    {
        InitializeClothingOptions();
        LoadClothingPreferences();
        UpdateAllFamilyClothing();
    }

    private void InitializeClothingOptions()
    {
        clothingOptions = new Dictionary<string, Dictionary<string, GameObject[]>>();

        clothingOptions.Add("satisfied", GetClothingOptionsForEmotion(satisfiedState));
        clothingOptions.Add("hungry", GetClothingOptionsForEmotion(hungryState));
        clothingOptions.Add("sick", GetClothingOptionsForEmotion(sickState));

        familyMembers = new List<string>();
        foreach (var member in clothingOptions["satisfied"].Keys)
        {
            familyMembers.Add(member);
        }
    }
    private void LoadClothingPreferences()
    {
        if (familyScript.Instance.getDay() == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        else
        {
            foreach (var state in clothingOptions)
            {
                foreach (var member in state.Value)
                {
                    string familyMember = member.Key;
                    GameObject[] clothingOptionsForMember = member.Value;

                    // Load saved clothing index for each family member and state
                    int savedIndex = PlayerPrefs.GetInt(familyMember + "_" + state.Key + "_clothingIndex", 0);

                    // Ensure the index is within bounds
                    if (savedIndex >= 0 && savedIndex < clothingOptionsForMember.Length)
                    {
                        // Disable all clothing options for the family member in the current emotional state
                        foreach (GameObject clothing in clothingOptionsForMember)
                        {
                            clothing.SetActive(false);
                        }

                        // Enable the selected clothing option
                        clothingOptionsForMember[savedIndex].SetActive(true);
                    }
                }
            }
        }
    }

    private void UpdateAllFamilyClothing()
    {
        for (int i = 0; i < familyScript.Instance.FamilyNames.Length; i++)
        {
            string memberName = familyScript.Instance.FamilyNames[i];
            UpdateFamilyMemberClothing(memberName);
        }
    }

    public void UpdateFamilyMemberClothing(string familyMember)
    {
        int memberIndex = familyScript.Instance.GetFamilyMemberIndex(familyMember);
        if (memberIndex == -1)
        {
            Debug.LogError($"Family member '{familyMember}' not found.");
            return;
        }

        string currentState = familyScript.Instance.GetFamilyMemberState(memberIndex);

        if (currentState == "dead")
        {
            SetAllClothingInactive(familyMember);
            return;
        }

        foreach (var state in clothingOptions)
        {
            if (state.Key == currentState)
            {
                // Activate clothing for the current state based on saved preferences
                ActivateClothingForMember(state.Key, familyMember);
            }
            else
            {
                // Deactivate clothing for other states
                DeactivateClothingForMember(state.Key, familyMember);
            }
        }
    }

    private void ActivateClothingForMember(string state, string familyMember)
    {
        if (!clothingOptions.ContainsKey(state) || !clothingOptions[state].ContainsKey(familyMember))
        {
            Debug.LogError($"Clothing options for state '{state}' or member '{familyMember}' not found.");
            return;
        }

        GameObject[] clothingOptionsForMember = clothingOptions[state][familyMember];

        // Retrieve saved clothing index
        int savedIndex = PlayerPrefs.GetInt(familyMember + "_" + state + "_clothingIndex", 0);

        // Ensure the index is within bounds
        if (savedIndex < 0 || savedIndex >= clothingOptionsForMember.Length)
        {
            savedIndex = 0; // Fallback to first option
        }

        // Disable all clothing options first
        foreach (GameObject clothing in clothingOptionsForMember)
        {
            clothing.SetActive(false);
        }

        // Activate the saved clothing option
        clothingOptionsForMember[savedIndex].SetActive(true);
    }

    private void DeactivateClothingForMember(string state, string familyMember)
    {
        if (!clothingOptions.ContainsKey(state) || !clothingOptions[state].ContainsKey(familyMember))
        {
            Debug.LogError($"Clothing options for state '{state}' or member '{familyMember}' not found.");
            return;
        }

        GameObject[] clothingOptionsForMember = clothingOptions[state][familyMember];

        foreach (GameObject clothing in clothingOptionsForMember)
        {
            clothing.SetActive(false);
        }
    }

    private void SetAllClothingInactive(string familyMember)
    {
        foreach (var state in clothingOptions)
        {
            if (state.Value.ContainsKey(familyMember))
            {
                GameObject[] clothingOptionsForMember = state.Value[familyMember];
                foreach (GameObject clothing in clothingOptionsForMember)
                {
                    clothing.SetActive(false);
                }
            }
        }
    }

    private Dictionary<string, GameObject[]> GetClothingOptionsForEmotion(GameObject emotionState)
    {
        var memberClothingOptions = new Dictionary<string, GameObject[]>();

        foreach (Transform familyMember in emotionState.transform)
        {
            if (familyMember.CompareTag("FamilyMember"))
            {
                var clothingList = new List<GameObject>();
                foreach (Transform clothingOption in familyMember)
                {
                    if (clothingOption.CompareTag("Clothing"))
                    {
                        clothingList.Add(clothingOption.gameObject);
                    }
                }
                memberClothingOptions.Add(familyMember.name, clothingList.ToArray());
            }
        }

        return memberClothingOptions;
    }

    public void ChangeClothing(string familyMember, int clothingIndex)
    {
        int memberIndex = familyScript.Instance.GetFamilyMemberIndex(familyMember);
        if (memberIndex == -1)
        {
            Debug.LogError($"Family member '{familyMember}' not found.");
            return;
        }

        string[] states = { "satisfied", "hungry", "sick" }; // Apply the change to all emotional states

        foreach (var currentState in states)
        {
            if (!clothingOptions.ContainsKey(currentState) || !clothingOptions[currentState].ContainsKey(familyMember))
            {
                Debug.LogError($"Clothing options for state '{currentState}' or member '{familyMember}' not found.");
                return;
            }

            GameObject[] clothingOptionsForMember = clothingOptions[currentState][familyMember];

            // Ensure the index is within bounds
            if (clothingIndex < 0 || clothingIndex >= clothingOptionsForMember.Length)
            {
                Debug.LogError("Invalid clothing index.");
                return;
            }

            // Disable all clothing options for the family member in the current state
            foreach (GameObject clothing in clothingOptionsForMember)
            {
                clothing.SetActive(false);
            }

            // Enable the selected clothing option
            clothingOptionsForMember[clothingIndex].SetActive(true);
            UpdateAllFamilyClothing();

            // Save the selected clothing index for each state
            PlayerPrefs.SetInt($"{familyMember}_{currentState}_clothingIndex", clothingIndex);
        }

        PlayerPrefs.Save();
    }

}
