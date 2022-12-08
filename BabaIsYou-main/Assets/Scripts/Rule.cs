using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    public bool isStop = false;
    public bool isPush = true;
    public bool isWin = false;
    public bool isYou = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 字符2D语义组合（rule)处理，调用ruleManager处理者（处理baba,is,you,wall等语句）
    void OnTriggerEnter2D(Collider2D coll)
    {
        RuleManager.Instance.isWhat();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        RuleManager.Instance.isWhat();
    }
}
