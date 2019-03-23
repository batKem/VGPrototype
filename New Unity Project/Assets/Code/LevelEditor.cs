
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

using UnityEngine;

public class LevelEditor : MonoBehaviour {

    //Level Editor Class generating XML file from user generated objects at runtime
    //will be used later, no need to work on that for now


    private int objectIndex;
    public int offset;

	Dictionary<Vector2, string> level;
    Dictionary<Vector2, Positionable> levelRenderer;
    List<string> classes = new List<string> (new string[]{"Floor","Boar","Player","Fox"});
    Dictionary<string, Sprite> editorSprites ;
    Vector2 mousPos;

    XmlSerializer serializer;

    FileStream fileStreamIn;
    FileStream fileStreamOut;

	// Use this for initialization
	void Start () {
		this.objectIndex = 0;
        this.level = new Dictionary<Vector2, string>();
        this.levelRenderer = new Dictionary<Vector2, Positionable>();

        List<Sprite> spriteList = new List<Sprite>( Resources.LoadAll<Sprite>("Sprites/"));
        Debug.Log(spriteList.Count);
        this.editorSprites = new Dictionary<string, Sprite>();

        foreach(Sprite s in spriteList){
            if(!editorSprites.ContainsKey(s.name)){
                editorSprites.Add(s.name, s);
               
            }
        }


        // Sereialization item generation 
        serializer = new XmlSerializer(typeof(item[]), 
                                 new XmlRootAttribute() { ElementName = "items" });
        
         
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mouseScrollDelta.y >=1 ){
            this.objectIndex = Mathf.Min(++this.objectIndex, this.classes.Count -1);
        }
        if (Input.mouseScrollDelta.y <= -1 ){
            this.objectIndex = Mathf.Max(--this.objectIndex, 0);
        }
        
        //this.mousPos = new Vector2(Mathf.Floor((Input.mousePosition/offset).x) , Mathf.Floor((Input.mousePosition/offset).y))*offset;
        this.mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.mousPos = new Vector2( mousPos.x+offset/2,mousPos.y+offset/2);
        this.mousPos = new Vector2(Mathf.Floor((this.mousPos/offset).x ) , Mathf.Floor((this.mousPos/offset).y ))*offset;
        
        string name = classes[objectIndex];

        if (Input.GetMouseButtonDown(0) ){
            Debug.Log("adding entry to level map" + classes[objectIndex]);
            if (!level.ContainsKey(this.mousPos)){ 
                    this.level.Add(this.mousPos,name);
                if (editorSprites.ContainsKey(name)){
                    
                    this.levelRenderer.Add(this.mousPos, new Positionable(mousPos,editorSprites[name]));
                }
                else{
                Debug.Log("no sprite named"+ name+" found in ressources file");
                }

            }
            else{ 
                this.level[this.mousPos] =name;
                if (editorSprites.ContainsKey(name)){
                    Sprite o =  (Instantiate(editorSprites[name], mousPos,Quaternion.identity));
                    levelRenderer[this.mousPos].Destroy();
                    levelRenderer[this.mousPos] = new Positionable(mousPos,editorSprites[name]);
                    }
                }
            
        } 

        
        
        

        if (Input.GetKeyDown(KeyCode.S)){
            fileStreamOut = new FileStream("level.xml", FileMode.Create);
            serializer.Serialize(fileStreamOut, toItem(level));
            fileStreamOut.Close();
        }
        else if (Input.GetKeyDown(KeyCode.L)){
            
            fileStreamIn = new FileStream("level.xml",FileMode.Open);
            this.level = toRenderedMap((item[])serializer.Deserialize(fileStreamIn));
            fileStreamIn.Close();
        }
        
	}
    public void OnApplicationQuit(){
    }
    private item[] toItem( Dictionary<Vector2,string> m ){
        Debug.Log("ffffff");
        item[] i = null;
        if (m != null){
            
            i= new item[m.Count];
            int j =0;
            foreach(Vector2 k in m.Keys){
                Debug.Log("saving :"+ k.ToString(k.x+ ","+ k.y) + "->"+ m[k]);
                i[j++] = new item(){ id =k.x+","+k.y, value =m[k] };
            }
        }
        return i;
    }
    
    private Dictionary<Vector2, string> toRenderedMap(item[] i){
        Dictionary<Vector2, string> m = new Dictionary<Vector2, string>();
        this.levelRenderer = new Dictionary<Vector2, Positionable>();
        if (i != null){
            foreach( item j in i){
                if (  !m.ContainsKey( toVector(j.id))){
                m.Add(  toVector(j.id), j.value);
                if (editorSprites.ContainsKey(j.value)){
                        //Debug.Log(toVector(j.id) + " - " + j.value);
                        this.levelRenderer.Add(toVector(j.id), new Positionable(toVector(j.id),editorSprites[j.value]));
                }
                }
            }

        }
        return m;
    }

    

    private Vector2 toVector(string s){
        string[] sp = s.Split(',');
       Vector2 v =new Vector2( (float) System.Convert.ToDouble(sp[0]),(float) System.Convert.ToDouble(sp[1])) ;
       //Debug.Log("reading" +v);
       return v;
    }

    public class item
    {
        [XmlAttribute]
    public string id;
        [XmlAttribute]
    public string value;
    }

}
