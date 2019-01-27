using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_place : MonoBehaviour
{
	/**Slot represents a block slot in the grid where a block could go.
	 * isEmpty - true if there's no physical block placed in this block
	 * blockLen - the length of any side of a block in world coordinates
	 * gridPos - this block's position in grid coordinates
	 * worldCenter - this block's center in world coordinates
	 */
	private class Slot{
		public bool isEmpty;
		public static float blockLen;//must be set before instantiating a block
		public Vector2 gridPos;
		public Vector3 worldCenter;
		
		public Slot(Vector2 gridPos) {
			isEmpty = true;
			this.gridPos = gridPos;
			worldCenter = new Vector3((gridPos[0] + 0.5f)*Slot.blockLen, (gridPos[1] + 0.5f)*Slot.blockLen, 0);
		}
	}
	
	
	public GameObject block;//A reference to the base block (should be a prefab eventually)
	public Camera cam;
	public Vector3 DNE = new Vector3(Mathf.Infinity,Mathf.Infinity,Mathf.Infinity);//A non-existent point; DO NOT MODIFY
	
	private Slot[,] blockGrid;
	private const int gridLen = 10;//The length in blocks of one dimension of the block grid
	private Rect gridWorldSize;//The size of the grid in world coordinates
	
	
    /**Start is called before the first frame update
     * Initializes the grid and other relevant variables.
     */
    void Start() {
		Slot.blockLen = block.transform.localScale.x;//Make sure the block's scale is positive!
		gridWorldSize = new Rect(0,0,Slot.blockLen*gridLen, Slot.blockLen*gridLen);
		
		blockGrid = new Slot[gridLen,gridLen];
		for(int i=0; i<gridLen; ++i) {
			for(int j=0; j<gridLen; ++j) {
				blockGrid[i,j] = new Slot(new Vector2(i,j));
			}
		}
		
    }


    /// Update is called once per frame.
    void Update() {
		if(Input.GetMouseButtonDown(0)) {
			//Places a block in the grid
			Vector3 mPos = cam.ScreenToWorldPoint(Input.mousePosition);
			mPos[2] = 0;
			Vector3 pos = PlaceBlock(mPos);
			if(pos != DNE) {
				GameObject.Instantiate(block,pos, Quaternion.identity);
			}
			
			Debug.Log("mPos = " + mPos + ", pos = " + pos);
		}
    }
    
    
    
    ///Custom methods
    /**GetAdjacentOpenSlots returns the world-coordinate centers of all empty blocks adjacent to the block containing a give position.
     * @param pos - the position to find blocks adjacent to
     * @return a list of world-coordinate centers of all empty blocks adjacent to the blocking containing @param pos
     */
    public List<Vector3> GetAdjacentEmptySlots(Vector3 pos) {
		Slot block = GetSlotContaining(pos);
		
		//Finding adjacent empty blocks
		List<Slot> adjacents = GetAdjacentSlots(block);
		for(int i=0; i<adjacents.Count; ++i) {
			if(!adjacents[i].isEmpty) {
				adjacents.RemoveAt(i);
			}
		}
		
		List<Vector3> adjPoss = new List<Vector3>();
		for(int i = 0; i<adjacents.Count; ++i) {
			adjPoss.Add(adjacents[i].worldCenter);
		}
		return adjPoss;
	}
	
	/**PlaceBlock places a block in the grid at a position if that position's grid slot is empty by handling all grid logic.
	 * NOTE: Does not create the actual block GameObject
	 * @param pos - the position to place a block at
	 * @return the center of the slot to actually spawn the block object at
	 */
	public Vector3 PlaceBlock(Vector3 pos) {
		Vector3 slotCenter = DNE;//if  block is empty (or doesn't exist), returns (-1,-1,-1)
		if(gridWorldSize.Contains(pos)) {
			Slot slot = GetSlotContaining(pos);
			if (slot.isEmpty) {
				slot.isEmpty = false;
				slotCenter = slot.worldCenter;
			}
		}
		return slotCenter;
	}
	
	
	/**GetSlotContaining returns the Slot object that contains the given position.
	 * @param pos - the position to find the slot for
	 * @return the Slot object containing @param pos
	 */
	private Slot GetSlotContaining(Vector3 pos) {
		int gridX = (int)Mathf.Floor(pos[0]/Slot.blockLen);
		int gridY = (int)Mathf.Floor(pos[1]/Slot.blockLen);
		return blockGrid[gridX, gridY];
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
				adjacents.Add(blockGrid[(int)ind[0] + i, (int)ind[1] + j]);
			}
		}
		return adjacents;
	}
}
