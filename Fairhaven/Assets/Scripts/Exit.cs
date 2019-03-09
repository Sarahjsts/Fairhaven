using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // loads level 2 when player gets to exit tile
        if(collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Level2");
        }
        
    }
}
