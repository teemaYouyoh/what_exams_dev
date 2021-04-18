using UnityEngine;

public class NPC_Contoller : MonoBehaviour
{
    [SerializeField] private GameObject Dialogue;

    public void ActivateDialogue()
    {
        Dialogue.SetActive(true);
    }

    public bool DialogueActive()
    {
        return Dialogue.activeInHierarchy;
    }
}
