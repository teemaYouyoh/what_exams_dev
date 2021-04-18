using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private NPC_Contoller npc;
    

    public Animator animator;

    private Vector2 movement;

    float verticalInput;
    float horizontalInput;
    public VectorValue pos;

    public GameObject Timer;

    // [SerializeField] public GameObject quizScript;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        // if (getPlayerPosition.position != new Vector3(0.0f, 0.0f, 0.0f))
        // {
        //     transform.position = getPlayerPosition.position;
        // }
        // else
        // {
        //     transform.position = pos.initialValue;
        // }
        if (LevelChanger.willChange)
        {
            transform.position = LevelChanger.position;
            LevelChanger.willChange = false;
        } else 
        {
            transform.position = pos.initialValue;
        }

        
    }
    private void Update()
    {
        if (!inDialogue() && !DataHolder.isQuizStart)
        {
            
        /*
            verticalInput = Input.GetAxis("Vertical") ;
            horizontalInput = Input.GetAxis("Horizontal") ;

            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            */

            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

        }
        
    }

    private void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            body.velocity = new Vector2(0, verticalInput * speed * Time.fixedDeltaTime);
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            body.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, 0);
        else
            body.velocity = new Vector2(0, 0);*/
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
    }

    private bool inDialogue()
    {
        if (npc != null)
            return npc.DialogueActive();
        else 
            return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.GetComponent<NPC_Contoller>();

            if (Input.GetKey(KeyCode.E))
                npc.ActivateDialogue();
        }

        if (collision.gameObject.tag == "Doors")
        {
            if (Input.GetKey(KeyCode.E))
            {
                string sceneName = collision.gameObject.GetComponent<door_Contoller>().levelToLoad;

                if (sceneName != "ScenePrefab") {
                    savePosition();
                } else {
                    loadPorsition();
                }

                Timer.GetComponent<Timer>().SetValue();
                SceneManager.LoadScene(sceneName);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }

    public void savePosition()
    {
        LevelChanger.position = transform.position;
    }

    public void loadPorsition()
    {
        LevelChanger.willChange = true;
    }
}
