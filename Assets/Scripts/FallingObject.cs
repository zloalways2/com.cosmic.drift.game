using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private PlayerControler _player;
    private UIGameManager _gameManager;

    [SerializeField] private float _fallSpeed;

    [SerializeField] private float _playerHeightOffset = 1f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIGameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _fallSpeed * Time.deltaTime);

        if ((transform.position.y < _player.transform.position.y + _playerHeightOffset && transform.position.y > _player.transform.position.y - _playerHeightOffset) &&
            Mathf.Abs(transform.position.x - _player.transform.position.x) < 0.5f)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Obstacle"))
            {
                _gameManager.PlayObstacleSound();
                _gameManager.ReduceHeart();
            }
            else
            {
                _gameManager.CollectStar();
            }
            
        }

        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetFallSpeed(float speed)
    {
        _fallSpeed = speed;
    }
}
