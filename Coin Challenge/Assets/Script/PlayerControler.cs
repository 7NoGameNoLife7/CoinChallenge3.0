
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Vector3 inputDir;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float strafeSpeed = 8;
    [SerializeField] Rigidbody rb;
    float curentVelox;
    float smoothTime = 0.05f;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundMask;
    bool jumpImputPressed;
   

    void Update()
    {

        inputDir = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));      //pour le control des touche 
        inputDir.Normalize ();
        UpdateAnimation();
        if (Input.GetKeyDown(KeyCode.Space) && jumpImputPressed == false) 
        {
            jumpImputPressed = true;    
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();                                                   //appel de la fonction 
        if (jumpImputPressed) Jump();
    }
    void PlayerMovement ()                                             
    {
        //Forward dir
        Vector3 forwardDir = transform.forward*inputDir.z;
        forwardDir.Normalize ();
        forwardDir *= moveSpeed;

        //Strafe dir
        Vector3 strafeDir = Vector3.Cross(Vector3.up, transform.forward)*inputDir.x;
        strafeDir.Normalize ();
        strafeDir *= strafeSpeed;
        
        Vector3 moveDir = forwardDir + strafeDir;

        rb.MovePosition (transform.position + (moveDir*Time.deltaTime));

        //PlayerRotation
        float targetRotation = cam.transform.eulerAngles.y;

        float playerAngleDamp = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref curentVelox, smoothTime);
        transform.rotation = Quaternion.Euler(0, playerAngleDamp, 0);
    }

    void UpdateAnimation ()
    {
        animator.SetFloat("ForwardMove", inputDir.z);
        animator.SetFloat("Straf", inputDir.x);
    }
    
    void Jump ()
    {
        jumpImputPressed = false;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundMask);
        if (!isGrounded) return; 
        GetComponent<Rigidbody>().velocity = new Vector2(0, 6);
        animator.SetTrigger("Jump");
    }
}
