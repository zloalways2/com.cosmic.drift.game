using UnityEngine;

public class Star : MonoBehaviour
{
    private UIGameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameManager.CollectStar();
            _gameManager.PlayStarSound();
            Destroy(gameObject);
        }
    }
}
