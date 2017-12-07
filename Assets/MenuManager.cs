using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    void Play(int num)
    {
        PlayInfo.stageNum = num;
        SceneManager.LoadScene("Stage" + num);
    }
}
