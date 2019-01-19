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
    public int fCount = 0, dFloorChangeIteration=50;
    public static int score = 0, highestScore, collectibleCount = 0, deathFloorSpawnCount = 0;
    
    void Start()
    {
        panelAC.SetBool("PanelVisible",false);
        score = 0;
        scoreText.text = score.ToString();
        highestScore = SaveSystem.GetInt("HighScore");
        floors.Add(GameObject.FindGameObjectWithTag("Floor"));
        InvokeRepeating("SpawnFloor", 0f, floorSpawnTime);
        //InvokeRepeating("SpawnDeathFloor", 0f, floorSpawnTime);
        InvokeRepeating("DestroyFloors", 20f, 1f);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Debug.Log("in unpause game");
        Time.timeScale = 1f;
    }

    public void ShowMenu()
    {
        Time.timeScale = 1f;
        panelAC.SetBool("PanelVisible", true);
        Invoke("LoadScene", 0f);
    }


    public void DisableCameraMovement()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = false;
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
        Invoke("LoadScene", 1.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SpawnDeathFloor()
    {
        
        Transform dFloorT = deathFloors[deathFloors.Count - 1].transform;
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 25, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 50, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 75, dFloorT.position.y, dFloorT.position.z), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x, dFloorT.position.y, dFloorT.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x - 25, dFloorT.position.y, dFloorT.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        GameObject dFloorAnchor = Instantiate(deathFloorPrefab, new Vector3(dFloorT.position.x + 25, dFloorT.position.y, dFloorT.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
        deathFloors.Add(dFloorAnchor);
        dFloorAnchor.name = dFloorAnchor.name + " anchor";

    }

    void SpawnFloor()
    {
        floorPrefab = floors[floors.Count - 1];
        if (collectibleCount > 0)
            Instantiate(collectiblePrefab, floorPrefab.transform);
        collectibleCount++;
        bool isLeft = floorPrefab.GetComponent<FloorBehaviour>().isLeft;
        GameObject floorPf = isLeft ? rFloorPrefabs[Random.Range(0, rFloorPrefabs.Count)] : lFloorPrefabs[Random.Range(0, lFloorPrefabs.Count)];
        Transform pos = floorPrefab.GetComponent<FloorBehaviour>().spawnPoint;
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

        Transform nearestDFloor = FindNearestDeathFloor(newFloor.transform, 5);
        if (nearestDFloor.position.x < newFloor.transform.position.x && nearestDFloor.position.z < newFloor.transform.position.z)
        {
            deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(nearestDFloor.position.x, nearestDFloor.position.y, nearestDFloor.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
            deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(nearestDFloor.position.x + 25, nearestDFloor.position.y, nearestDFloor.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
            deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(nearestDFloor.position.x + 50, nearestDFloor.position.y, nearestDFloor.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
            deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(nearestDFloor.position.x + 75, nearestDFloor.position.y, nearestDFloor.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
            deathFloors.Add(Instantiate(deathFloorPrefab, new Vector3(nearestDFloor.position.x - 25, nearestDFloor.position.y, nearestDFloor.position.z + 25), deathFloorPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f)));
        }
    }

    Transform FindNearestDeathFloor(Transform floorTransform,int iterations)
    {
        Transform nearestDFloor = deathFloors[deathFloors.Count - 1].transform;
        float leastDist = 0;
        leastDist = Vector3.Distance(floorTransform.position, deathFloors[deathFloors.Count - 1].transform.position);
        for (int i = 2; i <= iterations; i++)
        {
            if (leastDist > Vector3.Distance(floorTransform.position, deathFloors[deathFloors.Count - i].transform.position))
            {
                leastDist = Vector3.Distance(floorTransform.position, deathFloors[deathFloors.Count - i].transform.position);
                nearestDFloor = deathFloors[deathFloors.Count - i].transform;
            }
        }
        return nearestDFloor;
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
