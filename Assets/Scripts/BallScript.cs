﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public Transform extraLifePowerup;
    public Transform extendPowerup;
    public Transform shrinkPowerup;
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
            ResetBallPosition();
        }

        if(Input.GetButtonDown("Jump") && !inPlay)
        {
            SoundManagerScript.PlaySound("confirmSound");
            inPlay = true;
            rb.AddForce(new Vector2(0,speed));
        }
    }

    public void ResetBallPosition()
    {
        inPlay = false;
        rb.velocity = Vector2.zero;
        transform.position = paddle.position;
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
            if(randChance < 15)
            {
                Instantiate(extraLifePowerup, other.transform.position, other.transform.rotation);
            }
            else if(randChance < 30)
            {
                Instantiate(extendPowerup, other.transform.position, other.transform.rotation);
            }
            else if(randChance < 45)
            {
                Instantiate(shrinkPowerup, other.transform.position, other.transform.rotation);
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

            rb.velocity = Vector2.zero;
            float hitpoint = other.contacts[0].point.x;
            float paddleCenter = other.gameObject.transform.position.x;
            float difference = paddleCenter - hitpoint;
            if(hitpoint < paddleCenter)
            {
                rb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), speed));
            }
            else
            {
                rb.AddForce(new Vector2(Mathf.Abs(difference * 200), speed));
            }
        }
        else if(other.transform.CompareTag("Wall"))
        {
            SoundManagerScript.PlaySound("wallHitSound");
        }
    }
}
