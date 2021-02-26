using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public GameObject playerSensor;
    public GameObject healthBar;
    public GameObject temperatureBar;
    public GameObject foodBar;

    
    private float health;
    private float temperature;
    private float food;
    private float TEMPERATURE_THRESHOLD = 0.5f;
    public float FOOD_DECREASE;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        //health = PlayerPrefs.GetFloat("health", 100f);
        //temperature = PlayerPrefs.GetFloat("bodyTemperature", 100f);
        //food = PlayerPrefs.GetFloat("food", 100f);

        health = 100f;
        temperature = 100f;
        food = 100f;

        
        PlayerPrefs.SetFloat("health", health);
        PlayerPrefs.SetFloat("bodyTemperature", temperature);
        PlayerPrefs.SetFloat("food", food);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1f;
            updateFood();
            updateHealth();
            updateBodyTemperature();
        }
    }

    public void updateBars(){
        foodBar.GetComponent<Healthbar>().UpdateHealth();
        temperatureBar.GetComponent<Healthbar>().UpdateHealth();
        healthBar.GetComponent<Healthbar>().UpdateHealth();
    }

    void updateFood()
    {
        float currentFood = PlayerPrefs.GetFloat("food", 100f);
        if(currentFood > 0){
            currentFood -= FOOD_DECREASE;
            PlayerPrefs.SetFloat("food", currentFood);
        }
        foodBar.GetComponent<Healthbar>().UpdateHealth();
        
        
    }

    void updateHealth()
    {
        float currentFood = PlayerPrefs.GetFloat("food", 100f);
        float currentBodyTemperature = PlayerPrefs.GetFloat("bodyTemperature", 100f);
        if(currentFood <= 5.0){
            float currentHealth = PlayerPrefs.GetFloat("health", 100f);
            if(currentHealth > 0){
                currentHealth -= 5;
            PlayerPrefs.SetFloat("health", currentHealth);
            healthBar.GetComponent<Healthbar>().UpdateHealth();
            }
            
        }
        if(currentFood > 10 && currentBodyTemperature > 10){
            float currentHealth = PlayerPrefs.GetFloat("health", 100f);
            if(currentHealth < 100){
                currentHealth += 10;
                PlayerPrefs.SetFloat("health", currentHealth);
                healthBar.GetComponent<Healthbar>().UpdateHealth();
            }
            
        }
    }

    
    void updateBodyTemperature()
    {
        float currentBodyTemperature = PlayerPrefs.GetFloat("bodyTemperature", 100f);
        float currentTemperature = PlayerPrefs.GetFloat("temperature", 1f);
        if(currentBodyTemperature > 0 && currentTemperature < TEMPERATURE_THRESHOLD && !isNearFire()){
            currentBodyTemperature -= (1-currentTemperature)*3f;
            PlayerPrefs.SetFloat("bodyTemperature", currentBodyTemperature);
            temperatureBar.GetComponent<Healthbar>().UpdateHealth();
        }
        if(currentBodyTemperature < 100 &&  currentTemperature > TEMPERATURE_THRESHOLD || isNearFire()){
            currentBodyTemperature += 5;
            PlayerPrefs.SetFloat("bodyTemperature", currentBodyTemperature);
            temperatureBar.GetComponent<Healthbar>().UpdateHealth();
        }
        if(currentBodyTemperature <= 5){
            float currentHealth = PlayerPrefs.GetFloat("health", 100f);
            currentHealth -= 5;
            PlayerPrefs.SetFloat("health", currentHealth);
            temperatureBar.GetComponent<Healthbar>().UpdateHealth();
        }
    }

    bool isNearFire(){
        if(getNearestFire()){
            return true;
        }
        return false;
    }

        GameObject getNearestFire()
    {
        var campfires = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "PF FX Campfire Fire");
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in campfires)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            GameObject fireComponent =  getChildGameObject(potentialTarget, "Fire");
            bool isFireEnabled = fireComponent !=null ? fireComponent.activeInHierarchy : false;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < 5 && isFireEnabled)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        //Interact wiht object
        if(bestTarget != null){
            
        }
        return bestTarget;
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
         Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
         foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
         return null;
     }
}
