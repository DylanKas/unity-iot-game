using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireInteractable : MonoBehaviour, Interactable
{
    public GameObject[] fireSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void interact(){
        foreach (GameObject element in fireSfx){
            element.SetActive(!element.activeInHierarchy);
        }
        
    }
}
