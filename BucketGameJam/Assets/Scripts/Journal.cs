﻿using UnityEngine.UI;
using UnityEngine;
using System;

public class Journal : MonoBehaviour
{
    public string[] Journalpages;
    public string[] JournalPagesNormalCured;
    public string[] JournalPagesNormalNotCured;
    public string[] JournalPagesSmallEvilCured;
    public string[] JournalPagesSmallEvilNotCured;
    public string[] JournalPagesLargeEvilCured;
    public string[] JournalPagesLargeEvilNotCured;
    public string[] tMonth;
    [Space]
    [Space]
    public Sprite[] jSketch_NormCured;
    public Sprite[] jSketch_NormNotCured;
    public Sprite[] jSketch_MedCured;
    public Sprite[] jSketch_MedNotCured;
    public Sprite[] jSketch_LowCured;
    public Sprite[] jSketch_LowNotCured;
    [Space]
    public Sprite[] jSketch_SisDead;
    [Space]
    [Space]
    public int currentpage = 0;
    public Button TurnLeftPage;
    public Button TurnRightPage;
    public Button close;
    public static GameManager.state[] stateHistory;

    public Sprite[] bookState;
    public Image sketch;
    public Image journal;
    public Text entry;
    public Text title;
    public TextAsset textAssetData;

    public GameObject purpleOverlay;
    public Color[] textColor; // 0: normal, 1: eldritch

    GameManager gm;

    public void Start()
    {
        gm = GameManager.gm;
        CurBookState();
        switch (gm.worldState)
        {
            case GameManager.state.normal:
            case GameManager.state.smallEvil:
                purpleOverlay.gameObject.SetActive(false);
                entry.color = textColor[0];
                title.color = textColor[0];
                break;
            case GameManager.state.largeEvil:
                purpleOverlay.gameObject.SetActive(true);
                entry.color = textColor[1];
                title.color = textColor[1];
                break;
            case GameManager.state.terminator:
                break;
            default:
                break;
        }
        JournalPagesNormalCured = new string[gm.maxDays + 1];
        JournalPagesNormalNotCured = new string[gm.maxDays + 1];
        JournalPagesSmallEvilCured = new string[gm.maxDays + 1];
        JournalPagesSmallEvilNotCured = new string[gm.maxDays + 1];
        JournalPagesLargeEvilCured = new string[gm.maxDays + 1];
        JournalPagesLargeEvilNotCured = new string[gm.maxDays + 1];
        jSketch_NormCured = new Sprite[gm.maxDays + 1];
        jSketch_NormNotCured = new Sprite[gm.maxDays + 1];
        jSketch_MedCured = new Sprite[gm.maxDays + 1];
        jSketch_MedNotCured = new Sprite[gm.maxDays + 1];
        jSketch_LowCured = new Sprite[gm.maxDays + 1];
        jSketch_LowNotCured = new Sprite[gm.maxDays + 1];
        tMonth = new string[gm.maxDays + 1];
        //ReadCSV();
        SetupJournalText();
        //setup journal image
        DisplayPage(gm.curDay);
    }

    void SetupJournalText()
    {
        //entry text index in the scriptable object text array: 
        //  0 - normal cured
        //  1 - normal not cured
        //  2 - small evil cured
        //  3 - small evil not cured
        //  4 - large evil cured
        //  5 - large evil not cured
        for (int i = 0; i < gm.maxDays + 1; i++)
        {
            JournalPagesNormalCured[i] = gm.journalData[i].entryTexts[0];
            JournalPagesNormalNotCured[i] = gm.journalData[i].entryTexts[1];
            JournalPagesSmallEvilCured[i] = gm.journalData[i].entryTexts[2];
            JournalPagesSmallEvilNotCured[i] = gm.journalData[i].entryTexts[3];
            JournalPagesLargeEvilCured[i] = gm.journalData[i].entryTexts[4];
            JournalPagesLargeEvilNotCured[i] = gm.journalData[i].entryTexts[5];
            jSketch_NormCured[i] = gm.journalData[i].sketches[0];
            jSketch_NormNotCured[i] = gm.journalData[i].sketches[1];
            jSketch_MedCured[i] = gm.journalData[i].sketches[2];
            jSketch_MedNotCured[i] = gm.journalData[i].sketches[3];
            jSketch_LowCured[i] = gm.journalData[i].sketches[4];
            jSketch_LowNotCured[i] = gm.journalData[i].sketches[5];
            tMonth[i] = "Month " + i;
        }
    }

    /*void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int numColumns = 5;
        int tableSize = data.Length / numColumns - 1;

        for (int i = 0; i < tableSize; i++)
        {
            Debug.Log(i);
            JournalPagesNormal[i] = TextFixer(data[numColumns * (i + 1)]);
            JournalPagesSmallEvil[i] = TextFixer(data[numColumns * (i + 1) + 1]);
            JournalpagesLargeEvil[i] = TextFixer(data[numColumns * (i + 1) + 2]);
        }
    }*/

    /*string TextFixer(string _string)
    {
        string returnString = _string;

        while (returnString.Contains("`"))
        {
            returnString = returnString.Replace('`', ',');
        }

        while (returnString.Contains("�"))
        {
            int errIndex = returnString.IndexOf('�');

            if (errIndex == 0)
            {
                Debug.LogError("string in excel starts with period or apostrophy! Which one is it?");
                return returnString;
            }

            string leftPart = returnString.Substring(0, errIndex);
            string rightPart = returnString.Substring(errIndex);

            if (rightPart.Length == 1)
            {
                rightPart = ".";
            }
            else if (rightPart[1] == ' ')
            {
                rightPart = "." + rightPart.Substring(1);
            }
            else
            {
                rightPart = "'" + rightPart.Substring(1);
            }

            returnString = leftPart + rightPart;
        }

        return returnString;
    }*/

    public void CurBookState()
    {
        if (GameManager.gm.worldState == GameManager.state.largeEvil)
        {
            journal.sprite = bookState[1];
        }
        else
        {
            journal.sprite = bookState[0];
        }
    }


    /*
                switch (gm.mySister)
                {
                    case GameManager.sisterStatus.sick:
                    case GameManager.sisterStatus.cured:
                        gm.journalSketch[_day] = jSketch_NormCured[_day];
                        break;
                    case GameManager.sisterStatus.dead:
                        gm.journalSketch[_day] = jSketch_NormNotCured[_day];
                        break;
                    default:
                        break;
                }*/
    public void DisplayPage(int _page)
    {
        currentpage = _page;
        title.text = tMonth[_page];

        GameManager.state state = stateHistory[_page];
        switch (state)
        {
            case GameManager.state.smallEvil:
                if (_page < 20)
                {
                    entry.text = JournalPagesSmallEvilCured[_page];
                    gm.journalSketch[_page] = jSketch_MedCured[_page];
                }
                else
                {
                    if (gm.mySister == GameManager.sisterStatus.cured)
                    {
                        entry.text = JournalPagesSmallEvilCured[_page];
                        gm.journalSketch[_page] = jSketch_MedCured[_page];
                    }
                    else
                    {
                        entry.text = JournalPagesSmallEvilNotCured[_page];
                        gm.journalSketch[_page] = jSketch_MedNotCured[_page];
                    }
                }
                break;
            case GameManager.state.largeEvil:
                if (_page < 20)
                {
                    entry.text = JournalPagesLargeEvilCured[_page];
                    gm.journalSketch[_page] = jSketch_LowCured[_page];
                }
                else
                {
                    if (gm.mySister == GameManager.sisterStatus.cured)
                    {
                        entry.text = JournalPagesLargeEvilCured[_page];
                        gm.journalSketch[_page] = jSketch_LowCured[_page];
                    }
                    else
                    {
                        entry.text = JournalPagesLargeEvilNotCured[_page];
                        gm.journalSketch[_page] = jSketch_LowNotCured[_page];
                    }
                }
                break;
            default: // normal world state
                if (_page < 20)
                {
                    entry.text = JournalPagesNormalCured[_page];
                    gm.journalSketch[_page] = jSketch_NormCured[_page];
                }
                else
                {
                    if (gm.mySister == GameManager.sisterStatus.cured)
                    {
                        entry.text = JournalPagesNormalCured[_page];
                        gm.journalSketch[_page] = jSketch_NormCured[_page];
                    }
                    else
                    {
                        entry.text = JournalPagesNormalNotCured[_page];
                        gm.journalSketch[_page] = jSketch_NormNotCured[_page];
                    }
                }
                break;
        }

        sketch.sprite = gm.journalSketch[currentpage];

        TurnLeftPage.gameObject.SetActive(currentpage > 0);
        TurnRightPage.gameObject.SetActive(currentpage < gm.curDay);

        Debug.Log("Day: " + _page);
        Debug.Log("Sanity: " + state);
        Debug.Log("Journal Entry Index: " + _page);
    }

    public void ButtonSound()
    {
        AudioManager.am.PlaySFX("event:/click");
    }

    public void PageFlip(int _change)
    {
        DisplayPage(Mathf.Clamp(currentpage + _change, 0, GameManager.gm.curDay));
    }

    public void JournalClose()
    {
        Destroy(gameObject);
    }
}
