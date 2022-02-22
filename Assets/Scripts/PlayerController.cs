using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public CharacterController controller; // new way to move
    public float moveSpeed;
    public float jumpForce;

    private Vector3 moveDirection;
    public float gravityScale;


    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        // quit game
        if (Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
            //Debug.Log("Quit");
        }

        float yStore = moveDirection.y;

        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;           // 0 float value
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;           // 0 float value
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);        // moving character 

        if (moveDirection.y < -50) {
            SceneManager.LoadScene("test");
        }
    }
}
