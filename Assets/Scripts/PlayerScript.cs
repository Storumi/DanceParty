using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int Starting_Health;
    [SerializeField] AK.Wwise.Event fxEvent;
    
    Transform tf;
    bool canMove = true;
    int _offset;
    private int animIndex;
    Animation anim;
    Animation anim2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        anim2 = GetComponentInChildren<Animation>();
        tf = GetComponent<Transform>();
        RhythmManagerOne.rm1.playerScripts.Add(this);
        animIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                tf.position += new Vector3(0, 1, 0);
                canMove = false;
                CheckMatch();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                tf.position += new Vector3(-1, 0, 0);
                canMove = false;
                CheckMatch();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                tf.position += new Vector3(0, -1, 0);
                canMove = false;
                CheckMatch();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                tf.position += new Vector3(1, 0, 0);
                canMove = false;
                CheckMatch();
            }
        }
    }

    public void GrantMove()
    {
        canMove = true;
    }
    public void CycleColor()
    {
         Color temp = GetComponent<SpriteRenderer>().color;
        _offset=Random.Range(0, RhythmManagerOne.rm1.colorList.Count);
        if (temp == RhythmManagerOne.rm1.colorList[_offset])
        {
            Debug.Log("Same Color");
            CycleColor();
            return;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = RhythmManagerOne.rm1.colorList[_offset];
        }

    }
    
    public void CheckMatch()
    {
        //Debug.Log("Checking");
        foreach (var tile in RhythmManagerOne.rm1.floorTiles)
        {
            Vector2 temp1=new Vector2(tile.transform.position.x, tile.transform.position.y);
            Vector2 temp2=new Vector2(tf.position.x, tf.position.y);
            if (temp1==temp2)
            {
                
                //Debug.Log("SameSpot");
                if (tile.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
                {
                    //Debug.Log("SameColor");
                    //Debug.Log("SameColor");
                    
                    RhythmManagerOne.rm1.score++;
                    
                    //play a sound here
                    canMove = true;
                    anim.Rewind();
                    anim.Play();
                    anim2.Play();
                    tile.ScoreAnim();
                    fxEvent.Post(gameObject);
                    CycleColor();
                }
                else
                {
                    tile.MoveAnim();
                }
                
            }
        }
    }
}
