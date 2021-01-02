using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


/**
 Elegant? No
 Works? Yes

This needs to access the current and max health of the player (which I have assumed to be 12 because 3 hearts * 4 pieces of heart)
**/



public class PlayerHealthBar : MonoBehaviour
{
    //cached references

    //hearts in info bar
    [SerializeField] GameObject[] hearts;

    //heart sprite options
    [SerializeField] Sprite[] heartSprites;

    //need a reference to player's health
    PlayerHealth playerHealth;
    [SerializeField] private int playerCurrentHealth;

    //int holding max player health
    int maxPlayerHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        //get player's max health (set to 12 until I can access player health)
        maxPlayerHealth = playerHealth.GetMaxHealth();
        playerCurrentHealth = playerHealth.GetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameSession.GetScore().ToString());
        if (playerHealth != null)
        {

            //healthText.SetText(FindObjectOfType<Player>().GetHealth().ToString());
            UpdateHearts();
        }
        else
        {
            FinalUpdate();
        }


    }

    public void FinalUpdate()
    {
        //healthText.SetText("0");
        for (int i = 0; i < hearts.Length; i++)
        {
            //Debug.Log("i " + i.ToString());
            deactivateHeart(hearts[i]);
        }
    }

    public void UpdateHearts()
    {
        //Debug.Log("number of hearts " + hearts.Length.ToString());
        //Debug.Log("health " + playerCurrentHealth.ToString());
        for (int i = (hearts.Length - (maxPlayerHealth - playerHealth.GetPlayerHealth())); i < maxPlayerHealth; i++)
        {
            //Debug.Log("trying to deactivate " + hearts[i]);
            deactivateHeart(hearts[i]);
        }
        for (int i = 0; i < (hearts.Length - (maxPlayerHealth - playerHealth.GetPlayerHealth())); i++)
        {
            activateHeart(hearts[i]);
        }
    }

    public void deactivateHeart(GameObject heart)
    {
        heart.SetActive(false);
    }

    public void activateHeart(GameObject heart)
    {
        heart.SetActive(true);
    }

}
