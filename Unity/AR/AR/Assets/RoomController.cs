using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    // Start is called before the first frame update
    string respjoin;
    string respdata;
    int isJoined;
    public string token;
    public int room;
    public int type;
    void Start()
    {
        isJoined = 0;
        StartCoroutine(joinRoom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator getRoomData()
    {
        if (isJoined == 1)
        {
            string url = "http://192.168.43.183/ar/public/api/roomData/" + room.ToString();
            WWW www = new WWW(url);
            yield return www;
            if (www.error == null)
            {
                respdata = www.text;
                var n = JSON.Parse(respdata);
                Debug.Log(n["data"]["roomstate"]);
                if(n["data"]["started"] == 1)
                {
                    SceneManager.LoadScene("Game");
                }
                if (n["data"]["roomstate"] == 1 && type == 1)
                {
                    string url2 = "http://192.168.43.183/ar/public/api/startRoom/" + room.ToString();
                    WWW www2 = new WWW(url2);
                    yield return www2;
                    if (www2.error == null)
                    {
                    }
                }
            }
            else
            {
                Debug.Log("ERROR: " + www.error);
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(getRoomData());
    }
    public IEnumerator joinRoom()
    {
        string url = "http://192.168.43.183/ar/public/api/joinRoom";
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            isJoined = 1;
            respjoin = www.text;
            var n = JSON.Parse(respjoin);
            Debug.Log(n);
            room = n["id"];
            if(n["type"] == "master")
            {
                type = 1;
            }
            else
            {
                type = 2;
            }
            token = n["token"];
            GameObject a =  GameObject.Find("GlobalPlayerData");
            a.gameObject.GetComponent<PlayerDataScript>().token = token;
            a.gameObject.GetComponent<PlayerDataScript>().type = type;
            a.gameObject.GetComponent<PlayerDataScript>().room = room;
            StartCoroutine(getRoomData());
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }

    }

}
