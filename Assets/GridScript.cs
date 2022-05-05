using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    [SerializeField] private OneCellScript oneCellScriptPrefab;
	[SerializeField] private GridLayoutGroup myGrid;

    private OneCellScript[,] buttonField;

    public void Activate(int x, int y, int i)
    {
        buttonField[x, y].SetText(i);
    }
    public void CleanThisField()
    {
        foreach (OneCellScript i in buttonField)
            Destroy(i.gameObject);
    }
    public void GameStart()
    {
        int fW = GameLogicScript.fieldW;
        int fH = GameLogicScript.fieldH;
        buttonField = new OneCellScript[fW, fH];
		
		Rect mySize = GetComponent<RectTransform>().rect;
        if (mySize.height / fH < mySize.width / fW)
            myGrid.cellSize = new Vector2(mySize.height / fH, mySize.height / fH);
        else 
            myGrid.cellSize = new Vector2(mySize.width / fW, mySize.width / fW);

        //myGrid.cellSize = mySize.height / fH < mySize.width / fW ? new Vector2(mySize.height / fH, mySize.height / fH) : new Vector2(mySize.width / fW, mySize.width / fW);

        for (int j = 0; j < fH; j++)
            for (int i = 0; i < fW; i++)
            {
                buttonField[i, j] = Instantiate(oneCellScriptPrefab, transform);
                buttonField[i, j].SetPosition(i, j);
            }
    }
    void Start()
    {
        GameStart();
    }
}
