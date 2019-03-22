using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class LevelParser  {

    // Mohamed- Making this function to get rid of the big code in the main function


    public LevelParser() {

    }

    private Dictionary<string, Sprite> loadRessources()
    {
        List<Sprite> spriteList = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/"));
        Debug.Log(spriteList.Count);
        Dictionary<string, Sprite>  editorSprites = new Dictionary<string, Sprite>();
        foreach (Sprite s in spriteList)
        {
            if (!editorSprites.ContainsKey(s.name))
            {
                editorSprites.Add(s.name, s);

            }
        }
        return editorSprites;
    }

    private List<Positionable> generateMap(string path, Dictionary<string, Sprite> editorSprites)
    {
        List<Positionable> positionables = new List<Positionable>();
        // Sereialization item generation 
        XmlSerializer serializer = new XmlSerializer(typeof(item[]),
                                 new XmlRootAttribute() { ElementName = "items" });

        FileStream fileStreamIn = new FileStream(path, FileMode.Open);
        item[] i = (item[])serializer.Deserialize(fileStreamIn);
        foreach (item j in i)
        {
            positionables.Add(getInstanceFromName(toVector(j.id), j.value, editorSprites));

        }
        fileStreamIn.Close();
        return positionables;
    }

    private Positionable getInstanceFromName(Vector2 position, string name, Dictionary<string, Sprite> editorSprites)
    {
        switch (name)
        {

            case "Floor": return new Positionable(position, getSpriteFromName(name, editorSprites));
            case "Boar": return new Positionable(position, getSpriteFromName(name, editorSprites));
                /* ... */
        }
        return null;
    }

    private Sprite getSpriteFromName(string name, Dictionary<string, Sprite> editorSprites )
    {
        if (editorSprites.ContainsKey(name))
        {

            return editorSprites[name];
        }
        return null;
    }


    private Vector2 toVector(string s)
    {
        string[] sp = s.Split(',');
        Vector2 v = new Vector2((float)System.Convert.ToDouble(sp[0]), (float)System.Convert.ToDouble(sp[1]));
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
