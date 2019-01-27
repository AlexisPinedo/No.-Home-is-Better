using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToControllerAssigner : MonoBehaviour
{
//    private List<int> assignedControllers = new List<int>();
//    private PlayerPanel[] playerPanels;
//
//    private void Awake()
//    {
//        //playerPanels = FindObjectOfType<PlayerPanel>().OrderBy(t => t.PlayerNumber).ToArray();
//        
//    }
//
//    private void Update()
//    {
//        for (int i = 1; i <= 6; i++)
//        {
//            if (assignedControllers.Contains(i))
//                continue;
//
//            if (Input.GetButton("J" + i + "Jump"))
//            {
//                AddPlayerController(i);
//                break;
//            }
//        }        
//    }
//
//    public Player AddPlayerController(int controller)
//    {
//        assignedControllers.Add(controller);
//        for (int i = 0; i < playerPanels.Length; i++)
//        {
////            if (playerPanels[i].HasControllerAssigned == false)
////            {
////                return playerPanels[i].AssignController(controller);
////            }
//        }
//        return null;
//    }
}
