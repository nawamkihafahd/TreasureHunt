using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string token;
    public int room;
    public int type;
    public int myScore;
    public int enemyScore;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
