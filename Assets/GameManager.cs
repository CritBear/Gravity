using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    public GameObject m_Player;
    public GameObject m_SecondCam;
    public Transform m_SpawnPoint;
    public Text m_MessageText;

    public float m_StartDelay = 3f;
    public float m_EndDelay = 2f;
    
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        StartCoroutine(StageStarting());
    }

    IEnumerator StageStarting()
    {
        yield return m_StartWait;

        SetPlayer();
        m_MessageText.text = string.Empty;
    }

    public void StageReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Clear()
    {
        PlayInfo.stageNum++;
        StartCoroutine(StageEnding());
    }

    IEnumerator StageEnding()
    {
        m_MessageText.text = "Clear";
        yield return m_EndWait;

        SceneManager.LoadScene("Stage" + PlayInfo.stageNum.ToString());
    }

    public void Dead()
    {
        StartCoroutine(DeadEnding());
    }
    
    IEnumerator DeadEnding()
    {
        m_MessageText.text = "You Died";
        m_Player.GetComponent<FirstPersonController>().enabled = false;
        yield return m_EndWait;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetPlayer()
    {
        m_SecondCam.SetActive(false);
        m_Player.SetActive(true);

        m_Player.transform.position = m_SpawnPoint.position;
        m_Player.transform.rotation = m_SpawnPoint.rotation;
    }

}
