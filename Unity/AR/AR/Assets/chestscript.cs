using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class chestscript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chest1;
    public GameObject chest2;
    public GameObject chest3;
    public int room;
    public int type;
    public string token;
    public GameObject Globvar;
    public int tostart = 1;
    void Start()
    {
        Globvar = GameObject.Find("GlobalPlayerData");
        token = Globvar.GetComponent<PlayerDataScript>().token;
        room = Globvar.GetComponent<PlayerDataScript>().room;
        type = Globvar.GetComponent<PlayerDataScript>().type;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //getcheststate
        if(tostart == 1)
        {
            if(GameObject.Find("GameController").GetComponent<GameControllerScript>().started == 1)
            {
                tostart = 0;
                StartCoroutine(monitorchest());
                if (type == 1)
                {
                    StartCoroutine(refreshChest());
                }
            }
        }
    }
    public IEnumerator monitorchest()
    {
        string url = "http://192.168.43.183/ar/public/api/gameData/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            Debug.Log(n["data"]["chest1state"]);
            if (n["data"]["chest1state"] == 1)
            {
                
                chest1.gameObject.SetActive(true);
                chest1.gameObject.GetComponent<TreasureScript>().setcontent();
            }
            if(n["data"]["chest2state"] == 1)
            {
                chest2.gameObject.SetActive(true);
                chest1.gameObject.GetComponent<TreasureScript>().setcontent();
            }
            if(n["data"]["chest3state"] == 1)
            {
                chest3.gameObject.SetActive(true);
                chest1.gameObject.GetComponent<TreasureScript>().setcontent();
            }

        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(monitorchest());
    }
    public IEnumerator refreshChest()
    {
        yield return new WaitForSeconds(10);
        string url = "http://192.168.43.183/ar/public/api/refreshChest/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {

        }
        else
        {
            Debug.Log("ERROR: " + www.error);

        }
        StartCoroutine(refreshChest());
    }
}
