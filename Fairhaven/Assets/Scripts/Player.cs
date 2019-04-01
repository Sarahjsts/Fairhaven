using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;

    public float moveSpeed = 20f;
    public float horizontal;
    public float vertical;
    public int hp = 100;
    static public int playerHP = 100;
   static public int playerMP;
   static public int Att;
   static public int Def;
   static public int Mag;
   static public int Mdef;
    public GameObject gameOver;

    public Slider HPbar;
    public Slider targetBar;
    public GameObject holder;
    public Transform position;
    public static bool isDead;
    public bool targeted = false;
    public bool damaged;
    float moveLimiter = 0.7f;

    private Animator anim;
    public float range;
    public LayerMask enemies;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }
    // moves character
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    //damage to player
    public void TakeDamage(float damage)
    {
        if(playerHP > 0)
        {
            playerHP -= (int)damage;
            HPbar.value = playerHP;
        }else 
        {
            isDead = true;
            if (isDead)

                Time.timeScale = 0;
            gameOver.SetActive(true);
            }
        }

}

