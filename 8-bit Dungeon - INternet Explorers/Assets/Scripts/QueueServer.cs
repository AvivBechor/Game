using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class QueueServer : MonoBehaviour
{
    private Client client;
    public Queue<Message> messages;
    public GameObject baseAttack;
    public GameObject baseEnemy;
    public IntStorage uuidHolder;
    public IntStorage gameIDHolder;
    public RuntimeAnimatorController mageController;
    public RuntimeAnimatorController warriorController;
    public SendQueue sendQueue;



    private void Start()
    {
        client = gameObject.GetComponent<Client>();
    }
    void Update()
    {
        messages = GameObject.Find("Connection").GetComponent<Client>().messages;
        if (messages.Count > 0)
        {
            Message currentMessage = messages.Dequeue();
            Debug.Log("message.cmd " + currentMessage.command + ", uuid " + currentMessage.uuid + " val " + currentMessage.data);
            Player serverPlayer = GameObject.Find("OtherPlayer").GetComponent<Player>();
            
            switch (currentMessage.command)
            {
                case "mov":
                    switch(currentMessage.data[1])
                    {
                        case "Player":
                            serverMovementScript otherPlayerMovement = GameObject.Find("OtherPlayer").GetComponent<serverMovementScript>();
                            otherPlayerMovement.xMovement = int.Parse(currentMessage.data[0].Split(',')[0]);
                            otherPlayerMovement.yMovement = int.Parse(currentMessage.data[0].Split(',')[1]);
                            break;
                        case "Enemy":
                            GameObject enemies = GameObject.Find("EnemyContainer");
                            GameObject enemy = null;
                            foreach (Transform child in enemies.transform)
                            {
                                if (child.GetComponent<UUIDHandler>().UUID == currentMessage.uuid)
                                {
                                    enemy = child.gameObject;
                                    break;
                                }
                            }
                            
                            enemyMovementScript enemymovement = enemy.GetComponent<enemyMovementScript>();
                            enemymovement.xMovement = int.Parse(currentMessage.data[0].Split(',')[0]);
                            enemymovement.yMovement = int.Parse(currentMessage.data[0].Split(',')[1]);
                            
                            //enemy.transform.position = new Vector3(float.Parse(currentMessage.data[0].Split(',')[0]), float.Parse(currentMessage.data[0].Split(',')[1]), enemy.transform.position.z);
                            break;
                    }
                    break;
                case "atk":
                    string type = currentMessage.data[3];
                    Side dir = Side.UP;
                    if(currentMessage.data[2].Equals("UP"))
                    {
                        dir = Side.UP;
                    }
                    else if(currentMessage.data[2].Equals("LEFT"))
                    {
                        dir = Side.LEFT;
                    }
                    else if(currentMessage.data[2].Equals("DOWN"))
                    {
                        dir = Side.DOWN; 
                    }
                    else if(currentMessage.data[2].Equals("RIGHT"))
                    {
                        dir = Side.RIGHT;
                    }
                    createAttack(
                        currentMessage.uuid,
                        (float.Parse(currentMessage.data[0].Split(',')[0]), float.Parse(currentMessage.data[0].Split(',')[1])),
                        currentMessage.data[1],
                        dir,
                        type,
                        float.Parse(currentMessage.data[4]),
                        int.Parse(currentMessage.data[5])
                        );
                    break;
                case "srt":
                    
                    serverPlayer.gameObject.SetActive(true);
                    serverPlayer.GetComponent<UUIDHandler>().UUID = currentMessage.uuid;
                    serverPlayer.character = new Character();
                    serverPlayer.character.title = currentMessage.data[0];
                    serverPlayer.gender = currentMessage.data[1].Equals("1");
                    //serverPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"MightyPack and more\MV\Characters\Actors_2")[54];
                    serverPlayer.GetComponent<Animator>().runtimeAnimatorController = (serverPlayer.character.title.ToLower().Equals("mage") ? mageController : warriorController);
                    //This is beautiful and is my son alive i love it ^
                    serverPlayer.CurrentHP.value = (serverPlayer.character.title.Equals("mage") ? 150 : 250);
                    //This is also my son but i don't love it as much ^
                    serverPlayer.transform.position = new Vector3(float.Parse(currentMessage.data[2].Split(',')[0]), float.Parse(currentMessage.data[2].Split(',')[1]), 83.19981f);
                    //serverPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(@"MightyPack and more\MV\Characters\Actors_2_54", typeof(Sprite)) as Sprite;
                    break;
                case "pos":
                    if(currentMessage.data[1].Equals("Player"))
                    {
                        Debug.Log("messgae is POS" + currentMessage.data[0]);
                        serverPlayer.transform.position = new Vector3(float.Parse(currentMessage.data[0].Split(',')[0]), float.Parse(currentMessage.data[0].Split(',')[1]), 83.19981f);
                    }
                    else
                    {
                        Transform enm = null;
                        foreach(Transform child in GameObject.Find("EnemyContainer").transform)
                        {
                            if(child.GetComponent<UUIDHandler>().UUID == currentMessage.uuid)
                            {
                                enm = child;
                                break;
                            }
                        }
                        if(enm)
                        {
                            enm.transform.position = new Vector3(float.Parse(currentMessage.data[0].Split(',')[0]), float.Parse(currentMessage.data[0].Split(',')[1]), 83.19981f);
                        }
                    }
                    break;
                case "kil":
                    Debug.Log("WE ARE KILLING A " + currentMessage.data[0] + " AND IT'S UUID IS " + currentMessage.uuid);
                    if(currentMessage.data[0].Equals("Player"))
                    {
                        foreach (Transform child in GameObject.Find("PlayersContainer").transform)
                        {
                            if(currentMessage.uuid == child.GetComponent<UUIDHandler>().UUID)
                            {
                                child.GetComponent<Player>().isDead = true;
                                break;
                            }
                        }
                        break;
                    }
                    GameObject Container = GameObject.Find(currentMessage.data[0] + "Container");
                    foreach (Transform child in Container.transform)
                    {
                        if(child.GetComponent<UUIDHandler>().UUID == currentMessage.uuid)
                        {
                            //Debug.Log("KILLED CHILD");
                            GameObject.Destroy(child.gameObject);
                            break;
                        }
                    }
                    break;
                case "enm":
                    createEnemy(
                        currentMessage.uuid, 
                        currentMessage.data[0],
                        (float.Parse(currentMessage.data[1].Split(',')[0]), float.Parse(currentMessage.data[1].Split(',')[1]))
                        );
                    break;
                case "win":
                    messages.Clear();
                    client.s.Close();
                    SceneManager.LoadScene("WIN");
                    break;
                case "los":
                    messages.Clear();
                    client.s.Close();
                    SceneManager.LoadScene("LOSE");
                    break;

                case "ehp":
                    foreach(Transform child in GameObject.Find("EnemyContainer").transform)
                    {
                        if(currentMessage.uuid == child.GetComponent<UUIDHandler>().UUID)
                        {
                            Debug.Log("HP is: " + currentMessage.data[0]);
                            child.GetComponent<Enemy>().HP = (int)(float.Parse(currentMessage.data[0]));
                            break;
                        }
                    }
                    break;
            }
        }
    }

    void createAttack(int atkUUID, (float, float) pos, string atkName, Side direction, string type, float damage, int uuid)
    {
        if (type.Equals("Player"))
        {
            GameObject a = GameObject.Instantiate(baseAttack, new Vector3(pos.Item1, pos.Item2, 83.19981f), Quaternion.identity);
            a.transform.SetParent(GameObject.Find("AttackContainer").transform);
            switch(atkName)
            {
                case "strike":
                    a.AddComponent<Strike>().SpawnAttack(atkName, direction, atkUUID);
                    break;
                case "vroom":
                    a.AddComponent<Vroom>().SpawnAttack(atkName, direction, atkUUID);
                    break;
            }
            a.GetComponent<Attack>().isHeadless = false;
            a.AddComponent<UUIDHandler>()
             .UUID = atkUUID;
        }
        else
        {
            
            foreach(Transform child in GameObject.Find("PlayersContainer").transform)
            {
                Debug.Log("Found " + child.name);
                if(uuid == child.GetComponent<UUIDHandler>().UUID)
                {
                    Debug.Log("MATCHED UUID " + uuid + " to " + child.name);
                    Player hurt = child.GetComponent<Player>();
                    hurt.CurrentHP.value -= (int)damage;
                    if (hurt.CurrentHP.value <= 0)
                    {
                        hurt.isDead = true;
                        if(uuid == GameObject.Find("Player").GetComponent<UUIDHandler>().UUID)
                        {
                            sendQueue.addMessage("kil" + ":" + child.GetComponent<gameIDHandler>().gameID + ":" + uuid + ":" + "Player");
                        }
                        
                    }

                    break;
                }
            }
            if (type.Equals("Skeleton"))
            {
                Transform enm = null;
                foreach (Transform child in GameObject.Find("EnemyContainer").transform)
                {
                    if (atkUUID == child.GetComponent<UUIDHandler>().UUID)
                    {
                        enm = child;
                        break;
                    }
                }
                Debug.Log("A SKELETON AHAS ATAACKED");
                Sprite spr = Resources.Load(@"Attacks\ATK_FIRE", typeof(Sprite)) as Sprite;
                GameObject a = Instantiate(baseAttack, new Vector3(-1, 1, 83.19981f), Quaternion.identity);
                a.transform.parent = enm;
                a.transform.localPosition = new Vector3(-1, 1, 83.19981f);
                a.GetComponent<SpriteRenderer>().sprite = spr;
                a.AddComponent<Implode>().lifespan = 0.5f;

            }
            else if (type.Equals("Vampire"))
            {
                Transform enm = null;
                foreach (Transform child in GameObject.Find("EnemyContainer").transform)
                {
                    if (atkUUID == child.GetComponent<UUIDHandler>().UUID)
                    {
                        enm = child;
                        break;
                    }
                }

                Sprite spr = Resources.Load(@"Attacks\ATK_BAT", typeof(Sprite)) as Sprite;
                GameObject a = Instantiate(baseAttack, new Vector3(-1, 1, 83.19981f), Quaternion.identity);
                a.transform.parent = enm.transform;
                a.transform.localPosition = new Vector3(-1, 1, 83.19981f);
                a.GetComponent<SpriteRenderer>().sprite = spr;
                a.AddComponent<Implode>().lifespan = 0.5f;
            }
            else if (type.Equals("Sorcerer"))
            {

                Transform enm = null;
                foreach (Transform child in GameObject.Find("EnemyContainer").transform)
                {
                    if (atkUUID == child.GetComponent<UUIDHandler>().UUID)
                    {
                        enm = child;
                        break;
                    }
                }

                Sprite spr = Resources.Load(@"Attacks\ATK_NOTATK", typeof(Sprite)) as Sprite;
                GameObject a = Instantiate(baseAttack, new Vector3(0, 1, 83.19981f), Quaternion.identity);
                a.transform.parent = enm.transform;
                a.transform.localPosition = new Vector3(0, 0, 0);
                a.GetComponent<SpriteRenderer>().sprite = spr;
                a.AddComponent<Implode>().lifespan = 0.5f;
            }
        }
    }

    int createEnemy(int UUID, string enemyName, (float, float) pos)
    {
        enemyName = enemyName.ToLower();
        GameObject a = GameObject.Instantiate(baseEnemy, new Vector3(pos.Item1, pos.Item2, 83.19981f), Quaternion.identity);
        a.transform.SetParent(GameObject.Find("EnemyContainer").transform);
        switch(enemyName)
        {
            case "skeleton":
                a.AddComponent<Skeleton>().Init("Skeleton");
                break;
            case "sorcerer":
                a.AddComponent<Sorcerer>().Init("Sorcerer");
                break;
            case "vampire":
                a.AddComponent<Vampire>().Init("Vampire");
                break;
            case "boss":
                a.AddComponent<Boss>().Init("Boss");
                break;
            default:
                Debug.Log("Enemy doesn't exist: " + enemyName);
                Destroy(a);
                return 1;              
        }
        
        a.AddComponent<enemyMovementScript>();
        a.GetComponent<UUIDHandler>().UUID = UUID;
        return 0;
    }


}
