using UnityEngine;
using UnityEngine.UI;

public class DialogBoxScript : MonoBehaviour
{
    [SerializeField] Text myText;
    GameLogicScript mainScript;
    public void ISeeThitAsAnAbsoluteWin()
    {
        myText.text = "Поздравляю, вас не порвало на мине, хотите ещё?";
    }
    public void OnClickYes()
    {
        mainScript.DialogBox(true);
    }
    public void OnClickNo()
    {
        mainScript.DialogBox(false);
    }
    private void Awake()
    {
        mainScript = FindObjectOfType<GameLogicScript>();
    }
}
