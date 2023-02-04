using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int availableMoves;
    private InputManager inputManager;

    private Rigidbody2D rigidbody;

    private bool canMove = false;

    void Awake() {
        inputManager = new InputManager();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }


    protected void OnEnable() {
        inputManager.Plant.Enable();
    }

    protected void OnDisable() {
        inputManager.Plant.Disable();
    }

    void FixedUpdate() {
        float x = inputManager.Plant.Movement.ReadValue<Vector2>().x;
        float y = inputManager.Plant.Movement.ReadValue<Vector2>().y;

        if(Mathf.Abs(x) < 0.5f && Mathf.Abs(y) < 0.5f) {
            canMove = true;
        } else if (canMove && Mathf.Abs(x - y) > 0.2f) {
            canMove = false;
            
            if(Mathf.Abs(y) > Mathf.Abs(x)) {
                if(y > 0) {
                    Debug.Log(y);
                    rigidbody.MovePosition( rigidbody.position + new Vector2(0, 1) );
                } else {
                    Debug.Log(y);
                    rigidbody.MovePosition( rigidbody.position + new Vector2(0, -1) );
                }
            } else {
                if(x > 0) {
                    Debug.Log(x);
                    rigidbody.MovePosition( rigidbody.position + new Vector2(1, 0) );
                } else {
                    Debug.Log(x);
                    rigidbody.MovePosition( rigidbody.position + new Vector2(-1, 0) );
                }
            }
        }

    }


}
