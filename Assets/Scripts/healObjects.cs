using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healObjects : MonoBehaviour
{
    private GameManager gameManagerScript;
    public int HPValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // unable to get heal when full HP;
        if (gameManagerScript.HP <= 2)
        {
            
            if (collision.gameObject.CompareTag("Player"))
            {
                gameManagerScript.updateHP(HPValue);
                Destroy(gameObject);
                
            }   
        }
       
    }

}
