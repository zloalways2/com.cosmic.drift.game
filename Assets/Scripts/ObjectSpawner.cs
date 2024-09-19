using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToSpawn;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _minSpawnDistance = 2f;
    [SerializeField] private float _maxSpawnDistance = 5f;
    [SerializeField] private float _fallSpeed = 2f;

    private Camera _mainCamera;

    private float _screenTop;

    private int _currentLevel;

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenTop = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        _fallSpeed += 0.1f * _currentLevel;
        _spawnInterval -= 0.05f * _currentLevel;

        InvokeRepeating("SpawnObjects", 0f, _spawnInterval);
    }

    private void SpawnObjects()
    {
        Vector2 spawnPosition = GenerateSpawnPosition();

        if (spawnPosition != Vector2.zero)
        {
            GameObject obj;

            if (Random.value < 0.35f)
            {
                obj = _objectsToSpawn[Random.Range(0, 3)];
            }
            else
            {
                obj = _objectsToSpawn[3];
            }

            GameObject spawnedObject = Instantiate(obj, spawnPosition, Quaternion.identity);

            FallingObject fallingObject = spawnedObject.AddComponent<FallingObject>();
            fallingObject.SetFallSpeed(_fallSpeed);
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            float screenX = Random.Range(0f, 1f);
            float worldX = _mainCamera.ViewportToWorldPoint(new Vector3(screenX, 0, 0)).x;

            float worldY = _screenTop;

            Vector2 newPosition = new Vector2(worldX, worldY);

            return newPosition;
        }

        return Vector2.zero;
    }
}
