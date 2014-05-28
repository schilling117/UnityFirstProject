using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {
	public GameObject wall; 
	public int maxRows = 20;
	public int maxCols = 20;
	public int mazeScale = 5;
	private bool[,] grid;
	private GameObject mazeParent;
	private System.Random rnd;
	enum Directions {UP, DOWN, LEFT, RIGHT, NONE};

	//public Vector3 floorScale = new Vector3(mazeScale,0,mazeScale);
	//public Vector3 floorLoc = new Vector3(0,0,0);

	private class Cell {
		int row;
		int col;
		public Cell(){
			row = 0; 
			col = 0;
		}
		public Cell( int r, int c){
			row = r;
			col = c;
		}
		public int Row {
			get {
				return row;
			}
		}
		public int Column {
			get {
				return col;
			}
			set {
				col = value;
			}
		}

	}
	// Use this for initialization

	void Start () {
		rnd = new System.Random ();
		grid = new bool[maxRows , maxCols]; // ground true, wall is false
		mazeParent = new GameObject();
		mazeParent.name = "WallContainer";
		generateMaze ();
		printMaze ();
		scaleFloor ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void scaleFloor() {
		transform.localScale = new Vector3 ((float)mazeScale*(float)maxRows/10f, 1, (float)mazeScale*(float)maxCols/10f); // was floorScale
		transform.position = new Vector3 ((float)mazeScale*((float)maxRows/2.0f-0.5f),0f,(float)mazeScale*((float)maxCols/2.0f-0.5f)); // was floorLoc

	}

	void printMaze(){
		for (int n = -1; n <= maxRows; n++) { // x cols  z rows
			for(int p = -1; p <= maxCols; p++){
				if (n == -1 || n == maxRows || p == -1 || p == maxCols) {
					GameObject w = Instantiate(wall) as GameObject;
					w.transform.position = new Vector3(n*mazeScale,0,p*mazeScale);
					w.transform.parent = mazeParent.transform;
					w.transform.localScale = new Vector3(mazeScale,1,mazeScale);
				} else if(!grid[n,p]){
					GameObject w = Instantiate(wall) as GameObject;
					w.transform.position = new Vector3(n*mazeScale,0,p*mazeScale);
					w.transform.parent = mazeParent.transform;
					w.transform.localScale = new Vector3 (mazeScale,1,mazeScale);
				}
			}		
		}

	}

	void generateMaze(){
		Stack<Cell> floorList = new Stack<Cell>();
		Cell start = new Cell ();
		floorList.Push (start);

		while (floorList.Count > 0) {
			Cell c = floorList.Pop();
			if (isValidCell(c)) {
				grid[c.Row,c.Column] = true;
				selectDirection (floorList, c);
			}
		}
	}

	void selectDirection (Stack<Cell> cellList, Cell c) {
		List<Directions> directionList = new List<Directions> ();
		directionList.Add (Directions.UP); 
		directionList.Add (Directions.DOWN);
		directionList.Add (Directions.LEFT);
		directionList.Add (Directions.RIGHT);

		while (directionList.Count > 0) {
			int selDir = rnd.Next(0, directionList.Count);
			Directions sD = directionList[selDir];
			directionList.RemoveAt(selDir);
			//remove selection from list
			switch (sD) {
				case Directions.UP:
				Cell up = new Cell(Mathf.Max (0,c.Row-1),c.Column);
				if (isValidCell (up))
					cellList.Push (up);
					break;
				case Directions.DOWN:
				Cell down = new Cell(Mathf.Min (maxRows-1,c.Row+1),c.Column);
				if (isValidCell (down))
					cellList.Push (down);
					break;
				case Directions.LEFT:
				Cell left = new Cell(c.Row,Mathf.Max (0,c.Column-1));
				if (isValidCell (left))
					cellList.Push (left);
				break;
				case Directions.RIGHT:
				Cell right = new Cell(c.Row, Mathf.Min (maxCols-1,c.Column+1));
				if (isValidCell (right))
					cellList.Push (right);
				break;
			}
		}
	}

	bool isValidCell (Cell c) {
		int count = 0;
		if (grid [Mathf.Max (0,c.Row - 1), c.Column])
			count++;
		if (grid [Mathf.Min (maxRows-1,c.Row + 1), c.Column])
			count++;
		if (grid [c.Row, Mathf.Max (0,c.Column - 1)])
			count++;
		if (grid [c.Row, Mathf.Min (maxCols-1,c.Column + 1)])
			count++;
		if (count <= 1 && !grid [c.Row, c.Column])
			return true;
		else
			return false;
	}
}
