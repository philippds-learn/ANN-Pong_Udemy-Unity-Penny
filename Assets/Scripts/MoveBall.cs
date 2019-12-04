using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

    Vector3 ballStartPosition;
    Rigidbody2D rb;
    float speed = 400;
    public AudioSource blip;
    public AudioSource blop;

    // Use this for initialization
    void Start () {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.ballStartPosition = this.transform.position;
        ResetBall();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "backwall")
        {
            this.blop.Play();
        }
        else
        {
            this.blip.Play();
        }
    }

    public void ResetBall()
    {
        this.transform.position = this.ballStartPosition;
        this.rb.velocity = Vector3.zero;
        Vector3 dir = new Vector3(Random.Range(100, 300), Random.Range(-100, 100), 0).normalized;
        this.rb.AddForce(dir * this.speed);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown("space"))
        {
            ResetBall();
        }
	}
}
