using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGrid {
	public int x;
	public int y;

	public MapGrid( int _iX , int _iY ){
		x = _iX;
		y = _iY;
		return;
	}
	public MapGrid(){
		x = 0;
		y = 0;
	}

	static  public bool Equals( Grid _a , Grid _b ){
		return (_a.x == _b.x && _a.y == _b.y);
	}
	public bool Equals( int _x , int _y ){
		if (x == _x && y == _y) {
			return true;
		} else {
			return false;
		}
	}

	static private void setUsingGrid( ref List<MapGrid> _gridList , int _iX , int _iY , int _iWidth , int _iHeight ){
		for (int x = 0; x < _iWidth; x++) {
			for (int y = 0; y < _iHeight; y++) {
				MapGrid grid = new MapGrid ( _iX + x, _iY + y);
				_gridList.Add (grid);
			}
		}
		return;
	}

	static public void SetUsingGrid( ref List<MapGrid> _gridList , DataItemParam _dataItem ){
		setUsingGrid (ref _gridList, _dataItem.x, _dataItem.y, _dataItem.width, _dataItem.height);
		return;
	}

	static public void SetUsingGrid( ref List<MapGrid> _gridList , List<DataItemParam> _dataItemList ){
		_gridList.Clear ();
		foreach (DataItemParam data in _dataItemList) {
			List<MapGrid> grid_list = new List<MapGrid> ();
			SetUsingGrid (ref grid_list, data);
			foreach (MapGrid grid in grid_list) {
				_gridList.Add (grid);
			}
		}
		return;
	}

	static public bool AbleSettingItem( CsvItemParam _dataItem , int _iX , int _iY , List<MapGrid> _gridList ){
		bool bRet = true;

		List<MapGrid> useGrid = new List<MapGrid> ();
		setUsingGrid (ref useGrid, _iX, _iY, _dataItem.size, _dataItem.size);

		foreach (MapGrid check_grid in useGrid) {
			if (check_grid.x < 0) {
				bRet = false;
			} else if (check_grid.y < 0) {
				bRet = false;
			} else if (DataManager.user.m_iWidth <= check_grid.x) {
				bRet = false;
			} else if (DataManager.user.m_iHeight <= check_grid.y) {
				bRet = false;
			} else {
			}
		}
		foreach (MapGrid check_grid in useGrid) {
			foreach (MapGrid field_grid in _gridList) {

				if (MapGrid.Equals (check_grid, field_grid) == true) {
					//Debug.Log ("you cant setting here!");
					return false;
				}
			}
		}
		//Debug.Log ("able set GOOD POSITION");
		return bRet;
	}


}
