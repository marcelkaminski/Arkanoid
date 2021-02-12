using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public Transform powerup;
    public GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(gm.gameOver)
        {
            return;
        }

        if(!inPlay)
        {
            transform.position = paddle.position;
        }

        if(Input.GetButtonDown("Jump") && !inPlay)
        {
            SoundManagerScript.PlaySound("confirmSound");
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
            
            SoundManagerScript.PlaySound("deathSound");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("Brick"))
        {
            SoundManagerScript.PlaySound("brickHitSound");
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            
            if(brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }

            else
            {
            int randChance = Random.Range(1, 101);
            if(randChance < 50)
            {
                Instantiate(powerup, other.transform.position, other.transform.rotation);
            }

            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);

            gm.UpdateScore(brickScript.points);
            gm.UpdateNumberOfBricks();
            Destroy(other.gameObject);
            }
        }
        else if(other.transform.CompareTag("Paddle"))
        {
            SoundManagerScript.PlaySound("paddleHitSound");
        }
        else if(other.transform.CompareTag("Wall"))
        {
            SoundManagerScript.PlaySound("wallHitSound");
        }
    }

}
