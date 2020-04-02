using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleStart : MonoBehaviour
{

    public void OnBtnSingle(){
        SceneManager.LoadScene(1);
    }

}
