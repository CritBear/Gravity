using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoor : MonoBehaviour {

    public Transform leftDoor;
    public Transform rightDoor;

    Transform m_Player;

    GameObject m_GameManager;
    float distance;
    bool isOpenDoor;
    Vector3 leftDoorOriginPos;
    Vector3 rightDoorOriginPos;
    Vector3 leftDoorTargetPos;
    Vector3 rightDoorTargetPos;

    private void Awake()
    {
        m_Player = GameObject.Find("Player").GetComponent<Transform>();
        m_GameManager = GameObject.Find("GameManager");
    }

    private void Start()
    {
        InvokeRepeating("CheckDistance", 0, 0.1f);

        leftDoorOriginPos = leftDoor.localPosition;
        rightDoorOriginPos = rightDoor.localPosition;
        leftDoorTargetPos = leftDoor.localPosition + Vector3.left * 2.1f;
        rightDoorTargetPos = rightDoor.localPosition + Vector3.right * 2.1f;
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, m_Player.position);
        if (distance < 6)
        {
            isOpenDoor = true;
        }
        else
        {
            isOpenDoor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_GameManager.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Update()
    {
        if (isOpenDoor) {
            leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorTargetPos, Time.deltaTime * 3f);
            rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorTargetPos, Time.deltaTime * 3f);
        }
        else
        {
            leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorOriginPos, Time.deltaTime * 3f);
            rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorOriginPos, Time.deltaTime * 3f);
        }
    }
}
