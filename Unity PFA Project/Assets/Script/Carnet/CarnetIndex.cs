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
    public List<GameObject> pageList;
    public Transform pageArchetype;
    public GameObject pagePrefab;
    public Vector3 newPagePosition;
    public GameObject newFeedback;
    public Vector3 newFeedbackSize;
    // Start is called before the first frame update

    void Awake()
    {
        /*transform.GetComponentInChildren<GridLayoutGroup>().cellSize = cellSize;
        transform.GetComponentInChildren<GridLayoutGroup>().spacing = spacing;
        transform.GetComponentInChildren<GridLayoutGroup>().constraintCount = columnsCount; */
        /*pageArchetype = transform.GetChild(0);*/
        //pageList.Add(pageArchetype.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject newSymbol = Instantiate(newFeedback, newChild.transform);
        newSymbol.transform.localScale = newFeedbackSize;
        newSymbol.transform.localPosition = Vector3.zero;

    }
}
