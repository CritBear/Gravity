using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    public GameObject m_Player;
    public Transform m_SpawnPoint;
    public Text m_MessageText;

    public float m_StartDelay = 3f;
    public float m_EndDelay = 2f;
    
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        if (PlayInfo.isFirstTry)
        {
            StartCoroutine(StageStarting());
            PlayInfo.isFirstTry = false;
        }
    }

    IEnumerator StageStarting()
    {
        SetPlayer();
        yield return m_StartWait;
        
        m_MessageText.text = string.Empty;
    }

    void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Clear()
    {
        StartCoroutine(StageEnding());
    }

    IEnumerator StageEnding()
    {
        m_MessageText.text = "Clear";
        m_Player.GetComponent<PlayManager>().isClear = true;
        yield return m_EndWait;

        PlayInfo.stageNum++;
        PlayInfo.isFirstTry = true;
        SceneManager.LoadScene("Stage" + PlayInfo.stageNum.ToString());
    }

    void Dead()
    {
        StopAllCoroutines();
        StartCoroutine(DeadEnding());
    }
    
    IEnumerator DeadEnding()
    {
        m_MessageText.text = "You Died";
        m_Player.GetComponent<FirstPersonController>().enabled = false;
        m_Player.GetComponent<UserControl>().enabled = false;
        yield return m_EndWait;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetPlayer()
    {
        m_MessageText.text = "Stage " + PlayInfo.stageNum.ToString();

        m_Player.transform.position = m_SpawnPoint.position;
        m_Player.transform.rotation = m_SpawnPoint.rotation;
    }

}
