using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    bool isMoving;
    private Vector2 input;
    private Animator animator;

    private void Awake(){
        animator = GetComponentInChildren<Animator>();
    }

    private void Update(){
        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x != 0) input.y = 0;
            if(input.y != 0) input.x = 0;

            if(input != Vector2.zero){
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveZ", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.z += input.y;

                StartCoroutine(Move(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos){

        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
}
