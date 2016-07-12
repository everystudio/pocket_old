using UnityEngine;
using System.Collections;

public class MapData : CsvKvs {

	public readonly string WIDTH = "map_width";
	public readonly string HEIGHT = "map_height";

	public readonly string DIR_XX = "dir_xx";
	public readonly string DIR_XY = "dir_xy";

	public readonly string DIR_YX = "dir_yx";
	public readonly string DIR_YY = "dir_yy";
	//static public readonly Vector3 CELL_Y_DIR = new Vector3 (-80.0f, 40.0f, 0.0f);

	private Vector3 m_v3CellXDir;
	public Vector3 CELL_X_DIR{
		get{
			if (m_v3CellXDir.magnitude == 0.0f ) {
				m_v3CellXDir = new Vector3 (
					ReadFloat (DIR_XX),
					ReadFloat (DIR_XY),
					0.0f);
			}
			return m_v3CellXDir;
		}
	}
	private Vector3 m_v3CellYDir;
	public Vector3 CELL_Y_DIR{
		get{
			if (m_v3CellYDir.magnitude == 0.0f ) {
				m_v3CellYDir = new Vector3 (
					ReadFloat (DIR_YX),
					ReadFloat (DIR_YY),
					0.0f);
			}
			return m_v3CellYDir;
		}
	}
	private float cell_x_length = 0.0f;
	private float cell_y_length = 0.0f;
	public float CELL_X_LENGTH {
		get{
			if (cell_x_length == 0.0f) {
				cell_x_length = CELL_X_DIR.magnitude;
			}
			return cell_x_length;
		}
	}
	public float CELL_Y_LENGTH {
		get{
			if (cell_y_length == 0.0f) {
				cell_y_length = CELL_Y_DIR.magnitude;
			}
			return cell_y_length;
		}
	}

	public int GetWidth(){
		return ReadInt (WIDTH);
	}

	public int GetHeight(){
		return ReadInt (HEIGHT);
	}

	public readonly int DEPTH_ROAD 		= 100;
	public readonly int DEPTH_ITEM 		= 500;
	public readonly int DEPTH_VISITOR 	= 500;
	public readonly int DEPTH_DUST 		= 1000;
	public readonly int DEPTH_MONSTER	= 1500;
	public readonly int DEPTH_MONSTER_FUKIDASHI	= 2000;


}
