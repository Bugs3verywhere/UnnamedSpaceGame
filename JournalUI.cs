using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class JournalUI : MonoBehaviour 
{
    public GameObject journalPanel;
    public Transform entryListParent;
    public GameObject entryButtonPrefab;
    public TMP_Text entryContentText;

    private bool isOpen = false;

    private void Start()
    {
        journalPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        isOpen = !isOpen;
        journalPanel.SetActive(isOpen);

        if(isOpen)
        {
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        foreach (Transform child in entryListParent)
        {
            Destroy(child.gameObject);
        }

        List<JournalEntry> entries = JournalManager.Instance.GetEntries();

        foreach (var entry in entries)
        {
            GameObject buttonObj = Instantiate(entryButtonPrefab, entryListParent);

            JournalEntryButton button = buttonObj.GetComponent<JournalEntryButton>();
            button.Setup(entry, this);
        }
    }

    void ShowEntry(JournalEntry entry)
    {
        entryContentText.text = entry.content;
    }
}
