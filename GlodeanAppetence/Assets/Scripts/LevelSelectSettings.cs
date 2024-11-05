using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectSettings : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Settings();
    }

    public void Settings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainPanel.activeInHierarchy)
            {
                mainPanel.SetActive(false);
                settingsPanel.SetActive(true);
            }
            else
            {
                mainPanel.SetActive(true);
                settingsPanel.SetActive(false);
            }

        }
    }

    public void BackButton()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
