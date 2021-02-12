using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float leftWall;
    public float rightWall;
    public GameManager gm;


    private void Start()
    {

    }
    
    private void Update()
    {
        if(gm.gameOver)
        {
            return;
        }
        PaddleMovement();
        PaddleCollisionDetect();
    }

    private void PaddleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
    }

    private void PaddleCollisionDetect()
    {
        if(transform.position.x < leftWall)
        {
            transform.position = new Vector2(leftWall, transform.position.y);
        }
        if(transform.position.x > rightWall)
        {
            transform.position = new Vector2(rightWall, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ExtraLifePowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }

    }
}

