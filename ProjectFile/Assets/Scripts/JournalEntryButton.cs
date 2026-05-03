using UnityEngine;
using UnityEngine.UI;

public class JournalEntryButton : MonoBehaviour 
{
    public Text label;

    private JournalEntry entry;
    private JournalUI journalUI;

    public void Setup(JournalEntry newEntry, JournalUI ui)
    {
        entry = newEntry;
        journalUI = ui;
        label.text = entry.title;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        journalUI.SendMessage("ShowEntry, entry");
    }
}
