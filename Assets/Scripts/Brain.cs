using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public GameObject paddle;
    public GameObject ball;
    Rigidbody2D ballRigidBody;
    float yvel;
    float paddleMinY = 8.8f;
    float paddleMaxY = 17.4f;
    float paddleMaxSpeed = 15;
    public float numSaved = 0;
    public float numMissed = 0;

    ANN ann;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 10f;

        this.ann = new ANN(6, 1, 1, 4, 0.01);
        this.ballRigidBody = this.ball.GetComponent<Rigidbody2D>();
    }

    List<double> Run(double ballPosX, double ballPosY, double ballVelocityX, double ballVeclocityY, double paddlePosX, double paddlePosY, double paddleVelocity, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(ballPosX);
        inputs.Add(ballPosY);
        inputs.Add(ballVelocityX);
        inputs.Add(ballVeclocityY);
        inputs.Add(paddlePosX);
        inputs.Add(paddlePosY);
        outputs.Add(paddleVelocity);

        if(train)
        {
            return this.ann.Train(inputs, outputs);
        }
        else
        {
            return this.ann.CalcOutput(inputs, outputs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float posY = Mathf.Clamp(this.paddle.transform.position.y + (this.yvel * Time.deltaTime * this.paddleMaxSpeed), this.paddleMinY, this.paddleMaxY);
        this.paddle.transform.position = new Vector3(this.paddle.transform.position.x, posY, this.paddle.transform.position.z);

        List<double> output = new List<double>();
        int layerMask = 1 << 9;
        RaycastHit2D hit = Physics2D.Raycast(this.ball.transform.position, this.ballRigidBody.velocity, 1000, layerMask);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "tops")
            {
                Vector3 reflection = Vector3.Reflect(this.ballRigidBody.velocity, hit.normal);
                hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask);
            }

            if (hit.collider != null && hit.collider.gameObject.tag == "backwall")
            {
                float dy = hit.point.y - this.paddle.transform.position.y;

                output = Run(this.ball.transform.position.x,
                            this.ball.transform.position.y,
                            this.ballRigidBody.velocity.x,
                            this.ballRigidBody.velocity.y,
                            this.paddle.transform.position.x,
                            this.paddle.transform.position.y,
                            dy,
                            true);
                this.yvel = (float)output[0];
            }            
        }
        else
        {
            this.yvel = 0;
        }
    }
}
