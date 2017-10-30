using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    public float m_StartingHealth = 100f;

    private GameObject m_GameManager;

    private float m_CurrentHealth;
    private bool m_Dead;


    private void OnEnable()
    {
        m_GameManager = GameObject.Find("GameManager");
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GoalArea")
        {
            m_GameManager.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }
        
    }
}
