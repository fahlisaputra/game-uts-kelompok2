using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public BoxCollider2D bodyCollider;
    public BoxCollider2D enemyVision;
    public Rigidbody2D EnemyRigidbody;
    public Animator animator;
    public float attackRange = 7f;

    private Transform player;
    private bool PlayerEnter;
    private float speed = 2.5f;
    private bool isFlipped = false;

    public bool isDie = false;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (isDie == false)
            {
                isDie = true;
                Die();
            }
        }
    }
    void Die()
    {
        animator.SetTrigger("Die");
        bodyCollider.enabled = false;
        enemyVision.enabled = false;
        EnemyRigidbody.gravityScale = 0;
        StartCoroutine(ShowItem());
        //Destroy(gameObject);
    }

    IEnumerator ShowItem()
    {
        yield return new WaitForSeconds(2);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0 && !PlayerController.instance.isDie)
        {
            if (Vector2.Distance(player.position, EnemyRigidbody.position) <= 5)
            {
                float dist = player.position.y - EnemyRigidbody.position.y;
                if (Mathf.Abs(dist) < 1f)
                {
                    LookAtPlayer();
                    Vector2 target = new Vector2(player.position.x, EnemyRigidbody.position.y);
                    Vector2 newPos = Vector2.MoveTowards(EnemyRigidbody.position, target, speed * Time.fixedDeltaTime);
                    if ((EnemyRigidbody.position.x - player.position.x) > 1)
                    {
                        EnemyRigidbody.MovePosition(newPos);
                    }
                    animator.SetFloat("Speed", 1f);


                    if (Vector2.Distance(player.position, EnemyRigidbody.position) <= attackRange)
                    {
                        animator.SetTrigger("Shoot");
                        StartCoroutine(Shoot());
                    }
                }
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
    int shoot = 0;
    IEnumerator Shoot()
    {
        shoot++;
        if (shoot == 20)
        {
            yield return new WaitForSeconds(0.7f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shoot = 0;
        }
       
    }
    void LookAtPlayer()
    {
        Vector3 flipepd = transform.localScale;
        flipepd.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipepd;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipepd;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

}
