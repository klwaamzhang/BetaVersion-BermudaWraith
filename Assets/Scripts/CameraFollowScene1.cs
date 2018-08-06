using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScene1 : MonoBehaviour {


    [Header("Player Info")]
    public Transform playerTrans;

    [Header("Camera Settings")]
    public float threshold = 4.0f;
    public float thresholdVT = 3.0f;
    public float thresholdVB = 3.0f;


    // private variables
    private float minX;
    private Vector2 thresholdL;
    private Vector2 thresholdR;

    private Vector2 thresholdT;
    private Vector2 thresholdB;

    // Use this for initialization
    void Start () {
        // Initialize minimum X value with currently placed camara X value
        minX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // define threshold X positions relative to the camera
        thresholdL = new Vector2(transform.position.x - threshold, transform.position.y);
        thresholdR = new Vector2(transform.position.x + threshold, transform.position.y);

        thresholdT = new Vector2(transform.position.x, transform.position.y + thresholdVT);
        thresholdB = new Vector2(transform.position.x, transform.position.y - thresholdVB);

        // move the camera horizontally
        if (playerTrans.position.x < thresholdL.x && transform.position.x > minX) // checks if player has moved left of the thresholdL
        {
            transform.position = new Vector3(playerTrans.position.x + threshold, transform.position.y, transform.position.z);
        }
        else if (playerTrans.position.x > thresholdR.x)  // checks if player has moved right of the thresholdR
        {
            transform.position = new Vector3(playerTrans.position.x - threshold, transform.position.y, transform.position.z);
        }

        // move the camera vertically
        if (playerTrans.position.y < thresholdB.y) // checks if player has moved down of the thresholdB
        {
            transform.position = new Vector3(playerTrans.position.x, transform.position.y - thresholdVB, transform.position.z);
        }
        else if (playerTrans.position.y > thresholdT.y)  // checks if player has moved right of the thresholdT
        {
            transform.position = new Vector3(playerTrans.position.x, transform.position.y + thresholdVT, transform.position.z);
        }
    }

    // Define by Unity to allow Gizmo to be drawn
    private void OnDrawGizmos()
    {
        thresholdL = new Vector2(transform.position.x - threshold, transform.position.y);
        thresholdR = new Vector2(transform.position.x + threshold, transform.position.y);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(thresholdL + new Vector2(0, 100), thresholdL + new Vector2(0, -100));
        Gizmos.DrawLine(thresholdR + new Vector2(0, 100), thresholdR + new Vector2(0, -100));

        Gizmos.DrawLine(thresholdT + new Vector2(100, 0), thresholdT + new Vector2(-100, 0));
        Gizmos.DrawLine(thresholdB + new Vector2(100, 0), thresholdB + new Vector2(-100, 0));
    }
}
