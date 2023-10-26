using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnerTarget : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject dummyTarget;
    [SerializeField] Transform dummySpawner;

    private GameObject target;

    public float xOffset;

    private List<GameObject> targets = new List<GameObject>();

    public List<GameObject> targetsAlive = new List<GameObject>();

    public List<GameObject> targetsAliveCopy = new List<GameObject>();

    private List<int> intList = new List<int>();

    public static SpawnerTarget instance;

    Target[] targetScript;

    bool checkAlive = false;

    public int SpawnCount;
    SpawnManager spawnManager;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

        for (int i = 0; i < SpawnCount; i++)
        {

            float xOffsetSetter = xOffset * i;

            Vector3 spawnDirection = dummySpawner.right;

            Vector3 spawnPosition = dummySpawner.position + spawnDirection * xOffsetSetter;

            // Vector3 spawnPosition = new Vector3(dummySpawner.position.x + xOffsetSetter, dummySpawner.position.y, dummySpawner.position.z);

            target = Instantiate(dummyTarget, spawnPosition, dummySpawner.rotation);

            targets.Add(target);

        }
        //StartCoroutine(CallGiveHealth());
    }

    // Update is called once per frame
    void Update()
    {

       if (Input.GetKeyDown(KeyCode.Q))
        {

            //StartCoroutine(GiveHealth());
          
        }

        if (spawnManager.gameStart)
        {
            foreach (GameObject item in targetsAliveCopy)
            {
                Target target = item.GetComponent<Target>();

                if (!target.alive && target.health <= 0)
                {
                    RemoveTargetAtList(target.gameObject);
                }
                if (target.alive)
                {
                    checkAlive = true;
                }
                else
                {
                    checkAlive = false;
                }
            }
        }
        //Debug.Log(targetsAlive.Count);
    }
    public IEnumerator CallGiveHealth()
    {
        
        yield return new WaitForSeconds(1f);

        GiveHealth();

        yield return new WaitForSeconds(0.1f);

        intList.Clear();
    }

    public void GiveHealth()
    {
        if (targets.Count == 0)
        {
            Debug.LogWarning("No GameObjects in the 'targets' list to access.");
            return;
        }

        intList.Clear();

        for (int i = 0; i < Mathf.Min(3, targets.Count); i++)
        {
            int range;
            do
            {
                range = Random.Range(0, targets.Count);
            } while (intList.Contains(range));

            intList.Add(range);

            Target target = targets[range].GetComponent<Target>();

            if (target != null)
            {
                target.setHealth(10);
              
                //target.setSpeed();
                AddTargetToList(target.gameObject);
            }
            else
            {
             
                Debug.LogWarning("Target script not found on the selected GameObject.");
            }
        }
    }

    public void RemoveTargetAtList(GameObject gameObject)
    {

        targetsAlive.Remove(gameObject);
      
    }
    public void RemoveAll()
    {

        targetsAlive.Clear();
        targetsAliveCopy.Clear();
    }
    public void AddTargetToList(GameObject targetObject)
    {
    
        targetsAlive.Add(targetObject);
        targetsAliveCopy.Add(targetObject);
    }

   
}
