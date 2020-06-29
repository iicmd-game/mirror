using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHelper : MonoBehaviour
{
    public float speed = 0;
    float pos = 0;
    public float scale = 1f;
    public GameObject faith;
    public Faith_controller fc;
    private RawImage image; 
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
        fc = faith.GetComponent<Faith_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = fc.move * scale;
        pos += speed;

        if (pos > 1.0F)

            pos -= 1.0F;

        image.uvRect = new Rect(pos, 0, 1, 1);
    }
}
