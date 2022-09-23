using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    private GameObject ballReset;
    public bool isGutterBall = false;
    public bool isGameEnd = false;
    private float collisionTime = 0;
    private float maxCollisionTime = 3.0f;
    private float endPathPos = 19;
    private float startPathPos = 35;
    private bool isCollision = false;
    // Start is called before the first frame update
    void Update()
    {
        ballReset = GameObject.Find("BallReset");

        if (gameObject.transform.position.y < 0f && !isGutterBall)
        {
            ballReset = GameObject.Find("BallReset");
            ballReset.GetComponent<BallReset>().SetGutter(gameObject.transform.position);
            isGutterBall = true;
        }

        ballReset.GetComponent<BallReset>().CheckPinScore();
        
        collisionTime += Time.deltaTime;
        if ((collisionTime > maxCollisionTime && isCollision) || (transform.position.z > endPathPos  && transform.position.z < startPathPos  && GetComponent<Rigidbody>().velocity == Vector3.zero)) 
        {
            ballReset.GetComponent<BallReset>().ResetGame(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pin")
            isCollision = true;
    }
}
