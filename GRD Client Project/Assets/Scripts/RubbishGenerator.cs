using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RubbishGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> _containers = new List<Sprite>();
    [SerializeField] private List<RubbishTypeAndSprites> _rubbishTypesAndSprites = new List<RubbishTypeAndSprites>();

    public void GeneratePlasticObjects(Difficulty difficulty, GameObject slotsParent)
    {
        int myDifficulty = 0;

        // search for the given difficulty in the list of _rubbishTypesAndSprites
        for (int i = 0; i < _rubbishTypesAndSprites.Count; i++)
        {
            if (_rubbishTypesAndSprites[i].difficulty == difficulty) // 0 - easy, 1 - normal, 2 - hard
            {
                myDifficulty = i;
            }
        }

        for (int i = 0; i < slotsParent.transform.childCount; i++)
        {
            var randomContainer = Random.Range(0, _containers.Count);
            slotsParent.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = _containers[randomContainer];
            int randomSprite = 0;
            int typeIndex = 0;


            if (myDifficulty == 0 || myDifficulty == 1)
            {
                typeIndex = Random.Range(0, 3); // randomise the TYPE of recyclable
            }

            if (myDifficulty == 2)
            {
                typeIndex = Random.Range(0, 5); // randomise the TYPE of recyclable, hard difficulty has more options
            }


            switch (typeIndex)
            {
                case 0: // 0 TYPE plastic

                    // randomise the sprite of TYPE
                    randomSprite = Random.Range(0, _rubbishTypesAndSprites[myDifficulty].plasticSprites.Count);
                    slotsParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = _rubbishTypesAndSprites[myDifficulty].plasticSprites[randomSprite];
                    slotsParent.transform.GetChild(i).GetComponent<Rubbish>().type = rubbishTypes.plastic;
                    break;

                case 1: // 1 TYPE compostable

                    // randomise the sprite of TYPE
                    randomSprite = Random.Range(0, _rubbishTypesAndSprites[myDifficulty].compostableSprites.Count);
                    slotsParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = _rubbishTypesAndSprites[myDifficulty].compostableSprites[randomSprite];
                    slotsParent.transform.GetChild(i).GetComponent<Rubbish>().type = rubbishTypes.industrialCompostable;

                    break;

                case 2: // 2 TYPE landfill

                    // randomise the sprite of TYPE
                    randomSprite = Random.Range(0, _rubbishTypesAndSprites[myDifficulty].landfillSprites.Count);
                    slotsParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = _rubbishTypesAndSprites[myDifficulty].landfillSprites[randomSprite];
                    slotsParent.transform.GetChild(i).GetComponent<Rubbish>().type = rubbishTypes.landfill;

                    break;
                case 3: // 3 TYPE likely recycled

                    // randomise the sprite of TYPE
                    randomSprite = Random.Range(0, _rubbishTypesAndSprites[myDifficulty].likelyRecycled.Count);
                    slotsParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = _rubbishTypesAndSprites[myDifficulty].likelyRecycled[randomSprite];
                    slotsParent.transform.GetChild(i).GetComponent<Rubbish>().type = rubbishTypes.likelyRecycled;

                    break;
                case 4: // 2 TYPE not likely recycled

                    // randomise the sprite of TYPE
                    randomSprite = Random.Range(0, _rubbishTypesAndSprites[myDifficulty].notLikelyRecycled.Count);
                    slotsParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = _rubbishTypesAndSprites[myDifficulty].notLikelyRecycled[randomSprite];
                    slotsParent.transform.GetChild(i).GetComponent<Rubbish>().type = rubbishTypes.notLikelyRecycled;

                    break;
            }
        }
    }
}

[Serializable]
class RubbishTypeAndSprites
{
    public Difficulty difficulty;

    public List<Sprite> plasticSprites = new List<Sprite>();
    public List<Sprite> compostableSprites = new List<Sprite>();
    public List<Sprite> landfillSprites = new List<Sprite>();
    public List<Sprite> likelyRecycled = new List<Sprite>();
    public List<Sprite> notLikelyRecycled = new List<Sprite>();
}