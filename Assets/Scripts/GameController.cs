using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
//    public GameObject nejiko;
    public NejikoController nejiko;
    //    public Transform nejiko;
    public Text scoreText;
    public LifePanel lifePanel;
    // Update is called once per frame
    void Update()
    {
        int score = CalcScore();
        scoreText.text = $"Score:{score}m";

//        lifePanel.UpdateLife(nejiko.GetComponent<NejikoController>().Life());
        lifePanel.UpdateLife(nejiko.Life());

    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }
}
