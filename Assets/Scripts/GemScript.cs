using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private Transform tf;
    public Vector3 Direction;
    public Color color;
    // Start is called before the first frame update

    void Awake()
    {
        RhythmManagerOne.rm1.gemScripts.Add(this);
        tf= GetComponent<Transform>(); 
    }
    void Start()
    {
       tf= GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
       // tf.position+=Direction*Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /* if (other.gameObject.tag == "Player")
          {
              Debug.Log("Hit");
              RhythmManagerOne.rm1.score++;
          }
          //play a sound here
          Destroy(this.gameObject);
          */
        
        
        
    }

    public void Move()
    {
        tf.position += Direction;
    }
}
