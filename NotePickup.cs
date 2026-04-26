using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public string noteTitle;
    [TextArea(3, 10)]
    public string noteContent;

    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            PickUpNote();
        }
    }

    private void PickUpNote()
    {
        JournalEntry entry = new JournalEntry(noteTitle, noteContent);
        JournalManager.Instance.AddEntry(entry);

        Debug.Log("Picked up note: " + noteTitle);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
