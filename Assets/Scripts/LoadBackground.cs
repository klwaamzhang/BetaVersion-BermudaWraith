using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBackground : MonoBehaviour {
    public static int count;
    public GameObject background;
    bool backgroundCreated;
    public int x;
	// Use this for initialization
	void Start () {
        backgroundCreated = false;
	}
	
	// Update is called once per frame
	void Update () {
        x = count;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!backgroundCreated)
            {
                if (count < 4)
                {
                    Vector3 newLocation = transform.position + new Vector3(20.48f, 0f, 0f);
                    Instantiate(background, newLocation, Quaternion.identity);
                    backgroundCreated = true;
                    count++;
                }
                else
                {
                    //game win
                    Vector3 newLocation = transform.position + new Vector3(20.48f, 0f, 0f);
                    Instantiate(background, newLocation, Quaternion.identity);
                    backgroundCreated = true;
                    //replace the above code with game win code.
                }
            }
        }
    }
}
