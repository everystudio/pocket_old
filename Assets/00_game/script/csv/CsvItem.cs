using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvItemParam : CsvDataParam {

	public int item_id { get; private set; }
	public int status { get; private set; }
	public string name { get; private set; }
	public int category { get; private set; }
	public int type { get; private set; }
	public int cell_type { get; private set; }
	public string description { get; private set; }
	public int need_coin { get; private set; }
	public int need_ticket { get; private set; }
	public int need_money { get; private set; }
	public int size { get; private set; }
	public int cost { get; private set; }
	public int area { get; private set; }
	public int revenue { get; private set; }
	public int revenue_interval { get; private set; }
	public int revenue_up { get; private set; }
	public int production_time { get; private set; }
	public int setting_limit { get; private set; }
	public int sub_parts_id { get; private set; }
	public int open_item_id { get; private set; }
	public int revenue_up2 { get; private set; }
	public int add_coin { get; private set; }


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


