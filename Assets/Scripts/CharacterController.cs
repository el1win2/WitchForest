using System;
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
    public GameObject Score;
    public Text Heart;
    public Text timeTxt;
    public Text Point;
    public Text bestPoint;

    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;
    float nextTime = 0;

    void Start()
    {
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        nextTime += Time.deltaTime;
        timeTxt.text = nextTime.ToString("N2");

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

                if (PlayerPrefs.HasKey("bestPoint") == false)
                {
                    PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                }
                else
                {
                    if (PlayerPrefs.GetInt("bestPoint") < int.Parse(Point.text))
                    {
                        PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                    }
                }
                bestPoint.text = PlayerPrefs.GetInt("bestPoint").ToString();
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

                if (PlayerPrefs.HasKey("bestPoint") == false)
                {
                    PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                }
                else
                {
                    if (PlayerPrefs.GetInt("bestPoint") < int.Parse(Point.text))
                    {
                        PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                    }
                }
                bestPoint.text = PlayerPrefs.GetInt("bestPoint").ToString();
            }
            Heart.text = heart.ToString();
        }
        else if (other.gameObject.tag == "Flower")
        {
            TimeZero();
            end1.SetActive(true);
            float num = 50000 - nextTime * 500;
            if (num <= 0) num = 0;
            int point = 50000 + (int)num;
            Point.text = point.ToString();

            if (PlayerPrefs.HasKey("bestPoint") == false)
            {
                PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
            }
            else
            {
                if (PlayerPrefs.GetInt("bestPoint") < int.Parse(Point.text))
                {
                    PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                }
            }
            bestPoint.text = PlayerPrefs.GetInt("bestPoint").ToString();

        }
        else if (other.gameObject.tag == "FlowerTrue")
        {
            TimeZero();
            end3.SetActive(true);
            int point = 100000;
            Point.text = point.ToString();

            if (PlayerPrefs.HasKey("bestPoint") == false)
            {
                PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
            }
            else
            {
                if (PlayerPrefs.GetInt("bestPoint") < int.Parse(Point.text))
                {
                    PlayerPrefs.SetInt("bestPoint", int.Parse(Point.text));
                }
            }
            bestPoint.text = PlayerPrefs.GetInt("bestPoint").ToString();
        }

        anim.SetBool("isJump", false);
    }

    void TimeZero()
    {
        Time.timeScale = 0.0f;
        heart.SetActive(false);
        Score.SetActive(true);
    }
    
    void Dead()
    {
        end2.SetActive(true);
        Time.timeScale = 0.0f;
        heart.SetActive(false);
        Score.SetActive(true);
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
