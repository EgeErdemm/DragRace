using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    public static Traffic Instance { get; private set; }

    private ArcadeGameCreater arcade;


    [SerializeField] private List<GameObject> cars;
    public float TrafficRate;
    private bool frontTraffic= true;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        arcade = ArcadeGameCreater.Instance;
    }


    public void MakeTraffic()
    {
        // x      (-25,-12)   (-12,1) (4,15) her bir serit
        int k = Random.Range(0, cars.Count);
        Vector3 pos = arcade.TilesCoordinat;
        float x = Random.Range(-25, 15);
        pos.x = x;
        pos.y = 2;
        if (!frontTraffic)
        {
            pos.z = pos.z * 0.5f;
            frontTraffic = !frontTraffic;
            Debug.Log(frontTraffic);
        }
       
        Instantiate(cars[k], pos, Quaternion.identity);
    }



    private void Update()
    {
        TrafficRate -= Time.deltaTime;
        if (TrafficRate <= 0)
        {
            MakeTraffic();
            TrafficRate = 1f;
        }

    }

}
