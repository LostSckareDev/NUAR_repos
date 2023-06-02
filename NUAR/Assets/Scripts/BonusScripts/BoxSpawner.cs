using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BoxSpawner : MonoBehaviour
{
    PhotonView view;
    public float spawnDelay = 3.5f;       //задержка между спавнами
    public int maxSpawnCount = 10;      //максимальное количество объектов на сцене
    public float spawnAreaWidth = 10f;  //ширина прямоугольной зоны спавна
    public float spawnAreaHeight = 10f; //высота прямоугольной зоны спавна
    public GameObject HealthBox;
    public GameObject ThompsonBox;
    public GameObject WinchesterBox;
    public GameObject SpeedBox;

    GameObject obj;
    private int currentSpawnCount = 0;  //текущее количество объектов на сцене
    private List<GameObject> spawnedObjects = new List<GameObject>(); //список спавнутых объектов

    private void Start()
    {
        view = GetComponent<PhotonView>();
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            //удаляем объекты из списка спавнутых объектов, если они уничтожены
            for (int i = currentSpawnCount - 1; i >= 0; i--)
            {
                if (spawnedObjects[i] == null)
                {
                    spawnedObjects.RemoveAt(i);
                    currentSpawnCount--;
                }
            }
            //проверяем, не достигнуто ли максимальное количество объектов на сцене
            if (currentSpawnCount >= maxSpawnCount)
            {
                yield return null;
                continue;
            }

            //генерируем случайную точку в пределах прямоугольной зоны спавна
            Vector2 spawnPoint = new Vector2(Random.Range(-37f, 41f), Random.Range(-22f, 26f));

            // проверяем, не попадает ли точка спавна на стены
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint, 1f);
            bool canSpawn = true;
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("WallCollider") || col.CompareTag("ThompsonBox") || col.CompareTag("WinchesterBox") || col.CompareTag("SpeedBox") || col.CompareTag("HealthBox") || col.CompareTag("Player"))
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                //спавним объект и добавляем его в список спавнутых объектов
                int randomIndex = Random.Range(0, 4);
                view.RPC("Spawn", RpcTarget.AllBufferedViaServer, randomIndex, spawnPoint);
                currentSpawnCount++;
            }

            //ждём заданную задержку перед следующим спавном
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    [PunRPC]
    public void Spawn(int number, Vector2 Point)
    {
        switch (number)
        {
            case 0:
                obj = Instantiate(ThompsonBox, Point, Quaternion.identity);
                break;
            case 1:
                obj = Instantiate(WinchesterBox, Point, Quaternion.identity);
                break;
            case 2:
                obj = Instantiate(SpeedBox, Point, Quaternion.identity);
                break;
            case 3:
                obj = Instantiate(HealthBox, Point, Quaternion.identity);
                break;
            default:
                obj = Instantiate(HealthBox, Point, Quaternion.identity);
                break;
        }     
        spawnedObjects.Add(obj);
    }

    private void Update()
    {
        
    }
}
