using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// 当前玩家移动（通过上下左右来实现移动）
public class PlayerMove : MonoBehaviour
{
    private static PlayerMove instance;
    public List<GameObject> currentPlayer = new List<GameObject>();
    public GameObject[] allObjects;
    public Animator an;
    public int moveDistance = 1;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
        allObjects = GameObject.FindGameObjectsWithTag("object");
    }
    public static PlayerMove Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        // 注意到不能有重叠的冲突情况
        if (Input.GetKeyDown("right"))
        {
            List<GameObject> movingObjects = new List<GameObject>();
            movingObjects = moveObjects(movingObjects, 1, 0);
            movingObjects = movingObjects.Distinct().ToList();
            foreach(GameObject ob in movingObjects)
            {
                ob.transform.Translate(new Vector2(moveDistance, 0));
                if(ob.name == "Baba") an.Play("babaright");
            }
            if(RuleManager.Instance.isWin()) SceneManager.LoadScene("Menu");
        }
        if(Input.GetKeyDown("left"))
        {
            List<GameObject> movingObjects = new List<GameObject>();
            movingObjects = moveObjects(movingObjects, -1, 0);
            movingObjects = movingObjects.Distinct().ToList();
            foreach(GameObject ob in movingObjects)
            {
                ob.transform.Translate(new Vector2(-moveDistance, 0));
                if(ob.name == "Baba") an.Play("babaleft");
            }
            if(RuleManager.Instance.isWin()) SceneManager.LoadScene("Menu");
        }
        if(Input.GetKeyDown("up"))
        {
            List<GameObject> movingObjects = new List<GameObject>();
            movingObjects = moveObjects(movingObjects, 0, 1);
            movingObjects = movingObjects.Distinct().ToList();
            foreach(GameObject ob in movingObjects)
            {
                ob.transform.Translate(new Vector2(0, moveDistance));
                if(ob.name == "Baba") an.Play("babaup");
            }
            if(RuleManager.Instance.isWin()) SceneManager.LoadScene("Menu");
        }
        if(Input.GetKeyDown("down"))
        {
            List<GameObject> movingObjects = new List<GameObject>();
            movingObjects = moveObjects(movingObjects, 0, -1);
            movingObjects = movingObjects.Distinct().ToList();
            foreach(GameObject ob in movingObjects)
            {
                ob.transform.Translate(new Vector2(0, -moveDistance));
                if(ob.name == "Baba") an.Play("babadown");
            }
            if(RuleManager.Instance.isWin()) SceneManager.LoadScene("Menu");
        }
    }

    public List<GameObject> moveObjects(List<GameObject> movingObjects, float dx, float dy)
    {
        // 输入玩家移动方向，对本体和其他物体造成动作
        foreach(GameObject cp in currentPlayer)
        {
            float currentX = cp.transform.position.x + dx;
            float currentY = cp.transform.position.y + dy;
            movingObjects.Add(cp);
            for(int i = 0; i < 30; i++)
            {
                bool check = false;
                foreach(GameObject ao in allObjects)
                {
                    if(ao.transform.position.x == currentX && ao.transform.position.y == currentY)
                    {
                        if(ao.GetComponent<Rule>().isStop)
                        {
                            if(!ao.GetComponent<Rule>().isPush) return new List<GameObject>();
                        }
                        if(!ao.GetComponent<Rule>().isPush) continue;
                        movingObjects.Add(ao);
                        check = true;
                    }
                }
                currentX += dx;
                currentY += dy;
                if(!check) break;
            }
        }
        return movingObjects;
    }
}
