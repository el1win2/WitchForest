using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float movePower = 10f;
    public float jumpPower = 20f;
    public GameObject end1;
    public GameObject end2;
    public GameObject end3;
    public GameObject heart;
    public Text Heart;

    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;

    void Start()
    {
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Restart();
        if (alive)
        {
            // Attack();
            Jump();
            Run();
        }

        if (transform.position.y < -50)
        {
            int heart = int.Parse(Heart.text);
            transform.position = new Vector3(0, -5, 0);

            if (heart >= 1)
            {
                heart--;
            }

            if (heart <= 0)
            {
                heart = 0;
                Die();
                Invoke("Dead", 1.5f);
            }
            Heart.text = heart.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mob")
        {
            int heart = int.Parse(Heart.text);

            Hurt();
            if(heart >= 1)
            {
                heart--;
            }

            if (heart <= 0)
            {
                heart = 0;
                Die();
                Invoke("Dead", 1.5f);
            }
            Heart.text = heart.ToString();
        }
        else if (other.gameObject.tag == "Flower")
        {
            TimeZero();
            end1.SetActive(true);
        }
        else if (other.gameObject.tag == "FlowerTrue")
        {
            TimeZero();
            end3.SetActive(true);
        }

        anim.SetBool("isJump", false);
    }

    void TimeZero()
    {
        Time.timeScale = 0.0f;
        heart.SetActive(false);
    }
    
    void Dead()
    {
        end2.SetActive(true);
        Time.timeScale = 0.0f;
        heart.SetActive(false);
    }

    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    /*
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }
    }
    */
    public void Hurt()
    {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-10f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(10f, 1f), ForceMode2D.Impulse);

    }

    public void Die()
    {
        anim.SetTrigger("die");
        alive = false;
    }
    /*
    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
            transform.position = new Vector3(0, -5, 0);
            Time.timeScale = 1.0f;
            heart.SetActive(true);
            Heart.text = "10";
        }
    }*/
}
