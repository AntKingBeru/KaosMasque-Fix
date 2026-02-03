using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Paragraph")]
public class DialogueParagraph : ScriptableObject
{
    [TextArea(3, 6)]
    public string text;
    
    public bool triggerAnimation;
    public string animationTrigger;
}