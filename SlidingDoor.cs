using UnityEngine;
using UnityEngine.Rendering;

public class SlidingDoor : MonoBehaviour 
{
    public Transform door;
    public float slideDistance = 3f;
    public float speed = 2f;
    public KeyCode interactKey = KeyCode.E;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen;
    private bool playerInRange = false;
    
    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + Vector3.right * slideDistance;
    }

    void Update()
    {
        if(isOpen)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * speed);
        }
        
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, Time.deltaTime * speed);
        }

        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OnGUI()
    {
        if (playerInRange)
        {
            GUIStyle promptStyle = new GUIStyle(GUI.skin.label);
            promptStyle.fontSize = 20;
            promptStyle.alignment = TextAnchor.MiddleCenter;
            promptStyle.normal.textColor = Color.white;

            GUI.Label(
                new Rect(Screen.width / 2 - 100, Screen.height - 120, 200, 50),
                $"Press {interactKey}",
                promptStyle);
        }
    }
}
