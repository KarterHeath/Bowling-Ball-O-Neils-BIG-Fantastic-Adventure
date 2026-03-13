using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinCount = Player.Instance.coins;
    }
    // Update is called once per frame
    private void Update()
    {
        coinText.text = "COINS: " + coinCount.ToString();
    }
}
