using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class TreasureScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool openable;
    public int value;
    Animator myanimator;
    public Text openText;
    public GameObject Globvar;
    public int room;
    public int type;
    public int num;
    public string token;
    public Text StatusText;
    public GameObject scores;
    void Start()
    {
        Globvar = GameObject.Find("GlobalPlayerData");
        scores = GameObject.Find("ScoreController");
        token = Globvar.GetComponent<PlayerDataScript>().token;
        room = Globvar.GetComponent<PlayerDataScript>().room;
        type = Globvar.GetComponent<PlayerDataScript>().type;
        openable = true;
        myanimator = gameObject.GetComponent<Animator>();
        setcontent();
        if(this.gameObject.name == "chest1")
        {
            num = 1;
        }
        if (this.gameObject.name == "chest2")
        {
            num = 2;

        }
        if (this.gameObject.name == "chest3")
        {
            num = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void open()
    {
        if(openable)
        {
            openable = false;
            StartCoroutine(runanim());
        }
        
    }
    public IEnumerator runanim()
    {
        /*
         *Update DB 
         */
        string url = "http://192.168.43.183/ar/public/api/obtainChest/" + room + "/" + num + "/" + type + "/" + value;
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Claimed");
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            
        }
        else
        {
            Debug.Log("ERROR: " + www.error);

        }
        int atk = Random.Range(1, 10);
        int divisor = 3;
        if(scores.GetComponent<scorescript>().myScore < scores.GetComponent<scorescript>().hisScore)
        {
            divisor = 7;
        }
        if(atk<=divisor)
        {
            string url2 = "http://192.168.43.183/ar/public/api/globalAttack/" + room + "/"  + type ;
            WWW www2 = new WWW(url2);
            yield return www2;
            if (www2.error == null)
            {
                string respjoin = www.text;
                var n = JSON.Parse(respjoin);

            }
            else
            {
                Debug.Log("ERROR: " + www.error);

            }
        }
        StatusText.text = "You Attacked An Enemy!";
        openText.text = "You Get " + value + " Points!";
        myanimator.SetInteger("opened", 1);
        yield return new WaitForSeconds(1);
        myanimator.SetInteger("opened", 0);
        yield return new WaitForSeconds(1);
        openText.text = "";
        StatusText.text = "";
        gameObject.SetActive(false);
        
        

    }
    public void setcontent()
    {
        value = Random.Range(1, 10);
        value = value * 10;
        openable = true;
    }
}
