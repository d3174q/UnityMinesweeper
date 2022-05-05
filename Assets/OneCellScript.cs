using UnityEngine;

public class OneCellScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text myText;
    [SerializeField] private UnityEngine.UI.Button myButton;
    [SerializeField] private UnityEngine.UI.Image myImage;
    [SerializeField] private Sprite flagSpr;
    [SerializeField] private Sprite bombSpr;
    [SerializeField] private Sprite defaultSpr;

    GameLogicScript mainScript;

    private (int x, int y) myPosition;
    public void OnClick()
    {
        if (myImage.sprite != flagSpr)
            if (myButton.interactable)
                mainScript.ButtonClick(myPosition.x, myPosition.y);
    }
    public void OnPointerClick()
    {
        if (myButton.interactable)
            if (Input.GetMouseButtonUp(1))
                if (myImage.sprite != flagSpr)
                {
                    myImage.sprite = flagSpr;
                }
                else
                {
                    myImage.sprite = defaultSpr;
                }
    }
    public void SetPosition(int x, int y)
    {
        myPosition.x = x;
        myPosition.y = y;
    }
    public void SetText(int i)
    {
        if (myButton.interactable)
            if (myImage.sprite != flagSpr)
            {
                myButton.interactable = false;
                mainScript.fieldsCount--;
                switch (i)
                {
                    case 0:
                        for (int k = myPosition.x - 1; k < myPosition.x + 2; k++)
                            if (k > -1 & k < 30)
                                for (int l = myPosition.y - 1; l < myPosition.y + 2; l++)
                                    if (l > -1 & l < 16)
                                        mainScript.ButtonClick(k, l);
                        break;
                    case 1:
                        myText.color = Color.blue;
                        break;
                    case 2:
                        myText.color = Color.green;
                        break;
                    case 3:
                        myText.color = Color.red;
                        break;
                    case 4:
                        myText.color = Color.yellow;
                        break;
                    case 5:
                        myText.color = Color.cyan;
                        break;
                    case 6:
                        myText.color = Color.magenta;
                        break;
                    case 7:
                        myText.color = Color.grey;
                        break;
                    case 8:
                        myText.color = Color.black;
                        break;
                    default:
                        myImage.sprite = bombSpr;
                        break;
                }
                if (i > 0 & i < 9)
                    myText.text = i.ToString();
            }
    }
    private void Awake()
    {
        mainScript = FindObjectOfType<GameLogicScript>();
    }
}
