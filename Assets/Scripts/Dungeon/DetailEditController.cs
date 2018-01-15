using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dungeon;

public class DetailEditController : MonoBehaviour
{

    private DungeonGenerator dungeonGen;

    // All the children components;
    private Transform detailPanel;
    private InputField seedField;




    void Awake()
    {
        dungeonGen = GameObject.FindWithTag("DungeonGenerator").GetComponent<DungeonGenerator>();
        seedField = GetComponentInChildren<InputField>();
        detailPanel = transform.Find("Details Panel");
        detailPanel.gameObject.SetActive(false);

        EnableDetails();
    }

    void EnableDetails()
    {
        detailPanel.gameObject.SetActive(true);

    }

    // Use this for initialization
    void Start()
    {
		// Set the ranges of things here based on the dungeon generator?
		seedField.text = dungeonGen.seed.ToString();
    }


    public void SetSeed()
    {
        Debug.Log("setting seed");
        dungeonGen.seed = int.Parse(seedField.text);
    }

    public void RandomiseSeed()
    {
        var rand = new System.Random();
        int seed = rand.Next();
        seedField.text = seed.ToString();
        SetSeed();
    }
}
