using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines a spawner for two objects: click and slider
public class Spawner : MonoBehaviour
{
    private static Spawner _instance;

    [System.Serializable]
    public class Pool
    {
        public int maxObjects;
        public GameObject gameObject;
    }

    public Pool click;
    public Pool slider;

    private Queue<GameObject> clickQueue;
    private Queue<GameObject> sliderQueue;

    private Dictionary<int, GameObject> sliderCurrentlyVisible;
    private Dictionary<int, GameObject> clickCurrentlyVisible;

    #region Singleton
    public static Spawner Instance { get { return _instance; } }

    public void init(float radius, float approach)
    {
        clickQueue = new Queue<GameObject>();
        sliderQueue = new Queue<GameObject>();
        initQueue(clickQueue, click.maxObjects, click.gameObject, radius, approach);
        initQueue(sliderQueue, click.maxObjects, slider.gameObject, radius, approach);
        sliderCurrentlyVisible = new Dictionary<int, GameObject>();
        clickCurrentlyVisible = new Dictionary<int, GameObject>();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion Singleton

    #region private


    private void initQueue(Queue<GameObject> queue, int max, GameObject prefab, float radius, float approach)
    {

        for (int i = 0; i < max; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.localScale = new Vector3(radius, radius);
            onEnable objAnim = obj.GetComponentInChildren<onEnable>();
            if (objAnim != null)
            {

                obj.SetActive(false);
                objAnim.setSpeed(1.0f / approach);
            }
            queue.Enqueue(obj);
        }
    }


    private GameObject spawn(Queue<GameObject> from, Vector2 pos, int id)
    {
        GameObject toSpawn = from.Dequeue();

        toSpawn.SetActive(true);
        toSpawn.transform.position = pos;
        toSpawn.transform.rotation = Quaternion.identity;

        // enqueue from pool
        from.Enqueue(toSpawn);

        return toSpawn;
    }

    // simply deactivate and remove from mapping, spawn handles the queues
    private void delete(int id, Dictionary<int, GameObject> dict)
    {
        GameObject obj;
        dict.TryGetValue(id, out obj);
        if (obj != null)
        {
            obj.SetActive(false);
            dict.Remove(id);
        }
    }

    #endregion


    public void deleteClick(int id)
    {
        Debug.Log("deleting click with id: " + id);
        this.delete(id, clickCurrentlyVisible);
    }

    [System.Obsolete]
    public void deleteSlider(int id)
    {
        Debug.Log("deleting slider with id: " + id);
        this.delete(id, sliderCurrentlyVisible);
    }

    public void spawnClick(Vector2 pos, int id)
    {

        if (!clickCurrentlyVisible.ContainsKey(id))
        {
            Debug.Log("spawning new click with id: " + id);
            GameObject click = this.spawn(this.clickQueue, pos, id);
            clickCurrentlyVisible.Add(id, click);
        }
    }

    [System.Obsolete]
    public void spawnSlider(Vector2 pos, int id)
    {
        if (!sliderCurrentlyVisible.ContainsKey(id))
        {
            Debug.Log("spawning new slider with id: " + id);
            GameObject slider = this.spawn(this.sliderQueue, pos, id);
            sliderCurrentlyVisible.Add(id, slider);
        }

    }


}