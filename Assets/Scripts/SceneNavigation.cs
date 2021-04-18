using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNavigation : MonoBehaviour
{

    public GameObject currentDoor;
    public string levelToLoad;
    /*  public void onNavigate()
      {

          SceneManager.LoadScene(0);
      }*/

    /*public void Start()
    {
        if (currentDoor.gameObject.tag == "Doors")
        {
            Debug.Log(1);
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
*/

    private void OnTriggerStay2D(Collider2D door)
    {
        //if (collision.gameObject.tag == "NPC")
        // {
        /*door = collision.GetComponent<door_Contoller>();

        if (Input.GetKey(KeyCode.E))
            door.ActivateDoor();*/
        // }
    }
}
