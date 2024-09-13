using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField]
    public GameObject clue1;
    [SerializeField]
    public GameObject clue2;
    [SerializeField]
    public GameObject clue3;
    [SerializeField]
    public GameObject clue4;

    public bool clueFound = true;
    public void ClueFound1()
    {
        if(clueFound == true)
        {
            clue1.GetComponent<Image>().color = Color.blue;
        }
    }

    public void ClueFound2()
    {
        if (clueFound == true)
        {
            clue2.GetComponent<Image>().color = Color.red;
        }

    }

    public void ClueFound3()
    {
        if (clueFound == true)
        {
            clue3.GetComponent<Image>().color = Color.green;
        }

    }

    public void ClueFound4()
    {
        if (clueFound == true)
        {
            clue4.GetComponent<Image>().color = Color.yellow;
        }

    }

    private void Start()
    {
        ClueFound1();
        ClueFound2();
        ClueFound3();
        ClueFound4();
    }


}

