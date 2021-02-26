using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingInteractable : MonoBehaviour, Interactable
{
    public Sprite fishingRodSprite;
    public GameObject fish;
    public Sprite[] fishSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void interact(){
        Debug.Log("Fishing interact");
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Swordman>().actionInteract();
        
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        GameObject newFood = Instantiate(fish,new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.5f, gameObject.transform.position.z),Quaternion.identity);
        newFood.GetComponent<SpriteRenderer>().sprite = fishSprites[UnityEngine.Random.Range(0, fishSprites.Length)];
        newFood.SetActive(true);
        newFood.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150, 100), ForceMode2D.Force);
        
    }
}
