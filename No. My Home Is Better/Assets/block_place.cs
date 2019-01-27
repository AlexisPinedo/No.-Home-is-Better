using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_place : MonoBehaviour
{
	private class Block{
		public bool isEmpty;
		public static float blockLen;//must be set before instantiating a block
		public Vector2 index;
		public Vector3 center;
		
		public Block(Vector2 inDex) {
			isEmpty = true;
			index = inDex;
			center = new Vector3((index[0] + 0.5f)*Block.blockLen, (index[1] + 0.5f)*Block.blockLen, 0);
		}
	}
	
	
	public GameObject block;
	public Camera mainCamera;
	private Block[,] blockGrid;
	private int gridSize;
	private Rect gridSpace;
	public Vector3 DNE = new Vector3(-1f,-1f,-1f);
	
	
    // Start is called before the first frame update
    void Start() {
		Block.blockLen = block.transform.localScale.x;//Make sure the block's scale is positive!
		gridSize = 10;
		gridSpace = new Rect(0,0,Block.blockLen*gridSize, Block.blockLen*gridSize);
		
		Debug.Log(gridSpace);
		
		blockGrid = new Block[gridSize,gridSize];
		for(int i=0; i<gridSize; ++i) {
			for(int j=0; j<gridSize; ++j) {
				blockGrid[i,j] = new Block(new Vector2(i,j));
			}
		}
		
    }


    // Update is called once per frame
    void Update() {
       if(Input.GetMouseButtonDown(0)) {
			Vector3 mPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			mPos[2] = 0;
			Vector3 pos = PlaceBlock(mPos);
			if(pos != DNE) {
				GameObject.Instantiate(block,pos, Quaternion.identity);
			}
			
			Debug.Log("mPos = " + mPos + ", pos = " + pos);
	   }
    }
    
    
    
    ///Custom methods
    public List<Vector3> GetAdjacentOpenBlocks(Vector3 pos) {
		Block block = GetBlockContaining(pos);
		List<Block> adjacents = GetAdjacentBlocks(block);
		for(int i=0; i<adjacents.Count; ++i) {
			if(!adjacents[i].isEmpty) {
				adjacents.RemoveAt(i);
			}
		}
		
		List<Vector3> adjPoss = new List<Vector3>();
		for(int i = 0; i<adjacents.Count; ++i) {
			Block adj = adjacents[i];
			adjPoss.Add(adj.center);
			
		}
		return adjPoss;
	}
	
	
	public Vector3 PlaceBlock(Vector3 pos) {
		Vector3 blockCenter = DNE;//if  block is empty (or doesn't exist), returns (-1,-1,-1)
		if(gridSpace.Contains(pos)) {
			Debug.Log("WHOOOO!");
			Block block = GetBlockContaining(pos);
			if (block.isEmpty) {
				block.isEmpty = false;
				blockCenter = block.center;
			}
		}
		return blockCenter;
	}
	
	
	private Block GetBlockContaining(Vector3 pos) {
		int gridX = (int)Mathf.Floor(pos[0]/Block.blockLen);
		int gridY = (int)Mathf.Floor(pos[1]/Block.blockLen);
		return blockGrid[gridX, gridY];
	}
	
	
	private List<Block> GetAdjacentBlocks(Block block) {
		Vector2 ind = block.index;
		List<Block> adjacents = new List<Block>();
		for(int i=-1; i<2; ++i) {
			for(int j=-1; j<2; ++j) {
				adjacents.Add(blockGrid[(int)ind[0] + i, (int)ind[1] + j]);
			}
		}
		return adjacents;
	}
}
