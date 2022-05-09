using UnityEngine;
using UnityEngine.UI;

public class DialogBoxScript : MonoBehaviour
{
    [SerializeField] Text myText;
    GameLogicScript mainScript;
    public void ISeeThitAsAnAbsoluteWin() //при победе
    {
        myText.text = "Поздравляю, вас не порвало на мине, хотите ещё?"; //радуем юзера
    }
    public void OnClickYes()
    {
        mainScript.DialogBox(true); //если нажал да
    }
    public void OnClickNo()
    {
        mainScript.DialogBox(false); //если нажал нет
    }
    private void Awake()
    {
        mainScript = FindObjectOfType<GameLogicScript>();
    }
}
