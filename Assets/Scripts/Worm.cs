using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{

    private LevelGen level;

    public Vector2 pos = new Vector2(0, 4.5f);

    public int dir = 1;
    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();
    }

    public void Move() {
        if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
            dir *= 1;

            if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
                return;
            }
        }

        if(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ][ (int)Mathf.Round(pos.x + dir) + 9 ] == '.' ) {
            pos += new Vector2(dir, 0);

            transform.position = pos;

            return;
        }

        
        if(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ][ (int)Mathf.Round(pos.x + dir) + 9 ] == 'p' ) {
            //Lose
            return;
        }

        dir *= -1;
        pos += new Vector2(dir, 0);
        transform.position = pos;
    }
}
