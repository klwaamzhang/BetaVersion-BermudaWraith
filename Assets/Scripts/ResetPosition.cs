using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    public GameObject player;
    public GameObject mainCamera;

    Vector3 playerPosition = new Vector3(-1.5f, -2.7f, 0);
    Vector3 cameraPosition = new Vector3(-1.8f, -0.59f, -10);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("touch");
        if (other.gameObject.tag == ("Player"))
        {
            player.transform.position = playerPosition;
            mainCamera.transform.position = cameraPosition;
        }
    }
}
