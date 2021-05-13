using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class LevelFourIntroDialogue : MonoBehaviour
{
    public DialogManager DialogManager;

    void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("Your lantern is running out of fuel. It is getting\nharder to see.", "Narrator"));
        dialogTexts.Add(new DialogData("Please could you help me? I can not find my mother.", "Boy"));
        dialogTexts.Add(new DialogData("Are you lost?", "Protagonist"));
        dialogTexts.Add(new DialogData("Mother and I got separated. We were on our way to the conservatory when I saw a beetle and let go of her hand to pick it up.", "Boy"));
        dialogTexts.Add(new DialogData("Now I don�t know where she is, and I do not want to walk the\nhalls without her.", "Boy"));
        dialogTexts.Add(new DialogData("How about I see if I can find her? You said the two of you were going to the conservatory?", "Protagonist"));
        DialogData finalLine = new DialogData("Yes! Mother is an accomplished pianist. You will know\nit is her by the hairpin Father gave her - gold with a blue flower.\nPlease hurry - I know she must be worried about me.", "Boy");
        dialogTexts.Add(finalLine);

        DialogManager.Show(dialogTexts);

        finalLine.Callback = () => GoToPlatformer();

    }

    void GoToPlatformer()
    {
        SceneManager.LoadScene("Level4 first");
    }
}