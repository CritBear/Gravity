using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelButton : MonoBehaviour {

    public void CallGameManager(string buttonName)
    {
        Time.timeScale = 1;

        GameObject.Find("GameManager").SendMessage(buttonName, SendMessageOptions.DontRequireReceiver);
    }
}
