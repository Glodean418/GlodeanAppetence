using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemReader : MonoBehaviour
{
    public ItemObjects item;
    public TMP_Text itemName;
    public TMP_Text itemSequence;
    public GameObject itemSequenceBackground;
    public TMP_Text correctness;
    public Button seeSeqeunceButton;
    public Image itemImage;
    public int itemLength;
    public bool seeSequence = false;


    [SerializeField]
    private float waitDuration;
    private bool listenerAdded = false;
    // Start is called before the first frame update
    void Update()
    {
        itemName.text = item.itemName;
        itemLength = item.itemSequence.Length;
        itemImage.sprite = item.itemSprite;
        if (!listenerAdded)
        {
            seeSeqeunceButton.onClick.AddListener(SeeSequenceSwitch);
            listenerAdded = true;
        }

        if (seeSequence)
        {
            itemSequence.gameObject.SetActive(true);
            itemSequenceBackground.SetActive(true);
        }
        else
        {
            itemSequence.gameObject.SetActive(false);
            itemSequenceBackground.SetActive(false);
        }
    }

    public IEnumerator CorrectnessDisplay(string promt)
    {
        correctness.text = promt;
        yield return new WaitForSeconds(waitDuration);
        correctness.text = "";
    }

    public void SeeSequenceSwitch()
    {
        if (!seeSequence)
        {
            seeSequence = true;
        }
        else
        {
            seeSequence = false;
        }
    }
}
