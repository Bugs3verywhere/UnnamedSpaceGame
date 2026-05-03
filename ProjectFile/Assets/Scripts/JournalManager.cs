using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public static JournalManager Instance;

    private List<JournalEntry> entries = new List<JournalEntry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEntry(JournalEntry entry)
    {
        foreach (var e in entries)
        {
            if (e.title == entry.title)
                return;
        }

        entries.Add(entry);
        Debug.Log("New journal entry added." + entry.title);
    }

    public List<JournalEntry> GetEntries()
    {
        return entries;
    }
}
