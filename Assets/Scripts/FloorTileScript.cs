using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileScript : MonoBehaviour
{
    int _offset;
    // Start is called before the first frame update
    void Start()
    {
        RhythmManagerOne.rm1.floorTiles.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CycleColor()
    {
        if (Random.Range(0, 100) > 33)
        {
            _offset=Random.Range(0, RhythmManagerOne.rm1.colorList.Count);
            GetComponent<SpriteRenderer>().color = RhythmManagerOne.rm1.colorList[_offset];
        }
        
    }
    
}
