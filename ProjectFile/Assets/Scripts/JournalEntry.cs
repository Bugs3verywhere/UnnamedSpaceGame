using UnityEngine;

[System.Serializable]
public class JournalEntry
{
    public string title;
    [TextArea(3, 10)]
    public string content;

    public JournalEntry (string title, string content)
    {
        this.title = title;
        this.content = content;
    }
}
