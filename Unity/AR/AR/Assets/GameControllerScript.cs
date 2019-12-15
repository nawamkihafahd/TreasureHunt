using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int started;
    public GameObject Globvar;
    public int room;
    public int type;
    public string token;
    void Start()
    {
        Debug.Log("Starting...");
        started = 0;
        Globvar = GameObject.Find("GlobalPlayerData");
        token = Globvar.GetComponent<PlayerDataScript>().token;
        room = Globvar.GetComponent<PlayerDataScript>().room;
        type = Globvar.GetComponent<PlayerDataScript>().type;
        StartCoroutine(connectGame());
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(started == 0)
        {

        }
        elseif(Input.touchCount>0)
        {
            var touch = Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                {
                    if(Physics.Raycast(ray, out hit, 200))
                    {
                        if(hit.transform.tag=="Chest")
                        {
                            GameObject a = GameObject.Find("treasure_box");
                            a.GetComponent<TreasureScript>().open();
                            Debug.Log("Nice");
                        }
                    }
                }
            }
        }
        */
        if (started == 0)
        {

        }
        else if (Input.GetMouseButtonDown(0))
        {
            //var touch = Input.GetTouch(0);
            //if (touch.phase == TouchPhase.Began)
            //{
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                {
                    if (Physics.Raycast(ray, out hit, 200))
                    {
                        if (hit.transform.tag == "Chest")
                        {
                            GameObject a = GameObject.Find(hit.transform.gameObject.name);
                            a.GetComponent<TreasureScript>().open();
                            Debug.Log("Nice");
                        }
                    }
                }
            //}
        }
        
    }
    public IEnumerator Gamedur()
    {
        yield return new WaitForSeconds(30);
        string url = "http://192.168.43.183/ar/public/api/endGame/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {

        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
    public IEnumerator connectGame()
    {
        Debug.Log("Connecting...");
        string url = "http://192.168.43.183/ar/public/api/connectGame/" + room.ToString() + "/" + token + "/" + type.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            Debug.Log(n);
            if (n["hasil"] == "connected")
            {
                StartCoroutine(waitplayer());
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }

    public IEnumerator waitplayer()
    {
        string url = "http://192.168.43.183/ar/public/api/gameData/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            if (n["data"]["token1state"] == 1 && n["data"]["token2state"] == 1)
            {

                string url2 = "http://192.168.43.183/ar/public/api/startGame/" + room.ToString();
                WWW www2 = new WWW(url2);
                yield return www2;
                if (www.error == null)
                {

                    Debug.Log("Started");
                }
                else
                {
                    Debug.Log("ERROR: " + www.error);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(waitplayer());
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(waitplayer());
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            yield return new WaitForSeconds(1);
            StartCoroutine(waitplayer());
        }
    }

    public IEnumerator StartGame()
    {
        string url = "http://192.168.43.183/ar/public/api/gameData/" + room.ToString();
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string respjoin = www.text;
            var n = JSON.Parse(respjoin);
            if (started == 0 && n["data"]["gamestate"] == 1)
            {
                started = 1;
                StartCoroutine(Gamedur());
            }
            else if(n["data"]["gamestate"] == 2)
            {
                SceneManager.LoadScene("ConclusionScene");
            }
            
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(StartGame());
    }
}
