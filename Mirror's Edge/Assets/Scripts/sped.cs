using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sped : MonoBehaviour
{
    public Faith_controller fc;
    public GameObject faith;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        fc = faith.GetComponent<Faith_controller>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("sped", fc.realx);
    }
}
