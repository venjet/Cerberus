using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MutiStart : MonoBehaviour
{
    public void OnBtnMuti(){
        SceneManager.LoadScene(2);
        GameData gameData =  GameObject.FindWithTag("GameData").GetComponent<GameData>();
        gameData.gameMode = 2;
    }
}
