using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dungeon;

public class SpawnPlayer : MonoBehaviour {

	public GameObject playerPrefab;
	public void Spawn(Room room)
	{
		var pos = room.transform.position + new Vector3(2, 0, 2);
        GameObject player;
        try
        {
            player = Instantiate(playerPrefab);
        }
        catch
        {
            // This line won't build to device. what?
            //player = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(playerPrefab);
        }
        

    }
}
