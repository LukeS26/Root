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
    
    private AudioSource plantAudio;
    public AudioClip hitRockSFX;
    public AudioClip fertilizerSFX;
    public AudioClip soilSFX;
    public AudioClip eatenSFX;
    public AudioClip reachWaterSFX;
    public AudioClip splittingSFX;


    void Awake() {
        level = GameObject.Find("Level Generator").GetComponent<LevelGen>();

        trail = gameObject.GetComponent<LineRenderer>();

        plantAudio = GetComponent<AudioSource>();
    }

    public bool Move(Vector2 dir) {
        if( (pos + dir).x < -9 ||  (pos + dir).x > 9 || (pos + dir).y < -4.5f || (pos + dir).y > 4.5f) {
            return false;
        }

        // Checks if the Plant collided with a Worm
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '=') {
            plantAudio.PlayOneShot(eatenSFX, 1.0f); // plays sound effect of worm eating plant at full volume
           
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLoseLevelMenu(true);

            return false;
        }

        //To get array coordinates do (x+9), (4.5-y)
        // Checks if the Plant collided with Empty Space
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos + dir).x) + 9 ] == '.' ) {            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);  
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            GameObject particle = Instantiate( dirtParticle );
            particle.transform.position = pos;

            transform.position = pos;

            plantAudio.PlayOneShot(soilSFX, 0.025f); // plays sound effect of plant moving through soil at a quarter volume

            return true;
        }

        // Checks if the Plant collided with a Fragile Rock
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == '*') {            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = '.';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();

            GameObject particle = Instantiate( stoneParticle );
            particle.transform.position = pos + dir;
            
            level.levelSave[ (int)Mathf.Round(4.5f - (pos+dir).y), (int)Mathf.Round((pos+dir).x) + 9 ].GetComponent<FragileRock>().Shatter();

            return true;
        }

        // Checks if the Plant collided with a Splitter
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

            plantAudio.PlayOneShot(splittingSFX, 0.5f); // plays sound effect of plant splitting at half volume
            
            return true;
        }

        // Checks if the Plant collided with Water
        if( level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ][ (int)Mathf.Round((pos+dir).x) + 9 ] == 'g') {
            
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]);
            strBuilder[ (int)Mathf.Round((pos+dir).x) + 9 ] = 'p';
            level.levelCode[ (int)Mathf.Round(4.5f - (pos+dir).y) ]=strBuilder.ToString();
            
            pos += dir;
            trail.SetPosition(trail.positionCount++, pos );

            Destroy(gameObject.GetComponent<Movement>());
            GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles --;

            plantAudio.PlayOneShot(reachWaterSFX, 0.5f); // plays sound effect of plant reaching water at half volume
            
            return true;
        }

        
        // Checks if the Plant collided with Fertilizer
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

            plantAudio.PlayOneShot(fertilizerSFX, 1.0f); // plays sound effect of plant recieving more moves at full volume

            return true;
        }

        // Assumes obstacle was hit
        plantAudio.PlayOneShot(hitRockSFX, 0.5f); // plays sound effect of plant colliding with obstacle at half volume
        return false;
    }
}
