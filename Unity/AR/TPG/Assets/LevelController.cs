using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startpos;
    public int coincount;
    public int level;
    private int coinstart;
    private GameObject[] coins;
    void Start()
    {
        //startpos = new Vector3(-12,-1,-6);
        //coincount = 4;
        coinstart = coincount;
        coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetLevel()
    {
        GameObject.Find("Character").transform.position = startpos;
        coincount = coinstart;
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
    }
    public void nextLevel()
    {
        Debug.Log("NextLevel");
        if (level == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            Application.Quit();
        }
        
    }
    public void takeCoin()
    {
        coincount -= 1;
        if(coincount == 0)
        {
            nextLevel();
        }
    }
}
