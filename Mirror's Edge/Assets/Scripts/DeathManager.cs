using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public float Yspeed;
    public GameObject respawn;
    public Faith_controller fc;
    public GameObject faith;
    public CheckPoint cs;
    public bool isGrounded;
    public bool isRoll;

    public PauseMenu pm;
    public bool isDead;

    private void Start()
    {
        fc = faith.GetComponent<Faith_controller>();
        isDead = false;
    }
    private void Update()
    {
        Yspeed = fc.Yspeed;
        isGrounded = fc.isGrounded;
        isRoll = fc.isRoll;
        if (Yspeed < -30 && isGrounded && !isRoll)
        {
            death();
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            death();
        }
    }

    //вызов функции для показа меню смерти
    public void death()
    {
        isDead = true;
        pm.Dead();
        
    }

    public void RespawnCharacter()
    {
        respawn = cs.CheckPointPosition(); // берем точку респавна из скрипта для чекпоинтов
        fc.Freeze();
        faith.transform.position = respawn.transform.position;
        isDead = false;
    }
}
