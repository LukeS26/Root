using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    private LevelGen level;

    public LineRenderer trail;

    public Material deadTexture;

    public Vector2 pos = new Vector2(0, 4.5f);

    public GameObject dirtParticle;
    public GameObject stoneParticle;
    public GameObject fertilizerParticle;
    public GameObject extraMovesParticle;


    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();

        trail = gameObject.GetComponent<LineRenderer>();
    }

    public bool Move(Vector2 dir) {
        if( (pos + dir).x < -9 ||  (pos + dir).x > 9 || (pos + dir).y < -4.5f || (pos + dir).y > 4.5f) {
            return false;
        }

        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '=') {
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLoseLevelMenu(true); 
           
            return false;
        }

        //To get array coordinates do (x+9), (4.5-y)
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos + dir).x) + 9 ] == '.' ) {            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);  
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            GameObject particle = Instantiate( dirtParticle );
            particle.transform.position = pos;

            transform.position = pos;

            return true;
        }

        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '*') {            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = '.';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            GameObject particle = Instantiate( stoneParticle );
            particle.transform.position = pos + dir;
            
            level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos+dir).x) + 9 ].GetComponent<FragileRock>().Shatter();

            return true;
        }

        if ( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '+' ) {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Vector2 newDir = new Vector2(1, 1) - new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            GameObject newVine = Instantiate(gameObject);
            
            newVine.GetComponent<LineRenderer>().positionCount = 1;
            newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );

            newVine.GetComponent<Movement>().Move(-newDir);
            if(newVine.GetComponent<Movement>().pos == pos) {
                Destroy(newVine.GetComponent<Movement>());
                newVine.GetComponent<VineAnimation>().mats[0] = deadTexture;
                newVine.GetComponent<VineAnimation>().mats[1] = deadTexture;
                Destroy(newVine.GetComponent<VineAnimation>());
                newVine.GetComponent<LineRenderer>().positionCount = 2;
                newVine.GetComponent<LineRenderer>().material = deadTexture;
                newVine.GetComponent<LineRenderer>().SetPosition( 0, pos );
                newVine.GetComponent<LineRenderer>().SetPosition( 1, pos - (newDir * 0.5f) );
            }

            Vector2 oldPos = pos;
            Move(newDir);
            if(pos == oldPos) {
                Destroy(gameObject.GetComponent<Movement>());
                GameObject deadEnd = Instantiate(gameObject);
                Destroy(deadEnd.GetComponent<Movement>());
                deadEnd.GetComponent<VineAnimation>().mats[0] = deadTexture;
                deadEnd.GetComponent<VineAnimation>().mats[1] = deadTexture;

                Destroy(deadEnd.GetComponent<VineAnimation>());
                deadEnd.GetComponent<LineRenderer>().positionCount = 2;
                deadEnd.GetComponent<LineRenderer>().SetPosition(0, pos);
                deadEnd.GetComponent<LineRenderer>().SetPosition( 1, pos + (newDir * 0.5f) );
                deadEnd.GetComponent<LineRenderer>().material = deadTexture;
            }

            return true;
        }

        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == 'g') {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Destroy(gameObject.GetComponent<Movement>());
            GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles --;

            return true;
        }

        
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == 's') {
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            GameObject particle = Instantiate( fertilizerParticle );
            particle.transform.position = pos;

            Instantiate(extraMovesParticle);

            GameObject.Find("GameManager").GetComponent<GameManager>().movesLeft += 5;

            return true;
        }

        return false;
    }
}
