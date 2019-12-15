using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 chartrans = GameObject.Find("Character").transform.position;
        this.transform.position = new Vector3(chartrans.x, chartrans.y, chartrans.z-1);
    }
}
