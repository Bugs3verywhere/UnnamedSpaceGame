using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Object initialisation
    public GameObject mapOverlay;
    public GameObject[] mapPieces;
    public GameObject blizzardParticles;
    public Rigidbody player;
    public Camera playerCam;

    //Data initialisation
    public int collectedPieces;
    public bool blizzardActive = false;

    //Player movement stats
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 5.0f;
    public float jumpSpeed = 10f;
    public float walkForth;
    public float walkSide;
    public bool touchingGround = true;

    void Update()
    {

        //Map toggle
        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < mapPieces.Length; i++)
            {
                if (i < collectedPieces)
                {
                    mapPieces[i].gameObject.SetActive(!mapPieces[i].gameObject.activeSelf);
                }
            }
        }

        //Increase amount of map pieces collected (DEBUG)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            collectedPieces++;
        }

        //Player controls
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            walkForth = 1;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            walkForth = -1;
        }
        else
        {
            walkForth = 0;
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            walkSide = -1;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            walkSide = 1;
        }
        else
        {
            walkSide = 0;
        }

        if (Input.GetKey(KeyCode.Space) && touchingGround)
        {
            player.linearVelocity = new Vector3(player.linearVelocity.x, jumpSpeed, player.linearVelocity.z);
            touchingGround = false;
        }

        Vector3 move = transform.forward * walkForth + transform.right * walkSide;
        player.linearVelocity = new Vector3(move.x * walkSpeed, player.linearVelocity.y, move.z * walkSpeed);

        //Toggle blizzard effects (DEBUG)
        if (Input.GetKeyDown(KeyCode.B))
        {
            blizzardParticles.SetActive(!blizzardParticles.activeSelf);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check floor collision
        if (collision.collider.CompareTag("Terrain"))
        {
            touchingGround = true;
        }
    }
}
