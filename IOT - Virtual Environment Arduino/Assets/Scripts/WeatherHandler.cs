using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Text;
using System.IO;
using UnityEngine.Networking;

using System.Threading.Tasks;	
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
    private float apiCheckCountdown = 5;
    // Start is called before the first frame update
    void Start()
    {
        /*
        Component[] c = gameObject.GetComponents<MonoBehaviour>();

        Debug.Log(c);
        foreach (Component component in c){
            Debug.Log(component);
            if(component.name == "AkilliMum.Standard.D2WeatherEffects.D2RainsPE"){
                component.SendMessage = 25;
            }
        }
        */
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
              Weather weather = CreateFromJSON(weatherJSON);

              Debug.Log(weatherJSON);
              
              if(weather != null){
                PlayerPrefs.SetFloat("rain", weather.rain);
                PlayerPrefs.SetFloat("fog", weather.fog);
                PlayerPrefs.SetFloat("snow", weather.snow);
                PlayerPrefs.SetFloat("temperature", weather.temperature);
                PlayerPrefs.SetFloat("light", weather.light);
                updateLight(weather.light);
              }
              
          }
      }

    public void updateLight(float light){
        var background = GameObject.FindGameObjectWithTag("Background");
        if(light > 0.85){
            light = 1f;
        }
        byte lightValue = (byte) (100 + light*155);  
        foreach (Transform child in background.transform){
            child.gameObject.GetComponent<SpriteRenderer>().color = new Color32(lightValue,lightValue,lightValue,255);
        } 
    }

    public void decreaseLight(){
        float light = PlayerPrefs.GetFloat("light", 1f);
        light-=0.1f;
        PlayerPrefs.SetFloat("light", light);
    }

      public static Weather CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Weather>(jsonString);
    }
}
