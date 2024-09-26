using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSignalReceiver : MonoBehaviour
{

    public void LoadNextScene()
    {
        GameManager.Instance.LoadScene("TestEndScene", false);
    }

}
