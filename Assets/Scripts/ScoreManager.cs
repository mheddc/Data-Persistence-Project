using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text myText;

    void Start()
    {
        Debug.Log("muAAAAA" + DataManager.Instance.PlayerName);
        myText.text = "Best Score : " + DataManager.Instance.PlayerName + " " + DataManager.Instance.Get(DataManager.Instance.PlayerName);
    }
}

