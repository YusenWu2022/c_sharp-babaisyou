using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleManager : MonoBehaviour
{   // 检查有没有语句组合信息，包括win的判定
    private static RuleManager instance;
    public List<GameObject> isObjects = new List<GameObject>();

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    public static RuleManager Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }
    void Start()
    {
        foreach(GameObject obj in PlayerMove.Instance.allObjects)
        {
            if(obj.name == "Is(Clone)") isObjects.Add(obj);
        }
    }

    void Update()
    {
        // 按R键复位（重开）
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // 返回到菜单栏
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // 检查字符是否有连续语义（baba is you，wall is you，wall is stop等）
    public bool[] checkTrue(GameObject io, bool[] bools)
    {
        bool leftWall = false; 
        bool topWall = false, leftBaba = false, topBaba = false, 
        leftFlag = false, topFlag = false, rightStop = false, bottomStop = false,
        rightYou = false, bottomYou = false, rightWin = false, bottomWin = false,
        rightPush = false, bottomPush = false;
        foreach(GameObject gameObject in PlayerMove.Instance.allObjects)
        {
            if((io.transform.position.x - 1 == gameObject.transform.position.x) && (io.transform.position.y == gameObject.transform.position.y))
            {
                if(gameObject.name == "WallString(Clone)") leftWall = true;
                if(gameObject.name == "BabaString(Clone)") leftBaba = true;
                if(gameObject.name == "FlagString(Clone)") leftFlag = true;
            }
            if((io.transform.position.y + 1 == gameObject.transform.position.y) && (io.transform.position.x == gameObject.transform.position.x))
            {
                if(gameObject.name == "WallString(Clone)") topWall = true;
                if(gameObject.name == "BabaString(Clone)") topBaba = true;
                if(gameObject.name == "FlagString(Clone)") topFlag = true;
            }
            if((io.transform.position.x + 1 == gameObject.transform.position.x) && (io.transform.position.y == gameObject.transform.position.y))
            {
                if(gameObject.name == "Stop(Clone)") rightStop = true;
                if(gameObject.name == "You(Clone)") rightYou = true;
                if(gameObject.name == "Win(Clone)") rightWin = true;
                if(gameObject.name == "Push(Clone)") rightPush = true;
            }
            if((io.transform.position.y - 1 == gameObject.transform.position.y) && (io.transform.position.x == gameObject.transform.position.x))
            {
                if(gameObject.name == "Stop(Clone)") bottomStop = true;
                if(gameObject.name == "You(Clone)") bottomYou = true;
                if(gameObject.name == "Win(Clone)") bottomWin = true;
                if(gameObject.name == "Push(Clone)") bottomPush = true;
            }
        }
        if((leftBaba && rightStop) || (topBaba && bottomStop)) bools[0] = true;
        if((leftWall && rightStop) || (topWall && bottomStop)) bools[1] = true;
        if((leftFlag && rightStop) || (topFlag && bottomStop)) bools[2] = true;
        if((leftBaba && rightYou) || (topBaba && bottomYou)) bools[3] = true;
        if((leftWall && rightYou) || (topWall && bottomYou)) bools[4] = true;
        if((leftFlag && rightYou) || (topFlag && bottomYou)) bools[5] = true;
        if((leftBaba && rightWin) || (topBaba && bottomWin)) bools[6] = true;
        if((leftWall && rightWin) || (topWall && bottomWin)) bools[7] = true;
        if((leftFlag && rightWin) || (topFlag && bottomWin)) bools[8] = true;
        if((leftBaba && rightPush) || (topBaba && bottomPush)) bools[9] = true;
        if((leftWall && rightPush) || (topWall && bottomPush)) bools[10] = true;
        if((leftFlag && rightPush) || (topFlag && bottomPush)) bools[11] = true;
        return bools; 
    }

    // 如果出现连字符，则改变对应物体/组合的bool值来表示改动
    public void isWhat()
    {
        bool[] bools = new bool[12];
        foreach(GameObject io in isObjects)
        {
            bools = checkTrue(io, bools);
        }
        if(bools[0])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isStop = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isStop = false;
            }
        }
        if(bools[1])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isStop = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isStop = false;
            }
        }
        if(bools[2])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isStop = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isStop = false;
            }
        }
        if(bools[3])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isYou = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isYou = false;
            }
        }
        if(bools[4])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isYou = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isYou = false;
            }
        }
        if(bools[5])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isYou = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isYou = false;
            }
        }
        PlayerMove.Instance.currentPlayer = new List<GameObject>();
        foreach(GameObject ob in PlayerMove.Instance.allObjects)
        {
            if(ob.GetComponent<Rule>().isYou) PlayerMove.Instance.currentPlayer.Add(ob);
        }
        if(bools[6])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isWin = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isWin = false;
            }
        }
        if(bools[7])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isWin = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isWin = false;
            }
        }
        if(bools[8])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isWin = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isWin = false;
            }
        }
        if(bools[9])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isPush = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Baba") ob.GetComponent<Rule>().isPush = false;
            }
        }
        if(bools[10])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isPush = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Wall(Clone)") ob.GetComponent<Rule>().isPush = false;
            }
        }
        if(bools[11])
        {
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isPush = true;
            }
        }else{
            foreach(GameObject ob in PlayerMove.Instance.allObjects)
            {
                if(ob.name == "Flag(Clone)") ob.GetComponent<Rule>().isPush = false;
            }
        }
    }

    // 检查当前选定玩家和所有其他对象来判断是否获胜（isWin为True in）
    // 如果位置相同，则为True，否则为false
    public bool isWin()
    {
        foreach(GameObject ob in PlayerMove.Instance.currentPlayer)
        {
            foreach(GameObject ao in PlayerMove.Instance.allObjects)
            {
                if(ob == ao) continue;
                if(ob.transform.position == ao.transform.position)
                {
                    if(ao.GetComponent<Rule>().isWin) return true;
                }
            }
        }
        return false;
    }
}
