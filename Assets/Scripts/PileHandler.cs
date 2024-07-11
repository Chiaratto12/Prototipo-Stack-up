using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileHandler : MonoBehaviour
{
    [Header("Instance")]
    public static PileHandler instance;

    [Header("Pile Movement")]
    [SerializeField] public float xMove;
    [SerializeField] public float zMove;
    [SerializeField] public float yVariation;
    [SerializeField] private Vector3 firstItemPos;
    [SerializeField] private Vector3 currentItemPos;

    [Header("Pile List")]
    [SerializeField] public List<GameObject> itemList = new List<GameObject>();
    [SerializeField] public int itemListIndexCounter = 0;
    [SerializeField] public static int maxPileCapacity;

    private void Awake()
    {
         if(instance == null) instance = this;
    }

    private void Start()
    {
        //playerController = GetComponent<PlayerController>();
        GetComponent<ItemHandler>().UpdateCubePosition(PlayerController.instance.transform.GetChild(0), true);

        maxPileCapacity++;
    }

    void Update()
    {
        Debug.Log(itemList.Count);
    }

    public void AddToPile(GameObject item)
    {
        if(itemList.Count < maxPileCapacity)
        {
            item.transform.rotation = new Quaternion();
            itemList.Add(item.gameObject);
            item.GetComponent<Animator>().SetBool("TPose", true);
            //item.transform.SetParent(transform);
            if (itemList.Count == 1)
            {
                firstItemPos = GetComponent<MeshRenderer>().bounds.max;
                currentItemPos = new Vector3(item.transform.position.x, firstItemPos.y, item.transform.position.z);
                item.gameObject.transform.position = currentItemPos;
                currentItemPos = new Vector3(item.transform.position.x, transform.position.y + 2.0f, item.transform.position.z);
                item.gameObject.GetComponent<ItemHandler>().UpdateCubePosition(transform, true);
            }
            else if (itemList.Count > 1)
            {
                item.gameObject.transform.position = currentItemPos;
                currentItemPos = new Vector3(item.transform.position.x, item.gameObject.transform.position.y + yVariation, item.transform.position.z);
                item.gameObject.GetComponent<ItemHandler>().UpdateCubePosition(itemList[itemListIndexCounter].transform, true);
                itemListIndexCounter++;
            }
        }
    }
}
