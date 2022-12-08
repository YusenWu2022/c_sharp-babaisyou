using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public LevelCreator levelCreator;
    public GameObject parentMap;
    private GameObject createObject;

    // 读取creator信息来构建地图
    void Awake()
    {
        float currentX = -9.5f;
        float currentY = 8.5f;
        foreach(ElementTypes elementType in levelCreator.level)
        {
            if(elementType.ToString() == "Empty")
            {
                currentX += 1;
                if(currentX > 9.5f)
                {
                    currentX = -9.5f;
                    currentY -= 1;
                }
                continue;
            }
            foreach(GameObject ob in Prefabs)
            {
                if(elementType.ToString() == ob.name)
                {
                    createObject = Instantiate(ob, new Vector3(currentX, currentY, 0), Quaternion.identity);
                    createObject.transform.parent = parentMap.transform;
                }
            }
            currentX += 1;
            if(currentX > 9.5f)
            {
                currentX = -9.5f;
                currentY -= 1;
            }
        }
    }
    void Start()
    {

    }
    public void Update()
    {

    }
}
