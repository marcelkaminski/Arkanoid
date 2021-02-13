using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float leftWall;
    public float rightWall;
    public GameManager gm;
    private SpriteRenderer sr;
    private BoxCollider2D boxCol;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
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
        else if(other.CompareTag("ExtendPowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            StartCoroutine(ExtendPaddle());
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("ShrinkPowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            StartCoroutine(ShrinkPaddle());
            Destroy(other.gameObject);
        }

    }

     IEnumerator ExtendPaddle()
    {
        this.sr.size = new Vector2(1.75f, 0.16f);
        this.boxCol.size = new Vector2(1.75f, 0.16f);
        yield return new WaitForSeconds( 10 );
        this.sr.size = new Vector2(1.28f, 0.16f);
        this.boxCol.size = new Vector2(1.28f, 0.16f);
    }

    IEnumerator ShrinkPaddle()
    {
        this.sr.size = new Vector2(0.75f, 0.16f);
        this.boxCol.size = new Vector2(0.75f, 0.16f);
        yield return new WaitForSeconds( 10 );
        this.sr.size = new Vector2(1.28f, 0.16f);
        this.boxCol.size = new Vector2(1.28f, 0.16f);
    }

}

