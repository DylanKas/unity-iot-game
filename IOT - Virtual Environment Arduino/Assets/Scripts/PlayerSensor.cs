using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public GameObject actionButton;
    //public UnityEngine.UI.Button button;
    public float EAT_DISTANCE;
    public float INTERACT_DISTANCE;

    private int frames = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % 10 == 0) { //If the remainder of the current frame divided by 10 is 0 run the function.
            senseInteractableObject();
        }
    }

    GameObject eatClosestFood ()
    {
        var foods = GameObject.FindGameObjectsWithTag("Food");
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in foods)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < EAT_DISTANCE)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        //Eat food
        if(bestTarget != null){
            Debug.Log("EAT");
            var foodScript = bestTarget.GetComponent("Food") as Food;
            foodScript.eatFood();
            Destroy(bestTarget);
        }
        return bestTarget;
    }

    GameObject interactNearestObject()
    {
        var foods = GameObject.FindGameObjectsWithTag("Interactable");
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in foods)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < INTERACT_DISTANCE)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        //Interact wiht object
        if(bestTarget != null){
            var interactableScript = bestTarget.GetComponent("Interactable") as Interactable;
            interactableScript.interact();
        }
        return bestTarget;
    }

    public void senseInteractableObject(){
        var foods = GameObject.FindGameObjectsWithTag("Food");
        var interactables = GameObject.FindGameObjectsWithTag("Interactable");
        var crops = GameObject.FindGameObjectsWithTag("Crop");
        var fishingSpots = GameObject.FindGameObjectsWithTag("Crop");
        GameObject[] elements = foods.Concat(interactables).ToArray();
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        bool enableButton = false;
        foreach(GameObject potentialTarget in interactables)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < INTERACT_DISTANCE)
            {
                enableButton = true;
            }
        }
        foreach(GameObject potentialTarget in foods)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < EAT_DISTANCE)
            {
                enableButton = true;
            }
        }
        foreach(GameObject potentialTarget in crops)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr && dSqrToTarget < EAT_DISTANCE)
            {
                enableButton = true;
            }
        }

        if(enableButton){
            ButtonEnabled(actionButton.GetComponent<UnityEngine.UI.Button>());
        }else{
            ButtonDisabled(actionButton.GetComponent<UnityEngine.UI.Button>());
        }
     
        //Interact wiht object
        if(bestTarget != null){
            var interactableScript = bestTarget.GetComponent("Interactable") as Interactable;
            interactableScript.interact();
        }
    }

    public void ButtonEnabled(Button button)
     {
         ColorBlock colors = button.colors;
         colors.normalColor = new Color32(255,255,255,255);
         button.colors = colors;
     }

         public void ButtonDisabled(Button button)
     {
         ColorBlock colors = button.colors;
         colors.normalColor = new Color32(255,255,255,125);
         button.colors = colors;
     }


    public void interactAction(){
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Swordman>().actionInteract();
    }
    public void onClick(){
        if(eatClosestFood()){
            interactAction();
        }else if(interactNearestObject()){
            interactAction();
        }
    }

}
