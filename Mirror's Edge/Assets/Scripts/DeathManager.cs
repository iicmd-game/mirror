using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public float Yspeed;
    public GameObject respawn;
    public Faith_controller fc;
    public GameObject faith;
    public bool isGrounded;
    public bool isRoll;
    private void Start()
    {
        fc = faith.GetComponent<Faith_controller>();
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
    private void death()
    {
        faith.transform.position = respawn.transform.position;
    }
}
