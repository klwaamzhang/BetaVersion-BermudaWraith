using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour {

    public void Sword_Hitted()
    {
        Destroy(gameObject);
    }
}
