using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

    public float m_StartingHealth = 100f;
    public Text healthText;

    private GameObject m_GameManager;
    CharacterController m_Controller;

    private float m_CurrentHealth;
    private bool m_Dead;
    bool m_isDropping = false;
    float m_DropVelocity;

    private void Start()
    {
        m_GameManager = GameObject.Find("GameManager");
        m_Controller = GetComponent<CharacterController>();
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalArea")
        {
            m_GameManager.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Mathf.Abs(m_Controller.velocity.y) > 25f && !m_isDropping)
        {
            m_DropVelocity = Mathf.Abs(m_Controller.velocity.y);
            StartCoroutine(DropCheck());
        }
    }

    IEnumerator DropCheck()
    {
        m_isDropping = true;
        yield return new WaitForSeconds(0.1f);
        if((m_DropVelocity - Mathf.Abs(m_Controller.velocity.y)) > 1f)
        {
            m_CurrentHealth -= m_DropVelocity - Mathf.Abs(m_Controller.velocity.y);
        }
        UpdateUI();

        if(m_CurrentHealth <= 0)
        {
            m_GameManager.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
        }
        m_isDropping = false;
    }
    
    void UpdateUI()
    {
        healthText.text = "Health: " + Mathf.Clamp((int)m_CurrentHealth,0,100);
    }
}

