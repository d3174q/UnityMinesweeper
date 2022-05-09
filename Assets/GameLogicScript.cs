using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour //заметочка: сделать переменную для количества бомб
{
    [SerializeField] DialogBoxScript dialogBoxPrefab;
    [SerializeField] Canvas myCanvas;
    GridScript gridScript;

    public static int fieldW = 30; //размеры игрового поля
    public static int fieldH = 16;

    private int[,] mineField; //массив для работы с бомбами
    private List<(int x, int y)> mineList; //список бомб
    public int fieldsCount; //счётчик открытых полей для победы

    DialogBoxScript dialogBox; //диалоговое окно для работы с победой/проигрышом

    public void DialogBox(bool yesOrNo) //функция ответа диалогового окна
    {
        if (yesOrNo) //если юзер нажал "да"
        {
            GameStart(); //всё по новой
            gridScript.CleanThisField(); //просим сетку почистить кнопки
            gridScript.GameStart(); //всё по новой
            Destroy(dialogBox.gameObject); //убирает диалоговое окно
            dialogBox = null; //я не знаю зачем написал эту строку, я не проверял работоспособность без неё
        }
        else //иначе
        {
#if UNITY_EDITOR //если в юнити редакторе
            UnityEditor.EditorApplication.isPlaying = false; //выключаем редактор
#else //иначе
                Application.Quit(); //выключаем приложение
#endif
        }
    }
    private void Boom() //подрыв на мине
    {
        for (int i = 0; i < mineList.Count; i++) //находим все мины
            gridScript.Activate(mineList[i].x, mineList[i].y, mineField[mineList[i].x, mineList[i].y]); //и показываем
        dialogBox = Instantiate(dialogBoxPrefab, myCanvas.transform); //а потом спрашивает юзера что дальше
    }
    private void ISeeThitAsAnAbsoluteWin() //в случае победы (неоткрытых полей без бомб не осталось)
    {
        dialogBox = Instantiate(dialogBoxPrefab, myCanvas.transform); //спрашиваем юзера что дальше
        dialogBox.ISeeThitAsAnAbsoluteWin(); //но сообщаем ему, что он победил
    }
    public void ButtonClick(int x, int y) //нажатие кнопки
    {
        if (mineField[x, y] > 8) //если бомба
            Boom(); //бум
        else //иначе
        {
            gridScript.Activate(x, y, mineField[x, y]); //открываем
            if (fieldsCount == 99) //если закрытых полей столько
                ISeeThitAsAnAbsoluteWin(); //поебда
        }
    }
    private void GameStart() //основная логика создания игрового поля
    {
        int fW = fieldW; //размеры
        int fH = fieldH;
        fieldsCount = fW * fH; //сколько полей всего (обычно 480)
        mineField = new int[fW, fH]; 
        mineList = new List<(int x, int y)>();

        int k, l; //переменные для работы с рандомом
        for (int i = 0; i < 99; i++) //раскидываем бомбы
        {
            k = Random.Range(0, fW);
            l = Random.Range(0, fH);
            if (mineField[k, l] < 9) //если не бомба
            {
                mineField[k, l] = 9; //делаем бомбой (nine means bomb)
                for (int z = k - 1; z < k + 2; z++) //и сообщаем всем вокруг где бомба
                    if (z > -1 & z < fW)
                        for (int x = l - 1; x < l + 2; x++)
                            if (x > -1 & x < fH)
                                mineField[z, x]++;
                mineList.Add((k, l)); //записываем где бомба
            }
            else //иначе
                i--; //добавляем попытку
        }
    }
    void Start() //старт
    {
        GameStart(); //геймстарт
    }
    private void Awake()
    {
        gridScript = FindObjectOfType<GridScript>();
    }
}