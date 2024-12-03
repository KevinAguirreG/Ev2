using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogs : MonoBehaviour
{
    [SerializeField] Text dialogText;


    [SerializeField] private GameObject actionSelect;
    [SerializeField] private GameObject movementSelect;
    [SerializeField] private GameObject movementDesc;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> movementTexts;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    public float charactersPerSecond = 10.0f;
    [SerializeField] Color selectedColor = Color.blue;

    public IEnumerator SetDialog(string message)
    {
        dialogText.text = "";
        foreach (var character in message)
        {
            dialogText.text += character;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }
        yield return new WaitForSeconds(1.0f);
    }

    public void ToggleDialogText(bool activated)
    {
        dialogText.enabled = activated;
    }

    public void ToggleActions(bool activated)
    {
        actionSelect.SetActive(activated);

    }

    public void ToggleMovements(bool activated)
    {
        movementSelect.SetActive(activated);
        movementDesc.SetActive(activated);
    }

    public void SelectAction(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if(i == selectedAction)
            {
                actionTexts[i].color = selectedColor;
            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void SetPokemonMovements(List<Move> moves)
    {
        for(int i = 0;i < movementTexts.Count; i++)
        {
            if(i < moves.Count)
            {
                movementTexts[i].text = moves[i].Base.Name;
            }
            else
            {
                movementTexts[i].text = "---";
            }
        }
    }

    public void SelectMovement(int selectedMovement, Move move)
    {
        for (int i = 0; i < movementTexts.Count; i++)
        {
            if (i == selectedMovement)
            {
                movementTexts[i].color = selectedColor;
            }
            else
            {
                movementTexts[i].color = Color.black;
            }
        }
        ppText.text = $"PP {move.Pp}/{move.Base.Pp}";
        typeText.text = move.Base.Type.ToString();
    }

    public void UpdateMovementDescription(Move move)
    {

    }
}
