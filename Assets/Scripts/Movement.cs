using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int availableMoves;
    private InputManager inputManager;

    private Rigidbody2D rigidbody;

    private bool canMove = false;
    
    private LevelGen level;

    public Dictionary <Vector2, Sprite[]> sprites = new Dictionary <Vector2, Sprite[]>();

    public Vector2 pos = new Vector2(0, 4.5f);

    void Awake() {
        inputManager = new InputManager();

        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();
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
                    Move( new Vector2(0, 1) );
                } else {
                    Move( new Vector2(0, -1) );
                }
            } else {
                if(x > 0) {
                    Move( new Vector2(1, 0) );
                } else {
                    Move( new Vector2(-1, 0) );
                }
            }
        }

    }


    void Move(Vector2 dir) {
        if( (pos + dir).x < -9 ||  (pos + dir).x > 9 || (pos + dir).y < -4.5f || (pos + dir).y > 4.5f) {
            return;
        }

        //To get array coordinates do (x+9), (4.5-y)
        if( level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos + dir).x) + 9 ] < 2) {
            
            level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos + dir).x) + 9 ] = 5;

            pos += dir;
            availableMoves--;

            //level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos + dir).x) + 9 ] = Instantiate();

            transform.position = pos;
        }

        if( level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] == 2) {
            availableMoves--;
            level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] = 0;
            
            //GAME OBJECT: level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos+dir).x) + 9 ];
        }

    }
}