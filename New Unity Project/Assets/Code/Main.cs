using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour {

	List<Positionable> entities;

	Dictionary<string, Sprite> editorSprites ;


	// Use this for initialization
	void Start () {
        this.loadRessources();

        //this.entities = generateMap("level.xml");
        this.entities = new List<Positionable>();
        this.entities.Add(new Player(new Vector2(0, 0)));

	}
	
	// Update is called once per frame
	void Update () {
		if(this.entities != null){
			foreach( Positionable a in this.entities){
				a.update();

			}
		}
	}


    //Initializes datastructures and loads Sprite files 
    //(second part not used for now : will be used later to read actors positions from a xml level file)
    private void loadRessources() {
        this.entities = new List<Positionable>();

        List<Sprite> spriteList = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/"));
        Debug.Log(spriteList.Count);
        this.editorSprites = new Dictionary<string, Sprite>();
        foreach (Sprite s in spriteList)
        {
            if (!editorSprites.ContainsKey(s.name))
            {
                editorSprites.Add(s.name, s);

            }
        }
    }

    //Helper function for loadRessources()
	private List<Positionable> generateMap(string path){
		List<Positionable> positionables = new List<Positionable>();
		 // Sereialization item generation 
        XmlSerializer serializer = new XmlSerializer(typeof(item[]), 
                                 new XmlRootAttribute() { ElementName = "items" });

		FileStream fileStreamIn = new FileStream(path,FileMode.Open);
        item[] i = (item[])serializer.Deserialize(fileStreamIn);
		foreach( item j in i){
			positionables.Add( getInstanceFromName(toVector(j.id), j.value));

		}
        fileStreamIn.Close();
		return positionables;
	}
    //Helper function for loadRessources()
    private Positionable getInstanceFromName(Vector2 position, string name){
		switch (name){

			case "Floor" : return new Positionable(position, getSpriteFromName(name));
			case "Boar" : return new Positionable(position, getSpriteFromName(name));
			/* ... */
		}
		return null;
	}
    //Helper function for loadRessources()
    private Sprite getSpriteFromName(string name){
		if (editorSprites.ContainsKey(name)){
                    
        	return editorSprites[name] ;
        }
		return null;
	}

    //Helper function for loadRessources()
    private Vector2 toVector(string s){
        string[] sp = s.Split(',');
       Vector2 v =new Vector2( (float) System.Convert.ToDouble(sp[0]),(float) System.Convert.ToDouble(sp[1])) ;
       //Debug.Log("reading" +v);
       return v;
    }
    //Helper class for loadRessources() (xml parse pattern)
    public class item
    {
        [XmlAttribute]
    public string id;
        [XmlAttribute]
    public string value;
    }
}
