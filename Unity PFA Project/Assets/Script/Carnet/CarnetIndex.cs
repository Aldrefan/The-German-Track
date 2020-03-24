using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetIndex : MonoBehaviour
{
    public Vector2 cellSize;
    public Vector2 spacing;
    public int columnsCount;
    public int childNumber;
    public Vector2 stickerSize;
    public List<GameObject> pageList;
    [SerializeField]
    private Transform pageArchetype;
    [SerializeField]
    private GameObject pagePrefab;
    [SerializeField]
    private Vector3 newPagePosition;
    [SerializeField]
    private  GameObject newFeedback;
    [SerializeField]
    private  Vector3 newFeedbackSize;
    List<int> stickersContained;

    void Awake()
    {
        /*transform.GetComponentInChildren<GridLayoutGroup>().cellSize = cellSize;
        transform.GetComponentInChildren<GridLayoutGroup>().spacing = spacing;
        transform.GetComponentInChildren<GridLayoutGroup>().constraintCount = columnsCount; */
        /*pageArchetype = transform.GetChild(0);*/
        //pageList.Add(pageArchetype.gameObject);
    }

    void OnEnable()
    {
        if(pageList.Count > 1)
        {
            CanvasManager.CManager.GetCanvas("ButtonsInCarnet").transform.GetChild(0).gameObject.SetActive(true);
            CanvasManager.CManager.GetCanvas("ButtonsInCarnet").transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void CheckChildNumber(GameObject newChild)
    {
        GameObject PageSelected = null;

        for(int i = 0; i < pageList.Count; i++)
        {
            if(pageList[i].transform.childCount < childNumber)
            {
                PageSelected = pageList[i];
                break;
            }
            else pageList[i].SetActive(false);
        }
        if(PageSelected == null)
        {
            PageSelected = Instantiate(pagePrefab, transform);
            PageSelected.GetComponent<GridLayoutGroup>().cellSize = cellSize;
            PageSelected.GetComponent<GridLayoutGroup>().spacing = spacing;
            PageSelected.GetComponent<GridLayoutGroup>().constraintCount = columnsCount;
            PageSelected.transform.localPosition = newPagePosition;
            pageList.Add(PageSelected);
        }
        newChild.transform.SetParent(PageSelected.transform);
        newChild.transform.localPosition = new Vector3(0,0,0);
        newChild.transform.localScale = new Vector2(stickerSize.x, stickerSize.y);
        GameObject newSymbol = Instantiate(newFeedback, newChild.transform);
        newSymbol.transform.localScale = newFeedbackSize;
        newSymbol.transform.localPosition = Vector3.zero;
    }
}
