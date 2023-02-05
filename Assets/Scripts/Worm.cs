using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{

    private LevelGen level;

    public Vector2 pos;

    public int dir = 1;
    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();
        pos = new Vector2(transform.position.x, transform.position.y);
        transform.position = pos;
    }

    public void Move() {
        if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
            dir *= 1;

            if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
                return;
            }
        }

        System.Text.StringBuilder strBuilder;

        if(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ][ (int)Mathf.Round(pos.x + dir) + 9 ] == '.' ) {
            
            strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
            strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '.';
            level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();

            pos += new Vector2(dir, 0);

            strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
            strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '=';
            level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();

            transform.position = pos;

            return;
        }

        
        if(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ][ (int)Mathf.Round(pos.x + dir) + 9 ] == 'p' ) {
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLoseLevelMenu(); 
            return;
        }


        strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
        strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '.';
        level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();
        
        dir *= -1;
        pos += new Vector2(dir, 0);
        transform.position = pos;

        strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
        strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '=';
        level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();
    }
}
