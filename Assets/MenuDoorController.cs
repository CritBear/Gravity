using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDoorController : MonoBehaviour {

    GameObject m_MenuManager;

    private void Start()
    {
        m_MenuManager = GameObject.Find("MenuManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_MenuManager.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
        }
    }
}
