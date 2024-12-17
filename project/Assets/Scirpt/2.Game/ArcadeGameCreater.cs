using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeGameCreater : MonoBehaviour
{
    public static ArcadeGameCreater Instance;

    private CarController carController;

    [SerializeField] private List<GameObject> Lane3Tiles;
    public Vector3 TilesCoordinat = new Vector3(0f, 0f, 0f);
    [SerializeField] private List<GameObject> oldTiles;



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

    void Start()
    {
        carController = CarController.Instance;

        CreateTiles(20);

    }

    private void CreateTiles(int Lenght)
    {
        for (int i = 0; i < Lenght; i++)
        {
            int k = Random.Range(0, Lane3Tiles.Count);
            GameObject tile = Instantiate(Lane3Tiles[k], TilesCoordinat, Quaternion.identity);
            oldTiles.Add(tile);
            TilesCoordinat.z = TilesCoordinat.z + 50f;
        }
    }

    void Update()
    {
        if(carController.transform.position.z > TilesCoordinat.z - 500)
        {
            oldTiles.RemoveRange(0, 10);
            CreateTiles(10);
        }
    }
}
