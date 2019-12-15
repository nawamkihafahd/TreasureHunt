using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ScoreText;
    public Text ResultText;
    void Start()
    {
        if (GameObject.Find("GlobalPlayerData").GetComponent<PlayerDataScript>().myScore > GameObject.Find("GlobalPlayerData").GetComponent<PlayerDataScript>().enemyScore)
        {
            ResultText.text = "You Win";
        }
        else if (GameObject.Find("GlobalPlayerData").GetComponent<PlayerDataScript>().myScore < GameObject.Find("GlobalPlayerData").GetComponent<PlayerDataScript>().enemyScore)

        {
            ResultText.text = "You Lose";
        }
        else
        {
            ResultText.text = "Its A Tie";
        }
        ScoreText.text = "Score: " + GameObject.Find("GlobalPlayerData").GetComponent<PlayerDataScript>().myScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
