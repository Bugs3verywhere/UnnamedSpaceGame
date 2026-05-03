using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    public Transform teleportTarget;
    public KeyCode interactKey = KeyCode.E;
    public float spawnOffset = 1f;
    public float maxInteractDistance = 3f; // max distance to show "Press E"

    private GameObject player;
    private static TeleportDoor lastUsedDoor = null;

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > maxInteractDistance)
            {
                player = null; // player is too far → hide prompt / disable teleport
                return;
            }

            if (Input.GetKeyDown(interactKey))
            {
                if (lastUsedDoor != this)
                {
                    TeleportPlayer();
                }
            }
        }
    }

    private void TeleportPlayer()
    {
        if (player == null || teleportTarget == null) return;

        Vector3 targetPosition = teleportTarget.position + teleportTarget.forward * spawnOffset;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = targetPosition;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = targetPosition;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;
        }

        lastUsedDoor = this; // mark this door as just used
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnGUI()
    {
        if (player != null)
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