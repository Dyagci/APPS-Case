using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public HashSet<GameObject> leftestPlanes;
    public HashSet<GameObject> rightestPlanes;
    public HashSet<GameObject> backestPlanes;
    public List<GameObject> allPlanes;
    public Player player;
    [SerializeField] float lowestX;
    [SerializeField] private float lowestZ;
    [SerializeField] private float highestX;
    public List<GameObject> rightest;
    public List<GameObject> leftest;
    // Start is called before the first frame update
    void Start()
    {
        leftestPlanes = new HashSet<GameObject>();
        rightestPlanes = new HashSet<GameObject>();
        backestPlanes = new HashSet<GameObject>();
        SortPlanes();
    }

    // Update is called once per frame
    void Update()
    {
        SortPlanes();
        if (Math.Abs(Math.Abs(player.transform.position.x) -Math.Abs(highestX))<200 && player.xDistanceTraveled<-100)
        {
            leftest.Clear();
            foreach (GameObject obj in leftestPlanes)
            {
                if (obj.transform.position.x == lowestX)
                {
                    Vector3 newPos = obj.transform.position;
                    newPos.x += 1200;
                    obj.transform.position = newPos; 
                    leftest.Add(obj);
                }
            }
            leftestPlanes.Clear();
            player.xDistanceTraveled = 0;
            player.startPos = player.transform.position;

        }
        else if (Math.Abs(Math.Abs(player.transform.position.x) -Math.Abs(lowestX))<200 && player.xDistanceTraveled>100)
        {
            rightest.Clear();
            foreach (GameObject obj in rightestPlanes)
            {
                if (obj.transform.position.x == highestX)
                {
                    Vector3 newPos = obj.transform.position;
                    newPos.x -= 1200;
                    obj.transform.position = newPos; 
                    rightest.Add(obj);
                }
                
            }
            rightestPlanes.Clear();
            player.xDistanceTraveled = 0;
            player.startPos = player.transform.position;

        }
        if (-player.zDistanceTraveled > 395 && player.zDistanceTraveled < 0)
        {
            foreach (GameObject obj in backestPlanes)
                {
                    Vector3 newPos = obj.transform.position;
                    newPos.z += 1200;
                    obj.transform.position = newPos;
                }
                backestPlanes.Clear();
                player.zDistanceTraveled = 0;
                player.startPos = player.transform.position;
        }
    }

    private void SortPlanes()
    {
        lowestX = allPlanes[0].transform.position.x;
        lowestZ = allPlanes[0].transform.position.z;
        highestX = allPlanes[0].transform.position.x;
        for (int i = 0; i < allPlanes.Count; i++)
        {
            if (allPlanes[i].transform.position.x < lowestX)
            {
                lowestX = allPlanes[i].transform.position.x;
            }
            if (allPlanes[i].transform.position.z < lowestZ)
            {
                lowestZ = allPlanes[i].transform.position.z;
            }
            if (allPlanes[i].transform.position.x >highestX)
            {
                highestX = allPlanes[i].transform.position.x;
            }
            
        }
        for (int i = 0; i < allPlanes.Count; i++)
        {
            if (allPlanes[i].transform.position.z == lowestZ)
            {
                backestPlanes.Add(allPlanes[i]);
            }
            else
            {
                
            }
            if (allPlanes[i].transform.position.x == lowestX)
            {
                leftestPlanes.Add(allPlanes[i]);
            }
            else if (allPlanes[i].transform.position.x == highestX)
            {
                rightestPlanes.Add(allPlanes[i]);
            }

        }
    }
}
