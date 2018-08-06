using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBatController : MonoBehaviour {
    [Header("Flying variables")]
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public int health = 2;
    public bool playerInRange;
    public bool facingAway;
    public bool followOnLookAway;


    private CharacterController_2D playerController;
    private Transform playerTrans;

    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<CharacterController_2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.playerController.transform.position.x < this.transform.position.x && this.playerController.transform.localScale.x < 0)
            || (this.playerController.transform.position.x > this.transform.position.x && this.playerController.transform.localScale.x > 0))
        {
            this.facingAway = true;
        }
        else
        {
            this.facingAway = false;
        }

        if ((this.followOnLookAway && this.facingAway) || (!this.followOnLookAway))
        {
            this.playerInRange = Physics2D.OverlapCircle(this.transform.position, this.playerRange, this.playerLayer);
            if (this.playerInRange)
            {
                // face player
                Vector3 temp = this.transform.localScale;
                if ((playerTrans.position.x > this.transform.position.x && temp.x > 0) ||
                    (playerTrans.position.x < this.transform.position.x && temp.x < 0))
                {
                    temp.x *= -1;
                    this.transform.localScale = temp;
                }

                this.transform.position = Vector3.MoveTowards(this.transform.position, this.playerController.transform.position, this.moveSpeed * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(this.transform.position, this.playerRange);
    }

    public void Sword_Hitted()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (health > 0)
        {
            health--;
            sr.color = Color.white;
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
