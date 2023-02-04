using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int availableMoves;
    private InputManager inputManager;

    private bool canMove = false;

    void Awake() {
        inputManager = new InputManager();
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

        if(Mathf.Abs(x) < 0.1f && Mathf.Abs(y) < 0.1f) {
            canMove = true;
        } else if (canMove && Mathf.Abs(x - y) > 0.2f) {
            canMove = false;
            
            if(Mathf.Abs(y) > Mathf.Abs(x)) {
                if(y > 0) {
                    Debug.Log("UP");
                } else {
                    Debug.Log("DOWN");
                }
            } else {
                if(x > 0) {
                    Debug.Log("RIGHT");
                } else {
                    Debug.Log("LEFT");
                }
            }
        }

    }


}
