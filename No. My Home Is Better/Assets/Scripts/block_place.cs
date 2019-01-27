﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_place : MonoBehaviour
{
	/**Slot represents a block slot in the grid where a block could go.
	 * isEmpty - true if there's no physical block placed in this slot
	 * blockLen - the length of any side of a block in world coordinates
	 * gridPos - this block's position in grid coordinates
	 * worldCenter - this block's center in world coordinates
	 */
	private class Slot{
		public bool isEmpty;
		public static float blockLen;//must be set before instantiating a block
		public Vector2 gridPos;
		public Vector3 worldCenter;
		private Slot[,] blockGrid;
		private Rect gridWorldSize;
		public GameObject block;
		
		public Slot(Vector2 gridPos, Slot[,] blockGrid, Rect gridWorldSize) {
			isEmpty = true;
			this.gridPos = gridPos;
			this.blockGrid = blockGrid;
			this.gridWorldSize = gridWorldSize;
			worldCenter = new Vector3((gridPos[0] + 0.5f)*Slot.blockLen + gridWorldSize.x,
					(gridPos[1] + 0.5f)*Slot.blockLen + gridWorldSize.y, 0);
			Debug.Log("gridPos = " + gridPos + ", worldCenter = " + worldCenter);
			block = null;
		}
		
		///Returns true if a block could be placed in this slot
		public bool CheckValidity() {
			if (gridPos.y != 0) {
				Slot below = blockGrid[(int)gridPos.x, (int)gridPos.y-1];
				return isEmpty && !below.isEmpty;
			} else {
				return isEmpty;
			}
		}
	}
	
	
	public GameObject block;//A reference to the base block (should be a prefab eventually). MUST BE 1x1x1 SCALE.
	public Camera cam;
	public Rect gridWorldSize;//The size of the grid in world coordinates. MUST HAVE INTEGER WIDTH AND HEIGHT
	public Vector3 DNE;//A non-existent point; DO NOT MODIFY
	
	private Slot[,] blockGrid;
	private int gridWidth;
	private int gridHeight;
	
	
    /**Start is called before the first frame update
     * Initializes the grid and other relevant variables.
     */
    void Start() {
		DNE =  new Vector3(Mathf.Pow(10,10),Mathf.Pow(10,10),Mathf.Pow(10,10));
		Slot.blockLen = block.transform.localScale.x;//Make sure the block's scale is EXACTLY 1!
		gridWidth = (int)gridWorldSize.width;
		gridHeight = (int)gridWorldSize.height;
		
		blockGrid = new Slot[gridWidth,gridHeight];
		for(int i=0; i<gridWidth; ++i) {
			for(int j=0; j<gridHeight; ++j) {
				blockGrid[i,j] = new Slot(new Vector2(i,j),blockGrid, gridWorldSize );
			}
		}
    }


    /// Update is called once per frame.
    void Update() {
		Debug.DrawLine(gridWorldSize.min,gridWorldSize.max);
		if(Input.GetMouseButtonDown(0)) {
			//Places a block in the grid
			Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
			mPos[2] = 0;
			List<Vector3> poss = GetValidSlotsLR(mPos);
			if (poss != null) {
				for (int i=0; i<poss.Count; ++i) {
					Vector3 pos = PlaceBlock(poss[i], null);
					if(pos != DNE) {
						GameObject.Instantiate(block,pos, Quaternion.identity);
					}
				}
			}
		}
    }
    
	
	///Custom methods
	/**PlaceBlock places a block in the grid at a position if that position's grid slot is empty by handling all grid logic.
	 * NOTE: Does not create the actual block GameObject
	 * @param pos - the position to place a block at
	 * @return the center of the slot to actually spawn the block object at
	 */
	public Vector3 PlaceBlock(Vector3 pos, GameObject block) {
		Vector3 slotCenter = DNE;//if block is empty (or doesn't exist), returns (-1,-1,-1)
		if(gridWorldSize.Contains(pos)) {
			Slot slot = GetSlotContaining(pos);
			if (slot != null && slot.CheckValidity()) {
				slot.isEmpty = false;
				slotCenter = slot.worldCenter;
				slot.block = block;
			}
		}
		return slotCenter;
	}
	
	
    /**GetValidSlotsLR returns the world-coordinate centers of the unique (or non-existent) valid block to the left and right of the given position.
     * @param pos - the position to find blocks adjacent to
     * @return a list of world-coordinate centers of all empty blocks adjacent to the blocking containing @param pos. or NULL if block is null
     */
    public List<Vector3> GetValidSlotsLR(Vector3 pos) {
		Slot block = GetSlotContaining(pos);
		if(block == null) {
			return null;
		}
		//Finding adjacent empty blocks
		List<Slot> adjacents = GetAdjacentSlots(block);
		if (block.gridPos.y != 0) {
			adjacents.Remove(blockGrid[(int)block.gridPos.x,(int)block.gridPos.y-1]);//Removing the one below
		} else if (block.gridPos.y != gridHeight - 1) {
			adjacents.Remove(blockGrid[(int)block.gridPos.x,(int)block.gridPos.y+1]);//Removing the one above
		}
		for(int i=0; i<adjacents.Count; ++i) {
			if(!adjacents[i].CheckValidity()) {
				adjacents.RemoveAt(i);
			}
		}
		
		List<Vector3> adjPoss = new List<Vector3>();
		for(int i = 0; i<adjacents.Count; ++i) {
			adjPoss.Add(adjacents[i].worldCenter);
		}
		return adjPoss;
	}
	
	
	public GameObject GetGameObjectAt(Vector3 pos) {
		Slot slot = GetSlotContaining(pos);
		return slot.block;
	}
	
	
	/**GetSlotContaining returns the Slot object that contains the given position.
	 * @param pos - the position to find the slot for
	 * @return the Slot object containing @param pos, or null if no such slot exists
	 */
	private Slot GetSlotContaining(Vector3 pos) {
		Slot slot = null;
		int gridX = (int)Mathf.Floor((pos[0]-gridWorldSize.x)/Slot.blockLen);
		int gridY = (int)Mathf.Floor((pos[1]-gridWorldSize.y)/Slot.blockLen);
		
		Debug.Log(gridX + "," + gridY);
		
		if (IsInGrid(new Vector2(gridX, gridY))) {
			slot = blockGrid[gridX,gridY];
		}
		
		return slot;
	}
	
	
	private bool IsInGrid(Vector2 gridPos) {
		if(gridPos.x >= 0 && gridPos.x < gridWidth && gridPos.y >= 0 && gridPos.y < gridHeight) {
			return true;
		}
		return false;
	}
	
	
	/**GetAdjacentSlots returns all grid slots adjacent to the given one.
	 * @param slot - the slot to find adajacent slots for
	 * @return a list of adjacent Slot objects
	 */
	private List<Slot> GetAdjacentSlots(Slot slot) {
		Vector2 ind = slot.gridPos;
		List<Slot> adjacents = new List<Slot>();
		for(int i=-1; i<2; ++i) {
			for(int j=-1; j<2; ++j) {
				if(!(j==0 && i==0)) {
					int adjX = (int)ind[0] + i;
					int adjY = (int)ind[1] + j;
					if(IsInGrid(new Vector2(adjX,adjY))) {
						adjacents.Add(blockGrid[(int)ind[0] + i, (int)ind[1] + j]);
					}
				}
			}
		}
		return adjacents;
	}	
}
