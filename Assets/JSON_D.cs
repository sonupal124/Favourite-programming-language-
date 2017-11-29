using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class parseJSON
{
	public string title;
	public string id;
	public ArrayList but_title;
	public ArrayList but_image;
}
public class JSON_D : MonoBehaviour
{
	public parseJSON parsejson;
	public GameObject Refresh_btn;
	public Text Title;
	public Text[] Choice;
	public Text[] Votes;
	IEnumerator Start()
	{
		parsejson = new parseJSON();
		Refresh_btn.SetActive (false);
		Title.gameObject.SetActive (false);
		for(int i=0;i<Choice.Length;i++)
		{
			Choice[i].transform.parent.gameObject.SetActive(false);
		}
		string url = "https://private-5b1d8-sampleapi187.apiary-mock.com/questions";
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			Processjson(www.data);
		}
		else
		{
			Debug.Log("ERROR: " + www.error);
		}        
	}   

	private void Processjson(string jsonString)
	{
		JsonData jsonvalue = JsonMapper.ToObject(jsonString);
		parsejson.but_title = new ArrayList ();
		parsejson.but_image = new ArrayList ();
		for(int i=0;i< jsonvalue.Count;i++)
		{
			parsejson.title = jsonvalue[i]["question"].ToString();
			parsejson.id = jsonvalue[i]["published_at"].ToString();
			for(int j = 0; j<jsonvalue[i]["choices"].Count; j++)
			{
				parsejson.but_title.Add(jsonvalue[i]["choices"][j]["choice"].ToString());
				parsejson.but_image.Add(jsonvalue[i]["choices"][j]["votes"].ToString());
			}  
		}



		Data_Print ();
	}

	public void Data_Print()
	{
		Title.gameObject.SetActive (true);
		Title.text = parsejson.title.ToString();
		for(int i=0;i< parsejson.but_image.Count;i++)
		{
			if(i<Choice.Length)
			{
				Choice[i].transform.parent.gameObject.SetActive(true);
				Choice[i].text = parsejson.but_title[i].ToString();
				Votes[i].text = parsejson.but_image[i].ToString();
			}
		}
		Refresh_btn.SetActive (true);
	}
	public void Data_Refresh()
	{
		StartCoroutine ("Start");
	}
}
