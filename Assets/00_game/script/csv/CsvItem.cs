using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CsvItemParam : CsvDataParam {



	public int m_item_id;
	public int m_status;
	public string m_name;
	public int m_category;
	public int m_type;
	public int m_cell_type;
	public string m_description;
	public int m_need_coin;
	public int m_need_ticket;
	public int m_need_money;
	public int m_size;
	public int m_cost;
	public int m_area;
	public int m_revenue;
	public int m_revenue_interval;
	public int m_revenue_up;
	public int m_production_time;
	public int m_setting_limit;
	public int m_sub_parts_id;
	public int m_open_item_id;
	public int m_revenue_up2;
	public int m_add_coin;



	public int item_id { get{ return m_item_id; } set{ m_item_id = value; } }
	public int status { get{ return m_status; } set{ m_status= value; } }
	public string name { get{ return m_name; } set{ m_name = value; } }
	public int category { get{ return m_category; } set{ m_category = value; } }
	public int type { get{ return m_type; } set{ m_type = value; } }
	public int cell_type { get{ return m_cell_type; } set{ m_cell_type = value; } }
	public string description { get{ return m_description; } set{ m_description = value; } }
	public int need_coin { get{ return m_need_coin; } set{ m_need_coin = value; } }
	public int need_ticket { get{ return m_need_ticket; } set{ m_need_ticket = value; } }
	public int need_money { get{ return m_need_money; } set{ m_need_money = value; } }
	public int size { get{ return m_size; } set{ m_size = value; } }
	public int cost { get{ return m_cost; } set{ m_cost = value; } }
	public int area { get{ return m_area; } set{ m_area = value; } }
	public int revenue { get{ return m_revenue; } set{ m_revenue = value; } }
	public int revenue_interval { get{ return m_revenue_interval; } set{ m_revenue_interval = value; } }
	public int revenue_up { get{ return m_revenue_up; } set{ m_revenue_up = value; } }
	public int production_time { get{ return m_production_time; } set{ m_production_time = value; } }
	public int setting_limit { get{ return m_setting_limit; } set{ m_setting_limit = value; } }
	public int sub_parts_id { get{ return m_sub_parts_id; } set{ m_sub_parts_id = value; } }
	public int open_item_id { get{ return m_open_item_id; } set{ m_open_item_id = value; } }
	public int revenue_up2 { get{ return m_revenue_up2; } set{ m_revenue_up2 = value; } }
	public int add_coin { get{ return m_add_coin; } set{ m_add_coin = value; } }


	public void Copy( CsvItemParam _data ){
		int count = 0;
		item_id = _data.item_id;
		status = 0;			// 通常は利用できるとして扱う
		name = _data.name;
		category = _data.category;
		type = _data.type;
		cell_type = _data.cell_type;
		description = _data.description;
		need_coin = _data.need_coin;
		need_ticket = _data.need_ticket;
		need_money = _data.need_money;
		size = _data.size;
		cost = _data.cost;
		area = _data.area;
		revenue = _data.revenue;
		revenue_interval = _data.revenue_interval;
		revenue_up = _data.revenue_up;
		production_time = _data.production_time;
		setting_limit = _data.setting_limit;
		sub_parts_id = _data.sub_parts_id;
		open_item_id = _data.open_item_id;
		revenue_up2 = _data.revenue_up2;
		add_coin = _data.add_coin;
		item_id = _data.item_id;

	}


}


[System.Serializable]
public class CsvItem : CsvData< CsvItemParam>
{
	private static readonly string FilePath = "csv/item";
	public void Load() { Load(FilePath); }

	public CsvItemParam Select( int _iItemId ){
		return SelectOne( string.Format( " item_id = {0} " , _iItemId ));
	}

	public void Update( int _iItemId , Dictionary<string , string > _dict ){
		base.Update (_dict, string.Format (" item_id = {0}", _iItemId));
		return;
	}

}


