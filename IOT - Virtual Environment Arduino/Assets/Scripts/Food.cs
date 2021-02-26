using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isSpoiled = false;
    public Sprite spoiledSprite;
    public float FOOD_INCREASE = 25;
    public float HEALTH_DECREASE_SPOILED_MEAT = 10;
    public float secondsBeforeSpoiling = 20;
    
    //private float foodLifeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondsBeforeSpoiling -= Time.deltaTime;
        if (!isSpoiled && secondsBeforeSpoiling <= 0)
        {
            SpoilFood();
        }
    }

    void SpoilFood(){
        isSpoiled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = spoiledSprite;
    }

    public void eatFood(){
        float currentFood = PlayerPrefs.GetFloat("food", 100f);
        currentFood += FOOD_INCREASE;
        PlayerPrefs.SetFloat("food", currentFood);

        if(isSpoiled){
            float currentHealth = PlayerPrefs.GetFloat("health", 100f);
            currentHealth -= HEALTH_DECREASE_SPOILED_MEAT;
            PlayerPrefs.SetFloat("health", currentHealth);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().updateBars();
    }
}
