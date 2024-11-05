using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private ShopItem[] shopItems;

    [SerializeField]
    private GameObject shopIcon;

    [SerializeField]
    private GameObject vertContainer;

    private ShopUI shop;

    //TESTING VARS
    private int aliveFamily = 0;
    private int sickFamily = 0;

    // Start is called before the first frame update
    void Start()
    {
        int currentDay = familyScript.Instance.getDay();

        foreach (ShopItem shop in shopItems)
        {
            //Setup
            GameObject newItem = Instantiate(shopIcon, vertContainer.transform);
            ShopUI icon = newItem.GetComponent<ShopUI>();

            if (currentDay == 0)
            {
                //Reset everything
                icon.Setup(shop);
                icon.SetAvailability(true);
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            }
            else
            {
                bool upgradeExists = PlayerPrefs.GetInt(shop.nextUpgrade.itemName + "_available", shop.nextUpgrade.available ? 1 : 0) == 1;
                bool baseExists = PlayerPrefs.GetInt(shop.itemName + "_available", shop.available ? 1 : 0) == 1;
                // icon.SetAvailability(savedAvailability);
                if (!baseExists && !upgradeExists)
                {
                    icon.SetupSoldOut(shop);
                }
                else if (baseExists)
                {
                    icon.Setup(shop);
                }
                else
                {
                    icon.Setup(shop.nextUpgrade);
                }
            }

        }

        /* var i = 0; //TEMP
         aliveFamily = Random.Range(1, 5);
         sickFamily = Random.Range(0, aliveFamily);

         foreach (ShopItem shop in shopItems)
         {
             //Setup
             GameObject newItem = Instantiate(shopIcon);
             ShopUI icon = newItem.GetComponent<ShopUI>();

             //Create new slot
             icon.itemName.text = shop.itemName;


             //LOGIC PER ITEM (TEMP)
             if (i == 0)//Food
             {
                 icon.itemCost.text = (shop.cost* aliveFamily).ToString();
                 newItem.transform.parent = vertContainer.transform;
             }
             else //Med
             {
                 icon.itemCost.text = (shop.cost * sickFamily).ToString();
                 if (sickFamily > 0)
                 {
                     newItem.transform.parent = vertContainer.transform;
                 }
                 else
                 {
                     Destroy(newItem);
                 }
             }
             icon.itemIcon.sprite = shop.itemIcon;
             i++;
         }

         Debug.Log("alive "+aliveFamily);
         Debug.Log("sick "+sickFamily);*/
    }
}
