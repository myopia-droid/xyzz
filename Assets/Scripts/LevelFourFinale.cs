using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class LevelFourFinale : MonoBehaviour
{
    public DialogManager DialogManager;
    private float health;

    void Start() {
      health = PlayerPrefs.GetFloat("health");
    }

    void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("The horror of this house only grows every minute you stay here.", "Narrator"));
        dialogTexts.Add(new DialogData("The young boy's mother has clearly met a violent end,\nbut at least you can return her hairpin to him.", "Narrator"));
        dialogTexts.Add(new DialogData("You're back! Where's... where's mother?", "Boy"));
        dialogTexts.Add(new DialogData("I'm sorry... but I think something terrible has happened to her... I found only these in the conservatory.", "Protagonist"));
        dialogTexts.Add(new DialogData("No... NO! Mother can't be! You're lying! I'll find her myself!", "Boy"));
        dialogTexts.Add(new DialogData("Before you can protest, the boy runs down the hallway and disappears into the darkness.", "Narrator"));
        dialogTexts.Add(new DialogData("Your lantern is almost out of fuel. You won't be able to go after him.", "Narrator"));
        dialogTexts.Add(new DialogData("The only way to help these souls is to find the exit and let in the light.", "Narrator"));
        dialogTexts.Add(new DialogData("Getting back into the elevator, it starts going up again...", "Narrator"));
        DialogData finalLine = new DialogData("I need the map fast... this house doesn't feel right at all!", "Protagonist");
        dialogTexts.Add(finalLine);

        DialogManager.Show(dialogTexts);

        finalLine.Callback = () => GoToPlatformer();

    }

    void GoToPlatformer()
    {
        SceneManager.LoadScene("Level4Intro");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("health", health);
    }
}
