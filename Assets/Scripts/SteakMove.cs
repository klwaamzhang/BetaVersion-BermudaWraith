using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakMove : MonoBehaviour {

    public LayerMask steakMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;

    // Use this for initialization
    void Start () {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }
	
	// Update is called once per frame
	void Update () {
        // Check to see if there's ground in from of us before moving forward
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, steakMask);

        //If there's no ground, turn around
        if (!isGrounded)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;

        int enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
    }

    public void Sword_Hitted()
    {
        Destroy(gameObject);
    }
}
