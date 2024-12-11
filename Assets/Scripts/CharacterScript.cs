using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction;
    private PlayerInputActions playerInput;
    

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
       
        //Проекції
        Vector3 forward= Camera.main.transform.forward;
        forward.y=0.0f;
        if(forward == Vector3.zero){
            Debug.Log(forward);
            forward = Camera.main.transform.up;
            forward.y = 0.0f;
        }
        forward.Normalize();
        Vector3 right= Camera.main.transform.right;
        right.y=0.0f;
        right.Normalize();

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(100*Time.deltaTime * 
        (
            right * moveValue.x +
            forward * moveValue.y 
        ));
        
    }
}
