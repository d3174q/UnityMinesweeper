using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    [SerializeField] private OneCellScript oneCellScriptPrefab;
	[SerializeField] private GridLayoutGroup myGrid;

    private OneCellScript[,] buttonField; //массив кнопок (игровое поле)
    private float preferredSize = 0; //переменная чтобы новая игра начиналась с предыдущим выбранным размером

    public void Activate(int x, int y, int i) //нажатие кнопки
    {
        buttonField[x, y].SetText(i);
    }
    public void CleanThisField() //функция чтобы игру можно было перезапустить
    {
        foreach (OneCellScript i in buttonField) //удаляет все обьекты с экрана
            Destroy(i.gameObject);
    }
    public void GameStart() //основная логика создания игрового поля
    {
        int fW = GameLogicScript.fieldW; //размеры
        int fH = GameLogicScript.fieldH;
        buttonField = new OneCellScript[fW, fH];
		
		Rect mySize = GetComponent<RectTransform>().rect; //размер экрана
        if (preferredSize == 0) //если юзер размер не менял, делаем по размеру экрана
            if (mySize.height / fH < mySize.width / fW) //если в высоту поместится больше кнопок
                myGrid.cellSize = new Vector2(mySize.height / fH, mySize.height / fH);
            else
                myGrid.cellSize = new Vector2(mySize.width / fW, mySize.width / fW);
        else //если юзер размер менял, ставим старый размер
            myGrid.cellSize = new Vector2(preferredSize, preferredSize);

        //myGrid.cellSize = mySize.height / fH < mySize.width / fW ? //штуку выше можно было сделать тернарным оператором
        //new Vector2(mySize.height / fH, mySize.height / fH) : new Vector2(mySize.width / fW, mySize.width / fW);

        for (int j = 0; j < fH; j++) //забивает игровое поле кнопками
            for (int i = 0; i < fW; i++)
            {
                buttonField[i, j] = Instantiate(oneCellScriptPrefab, transform);
                buttonField[i, j].SetPosition(i, j); //сообщает кнопке её место >:(
            }
    }
    void Start() //старт
    {
        GameStart(); //геймстарт
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals)) //если юзер нажал клавишу с плюсом (+)
        {
            myGrid.cellSize = new Vector2(myGrid.cellSize.x + 1, myGrid.cellSize.y + 1); //делает побольше
            preferredSize = myGrid.cellSize.x; //и запоминает размер для будующих геймстартов

        }
        if (Input.GetKeyDown(KeyCode.Minus)) //если минус(-)
        {
            myGrid.cellSize = new Vector2(myGrid.cellSize.x - 1, myGrid.cellSize.y - 1); //делает поменьше
            preferredSize = myGrid.cellSize.x;
        }
    }
}
