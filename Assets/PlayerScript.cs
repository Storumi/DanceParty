using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int Starting_Health;
    Transform tf;
    bool canMove = true;
    int _offset;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        RhythmManagerOne.rm1.playerScripts.Add(this);
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
        
        _offset=Random.Range(0, RhythmManagerOne.rm1.colorList.Count);
        GetComponent<SpriteRenderer>().color = RhythmManagerOne.rm1.colorList[_offset];
        

    }

    public void CheckMatch()
    {
        Debug.Log("Checking");
        foreach (var tile in RhythmManagerOne.rm1.floorTiles)
        {
            if (tile.transform.position == tf.position)
            {
                Debug.Log("SameSpot");
                if (tile.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
                {
                    Debug.Log("SameColor");
                    canMove = true;
                    CycleColor();
                }
                
            }
        }
    }
}
