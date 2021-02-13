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
    private float halfOfPaddle;
    
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
        halfOfPaddle = boxCol.size.x / 2;
        Debug.Log(transform.position.x);
        Debug.Log(halfOfPaddle);
        if((transform.position.x - halfOfPaddle) < leftWall)
        {
            transform.position = new Vector2((leftWall + halfOfPaddle), transform.position.y);
        }
        if((transform.position.x + halfOfPaddle) > rightWall)
        {
            transform.position = new Vector2((rightWall - halfOfPaddle), transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ExtraLifePowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            gm.UpdateLives(1);
            gm.UpdateScore(50);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("ExtendPowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            StartCoroutine(ExtendPaddle());
            gm.UpdateScore(50);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("ShrinkPowerup"))
        {
            SoundManagerScript.PlaySound("powerupSound");
            StartCoroutine(ShrinkPaddle());
            gm.UpdateScore(50);
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

