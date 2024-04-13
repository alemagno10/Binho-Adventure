using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Tutorial no YouTube - https://www.youtube.com/watch?v=PA5DgZfRsAM&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=6
    // https://github.com/nickbota/Unity-Platformer-Episode-6/blob/main/2D%20Tutorial/Assets/Scripts/CameraController.cs

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }



}
