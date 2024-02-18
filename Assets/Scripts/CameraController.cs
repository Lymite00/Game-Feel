using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerMovement player;
    public BoxCollider2D bounds;

    private float halfHeight, halfWidth;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position =new Vector3(Mathf.Clamp(player.transform.position.x,bounds.bounds.min.x+ halfWidth,bounds.bounds.max.x )
                ,Mathf.Clamp(player.transform.position.y,bounds.bounds.min.y,bounds.bounds.max.y),-10f);
        }
    }
}
