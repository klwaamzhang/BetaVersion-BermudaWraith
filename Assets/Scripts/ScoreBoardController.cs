using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardController : MonoBehaviour {

    public static ScoreBoardController instance;

    public Text playerHealthText;
    public Text scoreCounterText;
    public Slider healthSlider;

    public static int health = 100;
    public static int scoreCounter = 0;

    // Use this for initialization
    void Start () {
        instance = this;
        healthSlider.value = health;
        scoreCounterText.text = scoreCounter.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { 
            healthSlider.value = 100;
            health = 100;
        }
	}


    public void UpdateHealth(int health)
    {
        healthSlider.value = health;
    }

    public void HealthDecrease()
    {
        health -= 5;
        healthSlider.value = health;
    }

    public void ScoreCounterIncrease()
    {
        scoreCounter += 10;
        scoreCounterText.text = scoreCounter.ToString();
    }

    public void GetStar1()
    {
        health += 20;
        healthSlider.value = health;
        scoreCounter += 50;
        scoreCounterText.text = scoreCounter.ToString();
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(6).gameObject.SetActive(false);
        
    }

    public void GetStar2()
    {
        health += 20;
        healthSlider.value = health;
        scoreCounter += 50;
        scoreCounterText.text = scoreCounter.ToString();
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(7).gameObject.SetActive(false);
        
    }

    public void GetStar3()
    {
        health += 20;
        healthSlider.value = health;
        scoreCounter += 50;
        scoreCounterText.text = scoreCounter.ToString();
        transform.GetChild(5).gameObject.SetActive(true);
        transform.GetChild(8).gameObject.SetActive(false);
        
    }
}
