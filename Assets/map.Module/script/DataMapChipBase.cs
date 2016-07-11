using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataMapChipBaseParam : CsvDataParam{

	public int m_mapchip_serial;
	public int m_item_id;
	public int m_x;
	public int m_y;
	public int m_width;
	public int m_height;
	public int m_category;

	public int mapchip_serial { get{ return m_mapchip_serial;} set{m_mapchip_serial= value; } }
	public int item_id { get{ return m_item_id;} set{m_item_id = value; } }
	public int x { get{ return m_x;} set{m_x = value; } }
	public int y { get{ return m_y;} set{m_y = value; } }
	public int width { get{ return m_width;} set{m_width = value; } }
	public int height { get{ return m_height;} set{m_height = value; } }
	public int category { get{ return m_category;} set{m_category = value; } }


}


public class DataMapChipBase<T> : CsvData<T> where T : DataMapChipBaseParam , new(){

}



