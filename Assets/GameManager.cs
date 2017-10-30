using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject m_Player;
    public GameObject m_SecondCam;
    public Transform m_SpawnPoint;
    public Text m_MessageText;
    public float m_StartDelay = 3f;

    private WaitForSeconds m_StartWait;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);

        StartCoroutine(GameStarting());
    }

    public void Clear()
    {
        Debug.Log("Goal");
    }

    IEnumerator GameStarting()
    {
        yield return m_StartWait;

        SetPlayer();
        m_MessageText.text = string.Empty;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        Debug.Log("Loop");
        yield return null;
    }

    void SetPlayer()
    {
        m_SecondCam.SetActive(false);
        m_Player.SetActive(true);

        m_Player.transform.position = m_SpawnPoint.position;
        m_Player.transform.rotation = m_SpawnPoint.rotation;
    }

}
