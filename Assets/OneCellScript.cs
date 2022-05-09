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

    private (int x, int y) myPosition; //переменная, чтобы работала функция нажатия
    public void OnClick() //функция нажатия
    {
        if (myImage.sprite != flagSpr) //если без флажка
            if (myButton.interactable) //если можно взаимодействовать
                mainScript.ButtonClick(myPosition.x, myPosition.y); //жмём
    }
    public void OnPointerClick() //функция для нажимания ПКМ
    {
        if (myButton.interactable)
            if (Input.GetMouseButtonUp(1)) //если ПКМ
                if (myImage.sprite != flagSpr) //если не флаг
                    myImage.sprite = flagSpr; //ставим флаг
                else //иначе
                    myImage.sprite = defaultSpr; //убираем флаг
    }
    public void SetPosition(int x, int y) //функция чтобы кнопка знала своё место ):<
    {
        myPosition.x = x;
        myPosition.y = y;
    }
    public void SetText(int i) //когда всё хорошо, открываем поле (функция нажатия)
    {
        if (myButton.interactable)
            if (myImage.sprite != flagSpr)
            {
                myButton.interactable = false; //убираем возможность нажимать 
                mainScript.fieldsCount--; //отнимаем счётчик для победы
                switch (i) //ставим цвет
                {
                    case 0: //если вокруг нет мин, открываем всё вокруг (рекурсия)
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
                        myImage.sprite = bombSpr; //если больше восьми, ставим картинку мины
                        break;
                }
                if (i > 0 & i < 9)
                    myText.text = i.ToString(); //ставим циферку
            }
    }
    private void Awake()
    {
        mainScript = FindObjectOfType<GameLogicScript>();
    }
}
