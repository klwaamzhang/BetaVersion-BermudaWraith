using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    //**********************************
    //* Variables for following player *
    //**********************************

    [Header("Game Objects")]

    //reference to the player object's transform
    public Transform player;

    [Header("Variables for Enemy")]

    public float maxTravelDistance;
    //max travel distance is the maximum distance the enemy can travel in every direction equally
    //         x                     x           x = maxTravelDistance
    //   <---------------- O ---------------->

    //movement speed of the enemy
    public int speed;

    //time it waits before damaging the player again
    public float damageWait = 2;

    //reference to the animator of the enemy to change animations
    private Animator anim;

    //check if the enemy is dead
    private bool isDead;

    //reference Transform for the enemy to move towards.
    private Transform relativePlayerTransform;

    //check if the enemy is on ground
    private bool grounded;

    //check to damage or not the player
    private bool playerDamage = false;

    //***********************************
    //* Variables for constant movement *
    //***********************************

    Transform leftPoint, rightPoint;

    //immidiate position to move to
    private Transform currentPoint;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        isDead = false;
        relativePlayerTransform = new GameObject().transform;

        //left point
        leftPoint = new GameObject().transform;
        leftPoint.position = new Vector3(this.transform.position.x - maxTravelDistance, this.transform.position.y, this.transform.position.z);

        //right point
        rightPoint = new GameObject().transform;
        rightPoint.position = new Vector3(this.transform.position.x + maxTravelDistance, this.transform.position.y, this.transform.position.z);

        currentPoint = rightPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            anim.SetBool("walk", false);
        }
        else if (isDead)
        {
            anim.SetTrigger("isDead");
        }
        else if (Math.Abs(player.position.x - this.transform.position.x) <= maxTravelDistance && !playerDamage)
        {
            //follow player
            if (grounded)
            {
                relativePlayerTransform.position = new Vector3(player.transform.position.x, this.transform.position.y, 0);
                //otherwise follow player
                this.transform.position = Vector3.MoveTowards(this.transform.position, relativePlayerTransform.position, Time.deltaTime * speed);//Max distance delta = speed of movement
                anim.SetBool("walk", true);
            }
        }
        else
        {
            //move whithin the max distance
            //makes enemy move towwards that location
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint.position, Time.deltaTime * speed);//Max distance delta = speed of movement

            //check if the thing had moved to the point
            if (this.transform.position == currentPoint.position)
            {
                if (currentPoint == rightPoint)
                {
                    currentPoint = leftPoint;
                }
                else
                {
                    currentPoint = rightPoint;
                }
                Vector3 temp = this.transform.localScale;
                temp.x *= -1;
                this.transform.localScale = temp;
            }
        }

        if (playerDamage)
        {
            if (damageWait >= 1f)
            {
                Debug.Log("Damaging");
                damageWait = 0;
            }
            else
            {
                Debug.Log("Waiting for timer");
                damageWait += Time.deltaTime;
            }
        }

        int enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("walk", false);
            playerDamage = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("walk", true);
            playerDamage = false;
        }
    }

    public void Sword_Hitted()
    {
        Destroy(gameObject);
    }
}
