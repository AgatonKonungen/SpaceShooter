using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    new Camera camera;
    public Vector2 camSize;
    public float desiredShipDistance;
    public float halfHeight;
    public float halfWidth;
    Transform Player;

    // Use this for initialization
    void Start()
    {
        camera = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        camSize = new Vector2(halfWidth, halfHeight);

        if (Player != null)
            transform.position = new Vector3((Player.position.x - desiredShipDistance + camSize.x), transform.position.y, transform.position.z);
    }
}
