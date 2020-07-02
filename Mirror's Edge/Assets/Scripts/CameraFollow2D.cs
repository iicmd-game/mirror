using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public GameObject faith;
    public bool isRight;
    public Faith_controller fc;
    public Vector2 offset = new Vector2(10f, 4f);
    private Transform player;
    public float damping = 1.5f;
    private int lastX;
    public GameObject myCamera;
    public Camera camera;

    public DeathManager dm;
    // Start is called before the first frame update
    void Start()
    {
        fc = faith.GetComponent<Faith_controller>();
        isRight = fc.isRight;
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isRight);
        camera = myCamera.GetComponent<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!dm.isDead)
        {
            camera.orthographicSize = 10 + Mathf.Abs(fc.move * 4); // Чем быстрее бежит гг, тем сильнее отдаляется камера
            isRight = fc.isRight;
            if (player)
            {
                lastX = Mathf.RoundToInt(player.position.x);
                Vector3 target;
                if (isRight)
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
                }
                else
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
                }
                transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);

                transform.position = currentPosition;
            }
        }
    }
    public void FindPlayer (bool playerFaceRight)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerFaceRight)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
        }
    }
}
