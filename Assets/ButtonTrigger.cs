using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public GameObject frame;
    public GameObject door;
    public GameObject[] otherFrames;

    public void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ; 
        {
            Debug.Log("start");
            anim.SetTrigger("isTriggered");
            frame.SetActive(true);
            foreach (GameObject frame in otherFrames)
            {
                frame.SetActive(false);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ;
        {
            anim.SetTrigger("isTriggered");
        }
    }
}
