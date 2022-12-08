using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // 点击菜单栏来选择载入的难度等级
    public void Select(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
