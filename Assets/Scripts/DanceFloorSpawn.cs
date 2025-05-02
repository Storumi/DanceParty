using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorSpawn : MonoBehaviour
{
    [SerializeField] private GameObject DanceTile;
    private AnimationRootScript ARS;
    private float tileSize = 1;

    private float xstart = -9.5f;

    private float ystart = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ARS=DanceTile.GetComponent<AnimationRootScript>();
                ARS.subPosition=new Vector3(xstart + i*tileSize, ystart - j*tileSize, 0);
                Instantiate(DanceTile, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
