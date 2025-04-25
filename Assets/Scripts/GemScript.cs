using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private Transform tf;
    public Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
       tf= GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        tf.position+=Direction*Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            RhythmManagerOne.rm1.score++;
        }
        //play a sound here
        Destroy(this.gameObject);
    }
}
