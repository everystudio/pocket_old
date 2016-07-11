using UnityEngine;
using System.Collections;

public class MapData : CsvKvs {

	public readonly string WIDTH = "map_width";
	public readonly string HEIGHT = "map_height";

	public int GetWidth(){
		return ReadInt (WIDTH);
	}

	public int GetHeight(){
		return ReadInt (HEIGHT);
	}


}
