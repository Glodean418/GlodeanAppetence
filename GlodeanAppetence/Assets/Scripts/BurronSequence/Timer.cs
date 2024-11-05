using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float countDownSeconds = 60f;
    public TMP_Text countDownDisplay;
    [SerializeField]
    private string _loadSceneName = "Family";
    [SerializeField]
    private LevelLoader _levelLoader;

    private float _countDownTime
    {
        get { return countDownSeconds; }
        set { countDownSeconds = value; }
    }
    private bool _countStart;
    // Start is called before the first frame update
    void Start()
    {
        CountStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (_countStart)
        {
            if (_countDownTime > 0)
            {
                _countDownTime -= Time.deltaTime;
                TimeUIUpdate(_countDownTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                _countDownTime = 0;
                CountStop();
                StartCoroutine(_levelLoader.LoadLevel(_loadSceneName));
            }
        }
    }

    public void CountStart()
    {
        _countDownTime = countDownSeconds;
        _countStart = true;
    }

    public void CountStop()
    {
        _countStart = false;
    }

    public void TimeUIUpdate(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        countDownDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
