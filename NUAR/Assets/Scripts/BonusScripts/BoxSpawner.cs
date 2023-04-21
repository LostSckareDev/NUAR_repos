using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject ThompsonBox;    //первый префаб объекта для спавна
    public GameObject WinchesterBox; //второй префаб объекта для спавна
    public GameObject SpeedBox; //третий префаб объекта для спавна
    public GameObject HealthBox; //четвёртый префаб объекта для спавна
    public float spawnDelay = 1f;       //задержка между спавнами
    public int maxSpawnCount = 10;      //максимальное количество объектов на сцене
    public float spawnAreaWidth = 10f;  //ширина прямоугольной зоны спавна
    public float spawnAreaHeight = 10f; //высота прямоугольной зоны спавна

    private int currentSpawnCount = 0;  //текущее количество объектов на сцене
    private List<GameObject> spawnedObjects = new List<GameObject>(); //список спавнутых объектов

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            //проверяем, не достигнуто ли максимальное количество объектов на сцене
            if (currentSpawnCount >= maxSpawnCount)
            {
                yield return null;
                continue;
            }

            //генерируем случайную точку в пределах прямоугольной зоны спавна
            Vector2 spawnPoint = new Vector2(Random.Range(transform.position.x - spawnAreaWidth / 2, transform.position.x + spawnAreaWidth / 2), Random.Range(transform.position.y - spawnAreaHeight / 2, transform.position.y + spawnAreaHeight / 2));

            // проверяем, не попадает ли точка спавна на стены
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint, 0.5f);
            bool canSpawn = true;
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("WallCollider") || col.CompareTag("ThompsonBox") || col.CompareTag("WinchesterBox") || col.CompareTag("SpeedBox") || col.CompareTag("HealthBox"))
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                //спавним объект и добавляем его в список спавнутых объектов
                int randomIndex = Random.Range(0, 4);
                GameObject obj;
                switch(randomIndex)
                {
                    case 0:
                        obj = Instantiate(ThompsonBox, spawnPoint, Quaternion.identity);
                        break;
                    case 1:
                        obj = Instantiate(WinchesterBox, spawnPoint, Quaternion.identity);
                        break;
                    case 2:
                        obj = Instantiate(SpeedBox, spawnPoint, Quaternion.identity);
                        break;
                    case 3:
                        obj = Instantiate(HealthBox, spawnPoint, Quaternion.identity);
                        break;
                    default:
                        obj = Instantiate(HealthBox, spawnPoint, Quaternion.identity);
                        break;
                }
                spawnedObjects.Add(obj);
                currentSpawnCount++;
            }

            //ждём заданную задержку перед следующим спавном
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        //удаляем объекты из списка спавнутых объектов, если они уничтожены
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null)
            {
                spawnedObjects.RemoveAt(i);
                currentSpawnCount--;
            }
        }
    }
}
