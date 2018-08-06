using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public GameObject cam;
    public GameObject player;
    bool followPlayer;
    Vector3 camOffest;
    private Vector3 oldposition;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (followPlayer)
        {
            //follow player code.
            cam.transform.position = new Vector3(player.transform.position.x + camOffest.x, cam.transform.position.y , cam.transform.position.z);
            this.transform.position = new Vector3(player.transform.position.x+2, this.transform.position.y, this.transform.position.z);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            camOffest = cam.transform.position - player.transform.position;
            followPlayer = true;
            oldposition = player.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.position.x < oldposition.x)
            {
                followPlayer = false;
            }
            else
            {
                oldposition = collision.gameObject.transform.position;
                followPlayer = true;
            }
        }
    }
}
