using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBottleController : MonoBehaviour {

    public int bloodCapacity = 50;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) { 
        if (other.tag == ("Player"))
        {
            Debug.Log("Heath Bottle");
            ScoreBoardController.instance.IncreaseHealth(bloodCapacity);

            Destroy(gameObject);
        }
    }
}
