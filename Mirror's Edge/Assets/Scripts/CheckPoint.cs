using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool triggered = false;
    public static GameObject[] CheckPointList;
    GameObject respawn;

    void Start()
    {
        CheckPointList = GameObject.FindGameObjectsWithTag("CheckPoint");
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    private void ActivateCheckPoint()
    {
        foreach (GameObject cp in CheckPointList)
        {
            cp.GetComponent<CheckPoint>().triggered = false;
        }

        triggered = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }

    public GameObject CheckPointPosition()
    {
        GameObject result = respawn;
        if (CheckPointList != null)
        {
            foreach (GameObject cp in CheckPointList)
            {
                if (cp.GetComponent<CheckPoint>().triggered)
                {
                    result = cp;
                    break;
                }
            }
        }
        return result;
    }
}
