using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<GameObject> lFloorPrefabs;
    public List<GameObject> rFloorPrefabs;
    public List<GameObject> floors;
    public List<GameObject> deathFloors;
    public GameObject floorPrefab,deathFloorPrefab;
    public GameObject collectiblePrefab;
    public Text scoreText;
    public Animator scoreTextAC, panelAC;
    public float floorSpawnTime = 1f;
    int fCount = 0;
    public static int score=0,highestScore;
    
    void Start()
    {
        panelAC.SetBool("PanelVisible",false);
        score = 0;
        scoreText.text = score.ToString();
        highestScore = SaveSystem.GetInt("HighScore");
        floors.Add(GameObject.FindGameObjectWithTag("Floor"));
        InvokeRepeating("SpawnFloor", 0f, floorSpawnTime);
        InvokeRepeating("SpawnDeathFloor", 0f, floorSpawnTime);
        InvokeRepeating("DestroyFloors", 15f, 0.7f);
    }

    public void GameOver()
    {
        CancelInvoke();
        if (score > highestScore)
        {
            SaveSystem.SetInt("HighScore", score);
            SaveSystem.SaveToDisk();
        }
        panelAC.SetBool("PanelVisible", true);
        SceneManager.LoadScene("MainMenu");
    }

    void SpawnDeathFloor()
    {
        Transform dFloorT = deathFloors[deathFloors.Count-1].transform;
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 25, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 50, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 75, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x, dFloorT.position.y, dFloorT.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x-25, dFloorT.position.y, dFloorT.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        GameObject dFloorAnchor = Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 25, dFloorT.position.y, dFloorT.position.z+25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
        deathFloors.Add(dFloorAnchor);
        dFloorAnchor.name = dFloorAnchor.name + " anchor";
        //Debug.Log(dFloorAnchor.name + " spawned");
    }

    void SpawnFloor()
    {
        floorPrefab = floors[floors.Count-1];
        Instantiate(collectiblePrefab, floorPrefab.transform);
        bool isLeft=floorPrefab.GetComponent<FloorBehaviour>().isLeft;
        GameObject floorPf= isLeft? rFloorPrefabs[Random.Range(0, rFloorPrefabs.Count)]: lFloorPrefabs[Random.Range(0, lFloorPrefabs.Count)];
        Transform pos = floorPrefab.GetComponent<FloorBehaviour>().spawnPoint ;
        //Debug.Log(floorPf.name+"at pos=" + pos.position.x+"," + pos.position.y + "," + pos.position.z );
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

    void DestroyFloors()
    {
        Destroy(floors[fCount]);
        Destroy(deathFloors[fCount]);
        fCount++;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score > highestScore)
        {
            scoreTextAC.SetTrigger("HighlightScore");
        }
    }
}
