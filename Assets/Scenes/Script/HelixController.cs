using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 StartRotation;
    private Vector3 lastPos;

    public Transform toptransform;
    public Transform goaltransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();

    private float helixDistance;
    private List<GameObject> SpawnedLevels = new List<GameObject>();
    
    void Awake()
    {
        StartRotation = transform.localEulerAngles;
        helixDistance = toptransform.localPosition.y - (goaltransform.localPosition.y + 0.1f);
        LoadStage(FindObjectOfType<GameManager>().CurrentStage);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 CurPos = Input.mousePosition;
            if (lastPos == Vector3.zero)//Only tap
            {
                lastPos = CurPos;
            }
            //Swipe method
            float delta = lastPos.x - CurPos.x;//swipe changes of finger position
            lastPos = CurPos;                   //Updates position

            transform.Rotate(Vector3.up * delta);//Rotation of helix

        }
        if (Input.GetMouseButtonUp(0))//Finger release
        {
            lastPos = Vector3.zero;     //Set to no change in position of finger.So that you can againg swipe from
                                        //any point on screen
        }
    }
    public void LoadStage(int stageNo)
    {
        Stage stage = allStages[Mathf.Clamp(stageNo, 0, allStages.Count - 1)];//Checks in which stage we are playing
        if (stage == null)
        {
            Debug.LogError("No stage " + stageNo + ", is in the Stages List.");
            return;
        }
        Camera.main.backgroundColor = allStages[stageNo].BackGroundColor;
        FindObjectOfType<BallController>().GetComponent<MeshRenderer>().material.color = allStages[stageNo].StageBallColor;

        transform.localEulerAngles = StartRotation;//Reset  Helix position

        foreach (GameObject GO in SpawnedLevels)
            Destroy(GO);
        //create a new Level/ platform
        float levelDistance = helixDistance / stage.Levels.Count;//Find What should be distance between HelixParts
        float SpawnPosY = toptransform.position.y;//total distance like 120 in y position.
        for (int i = 1; i < stage.Levels.Count; i++)
        {                             //-----Level Instantiating Method-------
            SpawnPosY -= levelDistance;//SpawnPosY takes the value where to instantiate
            GameObject Level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Levels Spawnwed.");
            Level.transform.position = new Vector3(0, SpawnPosY, 0);//Level will be spawned at SpawnPosY
            SpawnedLevels.Add(Level);//Add is used to add something in List.
                                     //-----------------------------------------
                                     //Random Parts of Level will be Deactivated
            int partsToDisable = 12 - stage.Levels[i].PartCount;
            List<GameObject> disabledParts = new List<GameObject>();
            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = Level.transform.GetChild(Random.Range(0, Level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);  
                }
            }// -------------------------

            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform t in Level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNo].StageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            //------------Death Part-------------
            List<GameObject> deathparts = new List<GameObject>();
            while(deathparts.Count < stage.Levels[i].DeathPartCount ) {
                GameObject randomParts = leftParts[Random.Range(0, leftParts.Count)];
                if (!deathparts.Contains(randomParts))
                {
                    
                       
                    randomParts.gameObject .AddComponent<DeathPart>(); 
                        
                        deathparts.Add(randomParts);
                    
                }
            
            }

            
        }

    }
}
