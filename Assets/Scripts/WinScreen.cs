using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    
    private LevelGen level;

    public LineRenderer trail;

    public Material deadTexture;

    public Vector2 pos = new Vector2(0, 4.5f);

    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();

        trail = gameObject.GetComponent<LineRenderer>();
    }

    void Start() {
        StartCoroutine(Move(new Vector2(0, -1)));
    }

    public IEnumerator Move(Vector2 dir) {
        if( (pos + dir).x < -9 ||  (pos + dir).x > 9 || (pos + dir).y < -4.5f || (pos + dir).y > 4.5f) {
            yield return new WaitForSeconds(0f);
        }

        //To get array coordinates do (x+9), (4.5-y)
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos + dir).x) + 9 ] == '.' ) {            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);  
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'o';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            transform.position = pos;

            yield return new WaitForSeconds(0f);
        }

        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '*') {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = '.';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos+dir).x) + 9 ].GetComponent<FragileRock>().Shatter();

            yield return new WaitForSeconds(0f);
        }

        if ( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '+' ) {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'o';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Vector2 newDir = new Vector2(1, 1) - new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            GameObject newVine = Instantiate(gameObject);
            
            newVine.GetComponent<LineRenderer>().positionCount = 1;
            newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );

            StartCoroutine(newVine.GetComponent<WinScreen>().Move(-newDir));
            if(newVine.GetComponent<WinScreen>().pos == pos) {
                Destroy(newVine.GetComponent<WinScreen>());
                newVine.GetComponent<LineRenderer>().positionCount = 2;
                newVine.GetComponent<LineRenderer>().material = deadTexture; 
                newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );
                newVine.GetComponent<LineRenderer>().SetPosition( 1, pos - (newDir * 0.5f) );
            }

            Vector2 oldPos = pos;
            StartCoroutine(Move(newDir));
            if(pos == oldPos) {
                Destroy(gameObject.GetComponent<WinScreen>());
                GameObject deadEnd = Instantiate(gameObject);
                Destroy(deadEnd.GetComponent<WinScreen>());
                deadEnd.GetComponent<LineRenderer>().positionCount = 2;
                deadEnd.GetComponent<LineRenderer>().SetPosition(0, pos);
                deadEnd.GetComponent<LineRenderer>().SetPosition( 1, pos + (newDir * 0.5f) );
                deadEnd.GetComponent<LineRenderer>().material = deadTexture;
            }

            yield return new WaitForSeconds(0.1f);
        }

        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == 'g') {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'o';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Destroy(gameObject.GetComponent<WinScreen>());
            GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles --;

            yield return new WaitForSeconds(0f);
        }

        
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == 's') {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'o';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );
            
            GameObject.Find("GameManager").GetComponent<GameManager>().movesLeft += 5;

            yield return new WaitForSeconds(0f);
        }

        yield return new WaitForSeconds(0f);
    }
}
