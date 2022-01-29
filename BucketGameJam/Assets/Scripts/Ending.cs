﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text endNameText;
    public Text endingFillerText;


    public string[] endName;
    public string[] endFill;


    int currentEnding = 0;


    private void Start()
    {
        //doublefaced = money goal + sibling healed
        //peaceful = sibling alive + full sanatiy 
        //deserted = sanity normal, sibling dies
        //evil = sibling dies, sanity 0

        if (GameManager.gm.isSiblingAlive == true && Inventory.inventory.money >= GameManager.gm.competeMoneyGoal)
        {
            //peaceful end
            currentEnding = 1;
        }
        else if (GameManager.gm.isSiblingAlive == true && Inventory.inventory.money <= GameManager.gm.competeMoneyGoal)
        {
            //double end
            currentEnding = 2;
        }
        else if (GameManager.gm.isSiblingAlive == false && Inventory.inventory.money < GameManager.gm.competeMoneyGoal )
        {
            //deserter end
            currentEnding = 3;
        }
        else if (GameManager.gm.isSiblingAlive == false && Inventory.inventory.money <= GameManager.gm.competeMoneyGoal)
        {
            //evil end
            currentEnding = 4;
        }
        else
        {
            Debug.Log("Oof");
        }

        endNameText.text = endName[currentEnding];
        endingFillerText.text = endFill[currentEnding];


        GameManager.gm.generalHUD.gameObject.SetActive(false);


    }
}
