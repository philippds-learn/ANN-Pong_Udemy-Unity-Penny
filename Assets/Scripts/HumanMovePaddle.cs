using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovePaddle : MonoBehaviour
{
    float yvel;
    float paddleMinY = 8.8f;
    float paddleMaxY = 17.4f;
    float paddleMaxSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            float posY = Mathf.Clamp(this.transform.position.y + Time.deltaTime * this.paddleMaxSpeed * -1, this.paddleMinY, this.paddleMaxY);
            this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            float posY = Mathf.Clamp(this.transform.position.y + Time.deltaTime * this.paddleMaxSpeed, this.paddleMinY, this.paddleMaxY);
            this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
        }
    }
}
