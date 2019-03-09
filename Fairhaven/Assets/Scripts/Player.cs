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
    public float playerHP = 100;
    public Slider HPbar;
    public bool isDead;
    public bool damaged;
    float moveLimiter = 0.7f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
    public void takeDamage(float damage)
    {
        if (damaged)
        {
            playerHP -= damage;
            HPbar.value = playerHP;
        }
        if(playerHP == 0)
        {
            isDead = true;
        }
    }
}
