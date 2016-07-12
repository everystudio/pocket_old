using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// テンプレートだと、Tからはじめるのがよくありますが、親のクラスにあわせるためにUでDataMapChipBaseParamを宣言します

abstract public class MapChipBase<U> : MonoBehaviourEx where U : DataMapChipBaseParam , new(){

	protected MapData m_mapData;
	public MapData mapData{
		get{
			return m_mapData;
		}
	}
	private U m_MapChipParam = new U();
	public U param{
		get{
			return m_MapChipParam;
		}
	}

	public enum ROAD_CONNECTION
	{
		NO_CHECK		= 0,
		DISCONNECT		,
		CONNECTION		,
		CONNECTION_SHOP	,		// SHOPだけじゃないけどね
		MAX				,
	}
	public ROAD_CONNECTION m_eRoadConnection;

	protected abstract void initialize (int _x, int _y, int _item_id);

	public virtual void Initialize(MapData _mapData, int _x , int _y , int _item_id ){
		m_mapData = _mapData;
		if (m_MapChipParam == null) {
			m_MapChipParam = new U ();
			m_MapChipParam.SetParam (_item_id,_x, _y);
		}
		initialize (_x, _y, _item_id);
	}
	public virtual void Initialize( MapData _mapData , U _param ) {
		m_MapChipParam = _param;
		Initialize (_mapData, _param.x, _param.y, _param.item_id);
	}

	// 自分が道かどうか
	public virtual bool IsRoad(){
		return false;
	}

	public int GetMapChipSerial(){
		return m_MapChipParam.mapchip_serial;
	}
	public int GetX(){
		return m_MapChipParam.x;
	}
	public int GetY(){
		return m_MapChipParam.y;
	}

	virtual protected void remove(){
		Debug.LogError ("please Inheritance");
	}
	public void Remove(){
		// データ的なものを削除するロジックを継承先で実装してください
		remove ();
		Destroy (gameObject);
		return;
	}


	// 自分おお店の周りに接続された道路があるかチェック＆セット
	public void CheckAroundConnectRoad<T>( MapRootBase<T,U> _mapRoot ) where T : MapChipBase<U> {
		MapChipBase<U>  mapchip = null;
		for (int x = param.x; x < param.x + param.width; x++) {
			int iYMin = param.y - 1;
			int iYMax = param.y + param.height;

			mapchip = _mapRoot.GetMapChip (x, iYMin);

			//Debug.Log (string.Format ("(x,y)=({0},{1})", x, iYMin));
			if ( mapchip != null && mapchip.IsConnectingRoad ()) {
				m_eRoadConnection = ROAD_CONNECTION.CONNECTION_SHOP;
				return;
			}
			mapchip = _mapRoot.GetMapChip (x, iYMax);
			//Debug.Log (string.Format ("(x,y)=({0},{1})", x, iYMax));
			if ( mapchip != null && mapchip.IsConnectingRoad ()) {
				m_eRoadConnection = ROAD_CONNECTION.CONNECTION_SHOP;
				return;
			}
		}
		for (int y = param.y; y < param.y + param.height; y++) {
			int iXMin = param.x - 1;
			int iXMax = param.x + param.width;

			mapchip = _mapRoot.GetMapChip (iXMin, y);
			//Debug.Log (string.Format ("(x,y)=({0},{1})", iXMin, y));
			if ( mapchip != null && mapchip.IsConnectingRoad ()) {
				m_eRoadConnection = ROAD_CONNECTION.CONNECTION_SHOP;
				return;
			}
			mapchip = _mapRoot.GetMapChip (iXMax, y);
			//Debug.Log (string.Format ("(x,y)=({0},{1})", iXMax, y));
			if (mapchip != null && mapchip.IsConnectingRoad ()) {
				m_eRoadConnection = ROAD_CONNECTION.CONNECTION_SHOP;
				return;
			}
		}
		//Debug.Log ("CheckAroundConnectRoad end nohit");
		return;
	}

	protected bool IsConnectingRoad(){

		if (IsRoad() && m_eRoadConnection == ROAD_CONNECTION.CONNECTION ) {
			return true;
		}
		return false;


	}



}
