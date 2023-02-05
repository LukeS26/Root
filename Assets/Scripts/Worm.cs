using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{

    private LevelGen level;

    public Vector2 pos;

    SpriteRenderer spriteRenderer;

    public Sprite[] sprites = new Sprite[2];
    
    int curSprite = 0;

    public int dir = 1;

    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();
        pos = new Vector2(transform.position.x, transform.position.y);
        transform.position = pos;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        spriteRenderer.flipX = dir < 0;
    }

    public IEnumerator RunMove() {
        yield return new WaitForSeconds(0.15f);

        Move();
    }

    public void Move() {
        if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
            dir *= -1;
            if( (pos.x + dir) < -9 || (pos.x + dir) > 9) {
                return;
            }
        }

        curSprite ++;
        curSprite %= 2;
        spriteRenderer.sprite = sprites[curSprite];

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
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLoseLevelMenu(true); 
            return;
        }


        strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
        strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '.';
        level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();
        
        dir *= -1;

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
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLoseLevelMenu(true); 
            return;
        }

        pos += new Vector2(dir, 0);
        transform.position = pos;

        strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ]);
        strBuilder[ (int)Mathf.Round(pos.x) + 9 ] = '=';
        level.levelCode[ (int)Mathf.Round(4.5f - pos.y) ] = strBuilder.ToString();
    }
}
