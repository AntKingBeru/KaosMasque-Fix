using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TypewriterDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    [SerializeField] private DialogueAsset dialogue;
    [SerializeField] private float charactersPerSecond = 40f;

    [SerializeField] private Animator animator;

    private int _currentParagraphIndex;
    private bool _isTyping;
    private Coroutine _typingCoroutine;

    private void Start()
    {
        dialogueText.text = "";
        _currentParagraphIndex = 0;
        PlayParagraph();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var clicked =
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        var keyPressed =
            Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;

        if (clicked || keyPressed)
        {
            OnAdvanceInput();
        }
    }
    
    private void OnAdvanceInput()
    {
        if (_isTyping)
        {
            StopCoroutine(_typingCoroutine);
            dialogueText.text = dialogue.paragraphs[_currentParagraphIndex].text;
            _isTyping = false;
        }
        else
        {
            AdvanceParagraph();
        }
    }

    private void PlayParagraph()
    {
        var paragraph = dialogue.paragraphs[_currentParagraphIndex];

        if (paragraph.triggerAnimation && animator)
        {
            animator.SetTrigger(paragraph.animationTrigger);
        }

        _typingCoroutine = StartCoroutine(TypeText(paragraph.text));
    }

    private IEnumerator TypeText(string text)
    {
        _isTyping = true;
        dialogueText.text = "";

        foreach (var character in text)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(1f / charactersPerSecond);
        }

        _isTyping = false;

        DialogueLog.Instance?.AddEntry(text);
    }

    private void AdvanceParagraph()
    {
        _currentParagraphIndex++;

        if (_currentParagraphIndex >= dialogue.paragraphs.Length)
        {
            DialogueFinished();
            return;
        }

        PlayParagraph();
    }

    private void DialogueFinished()
    {
        SceneFader.Instance.FadeToScene(2);
    }
}