using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class scorescript : MonoBehaviour
{
    // Start is called before the first frame update
    public int myScore;
    public int hisScore;
    public GameObject Globvar;
    public int room;
    public int type;
    public string token;
    public Text ScoreText;
    public Text StatusText;
    public int tostart;
    public GameObject hiteff;
    void Start()
    {
        tostart = 1;
        Globvar = GameObject.Find("GlobalPlayerData");
        token = Globvar.GetComponent<PlayerDataScript>().token;
        room = Globvar.GetComponent<PlayerDataScript>().room;
        type = Globvar.GetComponent<PlayerDataScript>().type;
        myScore = 0;
        hisScore = 0;
        hiteff = GameObject.Find("HitEffect");
        hiteff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tostart == 1)
        {
            if(GameObject.Find("GameController").GetComponent<GameControllerScript>().started == 1)
            {
                tostart = 0;
                StartCoroutine(checkScore());
            }
        }
        //UpdateScorefromDB
    }
    public IEnumerator checkScore()
    {
        string url = "http://192.168.43.183/ar/public/api/gameData/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            if(type == 1)
            {
                hisScore = n["data"]["token2score"];
                if(myScore > n["data"]["token1score"])
                {
                    StartCoroutine(attacked());
                }
                myScore = n["data"]["token1score"];
            }
            else
            {
                hisScore = n["data"]["token1score"];
                if (myScore > n["data"]["token2score"])
                {
                    StartCoroutine(attacked());
                }
                myScore = n["data"]["token2score"];
            }
            Globvar.GetComponent<PlayerDataScript>().myScore = myScore;
            Globvar.GetComponent<PlayerDataScript>().enemyScore = hisScore;
        }
        else
        {
            Debug.Log("ERROR: " + www.error);

        }
        StartCoroutine(checkScore());
        ScoreText.text = "" +myScore;
    }
    public IEnumerator attacked()
    {
        hiteff.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hiteff.SetActive(false);
        StatusText.text = "You Are Attacked!";
        yield return new WaitForSeconds(2);
        StatusText.text = "";
    }
}
