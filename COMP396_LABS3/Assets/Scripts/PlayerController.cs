using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed

    private Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Get input from WASD keys
        float moveX = Input.GetAxis("Horizontal"); // A and D (left and right)
        float moveZ = Input.GetAxis("Vertical");   // W and S (forward and backward)

        // Create movement direction based on input
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Apply movement to the player's position
        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
