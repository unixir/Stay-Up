using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<GameObject> lFloorPrefabs;
    public List<GameObject> rFloorPrefabs;
    public List<GameObject> floors;
    public GameObject floor;
    public GameObject collectiblePrefab;
    public Text scoreText;
    public float floorSpawnTime = 1f;
    int fCount = 0,score=0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(instance);

        score = 0;
        scoreText.text = score.ToString();
        floors.Add(GameObject.FindGameObjectWithTag("Floor"));
        InvokeRepeating("SpawnFloor", 0f, floorSpawnTime);
        InvokeRepeating("DestroyFloor", 20f, 0.7f);
    }

    void SpawnFloor()
    {
        floor = floors[floors.Count-1];
        Instantiate(collectiblePrefab, floor.transform);
        bool isLeft=floor.GetComponent<FloorBehaviour>().isLeft;
        GameObject floorPf= isLeft? rFloorPrefabs[Random.Range(0, rFloorPrefabs.Count)]: lFloorPrefabs[Random.Range(0, lFloorPrefabs.Count)];
        Transform pos = floor.GetComponent<FloorBehaviour>().spawnPoint ;
        Debug.Log(floorPf.name+"at pos=" + pos.position.x+"," + pos.position.y + "," + pos.position.z );
        GameObject newFloor;
        if (isLeft)
        {
            newFloor = Instantiate(floorPf, pos.position, Quaternion.Euler(0f, 0f, 0f));
        }
        else
        {
            newFloor = Instantiate(floorPf, pos.position, Quaternion.Euler(0f, 90f, 0f));
        }
        
        floors.Add(newFloor);
    }

    void DestroyFloor()
    {
        Destroy(floors[fCount]);
    }

    public void IncreaseScore()
    {
        score++;
        if(scoreText!=null)
        scoreText.text = score.ToString();
    }
}
