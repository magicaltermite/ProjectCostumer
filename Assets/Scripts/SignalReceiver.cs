using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour
{

    public void LoadNextScene()
    {
        Debug.Log("Scene changed");
        GameManager.Instance.LoadScene("Oscar_New_Scene", true);
    }

}
