using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            Debug.Log("star");
            if (!ScoreBoardController.instance.transform.GetChild(3).gameObject.activeInHierarchy)
            {
                ScoreBoardController.instance.GetStar1();
                Debug.Log("get1");
            }else if (!ScoreBoardController.instance.transform.GetChild(4).gameObject.activeInHierarchy)
            {
                ScoreBoardController.instance.GetStar2();
                Debug.Log("get2");
            }
            else
            {
                ScoreBoardController.instance.GetStar3();
                Debug.Log("get3");
            }
            Destroy(gameObject);
        }
        
        
    }
}
