using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileScript : MonoBehaviour
{
    int _offset;

    public Animation anim;

    public bool overriden;
    public GemScript gem;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animation>();
        RhythmManagerOne.rm1.floorTiles.Add(this);
        overriden = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (overriden)
        {
            GetComponent<SpriteRenderer>().color = gem.color;
        }
    }
    
    public void CycleColor()
    {
        if(overriden) return;
        if (Random.Range(0, 100) > -1)
        {
            _offset=Random.Range(0, RhythmManagerOne.rm1.colorList.Count);
            GetComponent<SpriteRenderer>().color = RhythmManagerOne.rm1.colorList[_offset];
        }
        
    }

    public void ScoreAnim()
    {
        anim.clip = anim.GetClip("TileScoreBump");
        anim.Rewind();
        anim.Play("TileScoreBump");
    }
    public void MoveAnim()
    {
        anim.clip = anim.GetClip("TileScoreBump");
        anim.Rewind();
        anim.Play("TileScoreBump");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Gem")
        {
            overriden = true;
            gem=other.gameObject.GetComponent<GemScript>();
        }
    }
    //add collission stay check
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Gem")
        {
            overriden = false;
            gem=null;
            CycleColor();
        }
    }
}
