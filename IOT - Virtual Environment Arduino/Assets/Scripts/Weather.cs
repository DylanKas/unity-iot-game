using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;

using System.Threading.Tasks;	
using UnityEngine;

public class Weather : MonoBehaviour
{
    private float apiCheckCountdown = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetWeather");
        //InvokeRepeating("GetWeather", 2.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
          if (apiCheckCountdown <= 0)
          {
              apiCheckCountdown = 5;
              StartCoroutine("GetWeather");
          }
    }
    

      IEnumerator GetWeather()
      {
          using (UnityWebRequest req = UnityWebRequest.Get(String.Format("https://aqueous-oasis-80188.herokuapp.com/api/data")))
          {
              yield return req.SendWebRequest();
              while (!req.isDone)
                  yield return null;
              byte[] result = req.downloadHandler.data;
              string weatherJSON = System.Text.Encoding.Default.GetString(result);
        
              Debug.Log(weatherJSON);
          }
      }
}
