using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField] DialogBoxScript dialogBoxPrefab;
    [SerializeField] Canvas myCanvas;
    GridScript gridScript;

    public static int fieldW = 30;
    public static int fieldH = 16;

    private int[,] mineField;
    private List<(int x, int y)> mineList;
    public int fieldsCount;

    DialogBoxScript dialogBox;

    public void DialogBox(bool yesOrNo)
    {
        if (yesOrNo)
        {
            GameStart();
            gridScript.CleanThisField();
            gridScript.GameStart();
            Destroy(dialogBox.gameObject);
            dialogBox = null;
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
    private void Boom()
    {
        for (int i = 0; i < mineList.Count; i++)
            gridScript.Activate(mineList[i].x, mineList[i].y, mineField[mineList[i].x, mineList[i].y]);
        dialogBox = Instantiate(dialogBoxPrefab, myCanvas.transform);
    }
    private void ISeeThitAsAnAbsoluteWin()
    {
        dialogBox = Instantiate(dialogBoxPrefab, myCanvas.transform);
        dialogBox.ISeeThitAsAnAbsoluteWin();
    }
    public void ButtonClick(int x, int y)
    {
        if (mineField[x, y] > 8)
            Boom();
        else
        {
            gridScript.Activate(x, y, mineField[x, y]);
            if (fieldsCount == 99)
                ISeeThitAsAnAbsoluteWin();
        }
    }
    private void GameStart()
    {
        int fW = fieldW;
        int fH = fieldH;
        fieldsCount = fW * fH;
        mineField = new int[fW, fH];
        mineList = new List<(int x, int y)>();

        int k, l;
        for (int i = 0; i < 99; i++)
        {
            k = Random.Range(0, fW);
            l = Random.Range(0, fH);
            if (mineField[k, l] < 9)
            {
                mineField[k, l] = 9; //nine means bomb
                for (int z = k - 1; z < k + 2; z++)
                    if (z > -1 & z < fW)
                        for (int x = l - 1; x < l + 2; x++)
                            if (x > -1 & x < fH)
                                mineField[z, x]++;
                mineList.Add((k, l));
            }
            else
                i--;
        }
    }
    void Start()
    {
        GameStart();
    }
    private void Awake()
    {
        gridScript = FindObjectOfType<GridScript>();
    }
}
