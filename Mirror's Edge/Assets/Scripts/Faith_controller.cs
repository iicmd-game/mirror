using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faith_controller : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float moveSpeed = 10f;
    public float startMoveSpeed = 10f;
    public  bool isRight = true;
    public float move;
    public float Yspeed;
    
    Animator anim;

    //ground
    public bool isGrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    ///////////////////// 
    public bool canRoll = false;
    public Transform rollCheck;
    float rollRadius = 0.4f;
    public bool isRoll = false;
    public float realx;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        SaveNewGameCheck();
    }
    void FixedUpdate()
    {
        Run();
        
    }
    // Update is called once per frame
    public void Flip()//Проверяем куда смотрит гг и поворачиваем её в обратную сторону
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * (-1);
        transform.localScale = theScale;
    }
    public void Run() {//При нажатии кнопок влево и вправо меняем горизонтальную скорость если гг стоит на земле и не перекатывается 
        if (isGrounded && !isRoll) move = Input.GetAxis("Horizontal");
        anim.SetFloat("Faith_speed", Mathf.Abs(move));
        anim.SetFloat("Yspeed", rigidBody.velocity.y);//действие гравитации на гг
        Yspeed = rigidBody.velocity.y;

        ////////////////
        float move1;
        
            move1 = Input.GetAxis("Horizontal");
            if (Mathf.Abs(move1) < 0.15)
                rigidBody.velocity = new Vector2(10 * move * moveSpeed, rigidBody.velocity.y);
            else if (Mathf.Abs(move) < 0.25)
                rigidBody.velocity = new Vector2(5 * move * moveSpeed, rigidBody.velocity.y);//устанавливаем скорость гг
            else if (Mathf.Abs(move) < 0.5)
                rigidBody.velocity = new Vector2(3.5f * move * moveSpeed, rigidBody.velocity.y);
            else if (Mathf.Abs(move) < 1 && move < 0)
                rigidBody.velocity = new Vector2(2.5f * -0.75f * moveSpeed, rigidBody.velocity.y);
            else if (Mathf.Abs(move) < 1 && move > 0)
                rigidBody.velocity = new Vector2(2.5f * 0.75f * moveSpeed, rigidBody.velocity.y);
            else if (Mathf.Abs(move) == 1)
                rigidBody.velocity = new Vector2(2.5f * move * moveSpeed, rigidBody.velocity.y);
            if (rigidBody.velocity.x > 25 || rigidBody.velocity.x < -25) rigidBody.velocity = new Vector2(2.5f * move * moveSpeed, rigidBody.velocity.y);// Костыль против черезмерной распрыжки
        realx = rigidBody.velocity.x;
        ////////////////
        if (move > 0 && !isRight)//поворот
            Flip();
        else if (move < 0 && isRight)
            Flip();
    }
    void GroundAndRollCheck() {//проверяем с помощью кружка есть ли под ногами земля
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", isGrounded);
      //  if (!isGrounded)
     //       return;
        if (Yspeed == 0)
        {
            anim.SetBool("Roll", false);
            canRoll = false;
        }
         if (Yspeed > 0)
           {
               canRoll = false; 
           }
           
        // if (Yspeed != 0)
        if (Yspeed < 0)
        {
            //canRoll = true;
            canRoll = Physics2D.OverlapCircle(rollCheck.position, rollRadius, whatIsGround);//Если героиня паает и под ней земля , то можно совершить кувырок
        }
    }
    void Isroll()
    {
        anim.SetBool("isRoll", false);
        isRoll = false;
    }
    void Update()
    {
        GroundAndRollCheck();
        if (rigidBody.velocity.y < -40)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -40);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))//если под ногами земля можно прыгнуть
        {
            anim.SetBool("Grounded", false);
            rigidBody.AddForce(new Vector2(0, 1000));
        }
        if(canRoll && Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded)//если героиня движется вниз, под ногами нет земли и нажата стрелка вниз, то при приземлении она совершит кувырок
        {
            anim.SetBool("Roll", true);
            isRoll = true;
            anim.SetBool("isRoll", true);
            Invoke("Isroll", 1.3f);
        }
    }


    //перемещение персонажа если есть сохранение
    void SaveNewGameCheck()
    {
        string key = "MESave";
        if (PlayerPrefs.HasKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            SaveData data = JsonUtility.FromJson<SaveData>(value);
            transform.position = data.playerPosition;
        }
    }

    //остановка песонажа перед перемещением на чекпоинт в случае смерти
    public void Freeze()
    {
        rigidBody.velocity = Vector2.zero;
    }
}
