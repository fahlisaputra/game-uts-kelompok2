using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public CharacterController2D controller;
    public Animator animator;
    public float moveSpeed;
    public SpriteRenderer warn;
    public bool PlayerEnabled = false;
    public float PlayerHealth = 1000;
    public bool jump = false;
    public bool run = false;
    float horizontalMove = 0f;
    float incrementSpeed = 0;
    public float tapSpeed = 0.5f; //in seconds
    public bool isDie = false;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
        if (!isDie)
        {
            PlayerHealth -= damage;
            animator.SetTrigger("Hurt");
            if (PlayerHealth <= 0)
            {
                isDie = true;
                Die();
            }
        }
    }

    void Die()
    {
        isDie = true;
        animator.SetTrigger("Die");
        this.tag = "PlayerDie";
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerEnabled && !isDie)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed + incrementSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetKeyDown("left shift"))
            {
                if (Mathf.Abs(horizontalMove) > 0.01)
                {
                    animator.SetBool("IsRun", true);
                    if (instance.transform.rotation.y < 0)
                    {
                        incrementSpeed = -10;
                    }
                    else
                    {
                        incrementSpeed = 10;
                    }
                }
            }
            if (Input.GetKeyUp("left shift"))
            {
                animator.SetBool("IsRun", false);
                incrementSpeed = 0;
            }
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJump", true);
            }
          
        } else
        {
            horizontalMove = 0;

        }
    }
    public void showWarnIcon()
    {
        warn.enabled = true;
    }
    public void hideWarnIcon()
    {
        warn.enabled = false;
    }
    public void OnLanding()
    {
        //StartCoroutine(WaitJump());
        animator.SetBool("IsJump", false);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
        
    }
}
