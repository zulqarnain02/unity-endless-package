using UnityEngine;
using System.Collections.Generic;
public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn=0;
    public float tileLenght=30;
    public int numberOfTiles=5;
    public Transform PlayerTransform;
    private List<GameObject> activeTiles=new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0;i<numberOfTiles;i++){
            if(i==0){
                SpawnTile(0);
            }else{
                SpawnTile(Random.Range(0,tilePrefabs.Length));
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerTransform.position.z - 35 >zSpawn-(numberOfTiles * tileLenght)){
            SpawnTile(Random.Range(0,tilePrefabs.Length));
            DeleteTile();

        }
    }

    public void SpawnTile(int tileIndex){

        GameObject go=Instantiate(tilePrefabs[tileIndex],transform.forward*zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn+=tileLenght;
    }

    public void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
