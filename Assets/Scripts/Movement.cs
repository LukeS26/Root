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

    public LineRenderer trail;

    public Material deadTexture;

    public Vector2 pos = new Vector2(0, 4.5f);

    void Awake() {
        inputManager = new InputManager();

        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();

        trail = gameObject.GetComponent<LineRenderer>();
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
            
            level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos + dir).x) + 9 ] = 4;

            pos += dir;
            availableMoves--;

            trail.SetPosition(trail.positionCount++, pos );

            transform.position = pos;

            return;
        }

        if( level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] == 2) {
            availableMoves--;
            level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] = 0;
            
            level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos+dir).x) + 9 ].GetComponent<FragileRock>().Shatter();

            return;
        }

        if ( level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] == 3 ) {
            
            level.level[ (int)Mathf.Round(4.5f - (pos+dir).y) ].Array[ (int)Mathf.Round((pos+dir).x) + 9 ] = 4;

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Vector2 newDir = new Vector2(1, 1) - new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            GameObject newVine = Instantiate(gameObject);
            
            newVine.GetComponent<LineRenderer>().positionCount = 1;
            newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );

            newVine.GetComponent<Movement>().Move(-newDir);
            if(newVine.GetComponent<Movement>().pos == pos) {
                Destroy(newVine.GetComponent<Movement>());
                newVine.GetComponent<LineRenderer>().positionCount = 2;
                newVine.GetComponent<LineRenderer>().material = deadTexture; 
                newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );
                newVine.GetComponent<LineRenderer>().SetPosition( 1, pos + new Vector2(-newDir.x * 0.5f, newDir.y * 0.5f) );
            }

            Vector2 oldPos = pos;
            Move(newDir);
            if(pos == oldPos) {
                Destroy(gameObject.GetComponent<Movement>());
                GameObject deadEnd = Instantiate(gameObject);
                deadEnd.GetComponent<LineRenderer>().positionCount = 2;
                deadEnd.GetComponent<LineRenderer>().SetPosition(0, pos);
                deadEnd.GetComponent<LineRenderer>().SetPosition( 1, pos + new Vector2(-newDir.x * 0.5f, newDir.y * 0.5f) );
                deadEnd.GetComponent<LineRenderer>().material = deadTexture;
            }

            return;
        }

    }
}
