using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
   [SerializeField] private float ms;

   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Animator animator;

   private Vector2 dir;

   private void FixedUpdate()
   {
      Move();
      SaveLastDirection();
   }

   private void Move()
   {
      dir.x = Input.GetAxisRaw("Horizontal");
      dir.y = Input.GetAxisRaw("Vertical");
      rb.MovePosition(rb.position + dir * ms * Time.deltaTime);

      animator.SetFloat("Horizontal", dir.x);
      animator.SetFloat("Vertical", dir.y);
      animator.SetFloat("Moving", dir.magnitude);
   }

   private void SaveLastDirection(){
    if(dir.x == 1 || dir.x == -1 || dir.y == 1 || dir.y == -1){
        animator.SetFloat("lastMoveX", dir.x);
        animator.SetFloat("lastMoveY", dir.y);
    }
   }
}
