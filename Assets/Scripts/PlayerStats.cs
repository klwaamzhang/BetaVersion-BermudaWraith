using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int health;
    public int lives;
    public Slider healthSlider;
	// Use this for initialization
	void Start () {
        health = 100;
        lives = 3;
	}
	
	// Update is called once per frame
	void Update () {
        healthSlider.value = health;
	}
}
