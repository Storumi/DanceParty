using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRootScript : MonoBehaviour
{
    public Vector3 subPosition;

    [SerializeField] private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf=transform.GetChild(0).GetComponent<Transform>();
        tf.position=subPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
