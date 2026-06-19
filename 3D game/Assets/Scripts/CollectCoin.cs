using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] AudioSource coinSound;
    public int coins = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Coin();
        }
    }

    void Coin()
    {
        coinSound.Play();
        coins++;
        coinsText.text = "Coins: "+coins;
    }
}
