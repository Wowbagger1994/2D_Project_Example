using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpScript : MonoBehaviour
{
    public GameObject player;
    private Transform playerTR;
    private float distCameraFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerTR = player.GetComponent<Transform>();
        distCameraFromPlayer = Vector2.Distance(new Vector3(playerTR.position.x, 0, 0), new Vector3(this.GetComponent<Transform>().position.x, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().position = new Vector3(playerTR.position.x + distCameraFromPlayer, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
    }
}
