using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //VARIABLES

   [SerializeField] private float moveSpeed;
   [SerializeField] private float walkSpeed;
   [SerializeField] private float runSpeed;

   private Vector3 moveDirection;
   private Vector3 Velocity;
    
    [SerializeField] private bool isGroundet;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float grvity;
    [SerializeField] private float jumpHeight;

   //REFERENCES
   private CharacterController controller;
   private Animator anim;

    private void Start() 
   {
    controller = GetComponent<CharacterController>(); 
    anim = GetComponentInChildren<Animator>();  
   }
   private void Update() 
   {
       Move();

       if(Input.GetKeyDown(KeyCode.Mouse0))
       {
           Attack();
       }
   
   } 
        
   
   private void Move() 
   {

    isGroundet = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

    if(isGroundet && Velocity.y < 0)
    {
        Velocity.y = -2f;
    }
    float moveZ = Input.GetAxis("Vertical");
     
    moveDirection = new Vector3(0, 0, moveZ);
    moveDirection = transform.TransformDirection(moveDirection); 
     if(isGroundet)
     {
      if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
     {
          Walk();
     }
     else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
     {
          Run();
     }
     else if(moveDirection == Vector3.zero)
     {
        Idle();
     }
        moveDirection *= moveSpeed;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
     }
    
    

      controller.Move(moveDirection * Time.deltaTime);

      Velocity.y += grvity * Time.deltaTime;
      controller.Move(Velocity * Time.deltaTime);
   }

   private void Idle()
   {
     anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
   }
 
   private void Walk() 
   {
    moveSpeed = walkSpeed;
    anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
   }
  
   private void Run()
   {
    moveSpeed = runSpeed;
    anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);  
   }
   
   private void Jump()
   {
    Velocity.y =Mathf.Sqrt(jumpHeight * -2 * grvity);
   }

   private void Attack()
   {
       anim.SetTrigger("Attack");
   }
}
