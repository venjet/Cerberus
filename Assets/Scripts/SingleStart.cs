using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleStart : MonoBehaviour
{

    public void OnBtnSingle(){
        SceneManager.LoadScene(1);
        GameData gameData =  GameObject.FindWithTag("GameData").GetComponent<GameData>();
        gameData.gameMode = 1;
    }

}
