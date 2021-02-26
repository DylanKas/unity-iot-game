using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Text;
using System.IO;
using UnityEngine.Networking;

using System.Threading.Tasks;	
using UnityEngine;


public class FoodHandler : MonoBehaviour
{
    public GameObject[] foods;
    public GameObject target;
    //private float zIndexItemLayer = 0.675f;
    private float apiCheckCountdown = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetFood");
    }


    // Update is called once per frame
    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
          if (apiCheckCountdown <= 0)
          {
              apiCheckCountdown = 3;
              StartCoroutine("GetFood");
          }
    }

    public void SpawnRandom()
     {
         //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5)); //will get the middle of the screen
 
         Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(0,Screen.width), UnityEngine.Random.Range(700,Screen.height), Camera.main.farClipPlane/2));
         GameObject newFood = Instantiate(foods[UnityEngine.Random.Range(0, foods.Length)],screenPosition,Quaternion.identity);
         newFood.SetActive(true);
     }
    

      IEnumerator GetFood()
      {
          using (UnityWebRequest req = UnityWebRequest.Get(String.Format("https://aqueous-oasis-80188.herokuapp.com/api/food")))
          {
              yield return req.SendWebRequest();
              while (!req.isDone)
                  yield return null;
              byte[] result = req.downloadHandler.data;
              string resultString = System.Text.Encoding.Default.GetString(result);
              int nbFood = Int32.Parse(resultString);
              for(int i=0; i<nbFood; i++){
                  SpawnRandom();
              }
              //Debug.Log(resultString);      
          }
      }

}
