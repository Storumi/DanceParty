using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawnScript : MonoBehaviour
{
    int _offset;
     [SerializeField] GameObject myGem;
      Vector3 spawnDirection;
    // Start is called before the first frame update
    void Start()
    {
        
        
        //Debug.Log(transform.position.x);
       // Debug.Log(spawnDirection);
        RhythmManagerOne.rm1.gemSpawnList.Add(this);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnGem()
    {
        _offset=Random.Range(0, RhythmManagerOne.rm1.colorList.Count);
        GetComponent<SpriteRenderer>().color = RhythmManagerOne.rm1.colorList[_offset];
        if(transform.position.x>0){spawnDirection=new Vector3(-1,0,0);}
        if(transform.position.x<0){spawnDirection=new Vector3(1,0,0);}
        myGem.GetComponent<GemScript>().Direction=spawnDirection;
        myGem.GetComponent<GemScript>().color=GetComponent<SpriteRenderer>().color;
        Instantiate(myGem, transform.position, Quaternion.identity);
    }
}
