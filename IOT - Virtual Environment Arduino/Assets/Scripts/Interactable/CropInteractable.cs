using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropInteractable : MonoBehaviour, Interactable
{
    public Sprite fertilePlant, spoiledPlant;
    private float timer = 2;
    public GameObject[] fireSfx;

    private bool isFertile = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.5f;
            updateCrops();
        }
    }

    public void updateCrops(){
        float currentRain = PlayerPrefs.GetFloat("rain", 0f);
        float currentLight = PlayerPrefs.GetFloat("light", 0f);
        if(currentRain > 0.3 && currentLight > 0.5){
            if(!isFertile){
                if(gameObject.transform.localScale.x < 1f){
                    gameObject.transform.localScale += new Vector3(0.025f,0.025f,0);
                }else{
                    gameObject.GetComponent<SpriteRenderer>().sprite = fertilePlant;
                    gameObject.GetComponent<Food>().enabled = true;
                    gameObject.tag = "Interactable";
                    isFertile = true;
                }
            }
        }
    }

    public void interact(){
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Swordman>().actionInteract();

        gameObject.GetComponent<Food>().eatFood();
        gameObject.GetComponent<SpriteRenderer>().sprite = spoiledPlant;
        gameObject.GetComponent<Food>().enabled = false;
        gameObject.transform.localScale = new Vector3(0.3f,0.3f,0);
        gameObject.tag = "Crop";
        isFertile = false;
    }
    
}
