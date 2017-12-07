using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {
    
    public float m_StartingHealth = 100f;
    public Image healthImage1;
    public Image healthImage2;
    public Image healthImage3;
    public AudioSource m_DamageAudio;
    public AudioClip m_DamageSound1;
    public AudioClip m_DamageSound2;

    private GameObject m_GameManager;
    CharacterController m_Controller;

    private float m_CurrentHealth;
    private bool m_Dead;
    int m_isCalmDown;
    bool m_isDropping = false;
    float m_DropVelocity;
    float m_DropDamage;

    bool groanDelayCheck = true;

    [HideInInspector] public bool isClear = false;

    private void Start()
    {
        m_GameManager = GameObject.Find("GameManager");
        m_Controller = GetComponent<CharacterController>();
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        UpdateUI();
        InvokeRepeating("LeaveMapCheck", 0, 0.5f);
    }

    void LeaveMapCheck()
    {
        if (isClear) return;

        if (Mathf.Abs(m_Controller.velocity.y) > 80 && !m_Dead)
        {
            m_Dead = true;
            m_CurrentHealth = 0;
            UpdateUI();
            m_GameManager.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
        }
    }
    
    void Damaged(float damage)
    {
        if (m_Dead || isClear)
        {
            return;
        }

        m_CurrentHealth -= damage;
        UpdateUI();

        if (groanDelayCheck)
        {
            if (damage < 70)
            {
                m_DamageAudio.clip = m_DamageSound1;
            }
            else
            {
                m_DamageAudio.clip = m_DamageSound2;
            }
            m_DamageAudio.Play();
            StartCoroutine(GroanDelay());
        }

        if (m_CurrentHealth <= 0)
        {
            m_Dead = true;
            m_GameManager.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
        }

        StartCoroutine(HealthGeneration());
    }

    IEnumerator GroanDelay()
    {
        groanDelayCheck = false;
        yield return new WaitForSeconds(1f);
        groanDelayCheck = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isClear) return;

        if (Mathf.Abs(m_Controller.velocity.y) > 20f && !m_isDropping)
        {
            m_DropVelocity = m_Controller.velocity.y;
            StartCoroutine(DropCheck());
        }
    }

    IEnumerator DropCheck()
    {
        m_isDropping = true;
        yield return new WaitForSeconds(0.1f);
        if(Mathf.Abs(m_DropVelocity - m_Controller.velocity.y) > 20f)
        {
            m_DropDamage = Mathf.Pow(Mathf.Clamp(Mathf.Abs(m_DropVelocity - Mathf.Clamp(m_Controller.velocity.y, -100, 0)) - 20f, 0, 100), 2f);

            Damaged(m_DropDamage);
        }   
        m_isDropping = false;
    }

    IEnumerator HealthGeneration()
    {
        m_isCalmDown++;
        yield return new WaitForSeconds(2f);
        m_isCalmDown--;
        
        while(m_isCalmDown <= 0 && m_CurrentHealth < 100)
        {
            m_CurrentHealth += 0.5f;
            UpdateUI();
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    void UpdateUI()
    {
        healthImage1.color = new Vector4(healthImage1.color.r, healthImage1.color.g, healthImage1.color.b, (100 - Mathf.Clamp(m_CurrentHealth, 0, 100)) / 3 * 0.1f);

        healthImage2.color = new Vector4(healthImage2.color.r, healthImage2.color.g, healthImage2.color.b, (70 - Mathf.Clamp(m_CurrentHealth, 0, 100)) / 4 * 0.1f);

        healthImage3.color = new Vector4(healthImage3.color.r, healthImage3.color.g, healthImage3.color.b, (30 - Mathf.Clamp(m_CurrentHealth, 0, 100)) / 3 * 0.1f);
    }
}

