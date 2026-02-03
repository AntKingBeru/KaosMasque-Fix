using UnityEngine;
using System.Collections.Generic;

public class DialogueLog : MonoBehaviour
{
    public static DialogueLog Instance;

    private readonly List<string> _log = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddEntry(string text)
    {
        _log.Add(text);
    }

    public string GetFullLog()
    {
        return string.Join("\n\n", _log);
    }

    public void Clear()
    {
        _log.Clear();
    }
}