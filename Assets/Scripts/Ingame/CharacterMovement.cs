using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick joystick;
    private CharacterController _characterController;
    private Animator _animator;

    [Header("Values")] 
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotateSpeed;

    enum MovementState
    {
        Idle,
        Walk,
        Run
    }
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        _animator.Play("Walk");
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.TransformDirection(
            new Vector3(
                joystick.Horizontal, 
                0f, 
                Mathf.Abs(joystick.Vertical)
                )
            ).normalized;
        
        if (moveDirection.magnitude != 0)
        {
            float speed;
            if (Mathf.Abs(joystick.Vertical) >= .5f)
            {
                speed = runSpeed;
                SetAnimation(MovementState.Run);
            }
            else
            {
                speed = walkSpeed;
                SetAnimation(MovementState.Walk);
            }
            
            _characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); 
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }  
        else 
            SetAnimation(MovementState.Idle);
    }

    void SetAnimation(MovementState movementState)
    {
        _animator.SetInteger("speedLevel", (int)movementState);
    }
}
