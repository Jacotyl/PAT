using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    public void toScoreScene() {
        SceneManager.LoadScene("score");
    }
}
