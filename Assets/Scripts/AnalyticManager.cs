using UnityEngine;
using Analytics;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/**
 * This is the main analysis manager from which all analysis objects can be accessed
 */
public class AnalyticManager : MonoBehaviour
{
    // Objects
    public GameObject player;
    private PlayerController controller;
    private TimeBank bank;
    
    // Session objects
    private AnalyticGameSession session;
    private string playID;
    private float rt = 0.0f;
    private long sessionId;
    private string levelName;
    private string userId;
    
    // Analytics
    private static int shotID;
    private static int damageID;
    private string currentSection;

    // URI Link 
    private APILink _linkHandler;
    private string editorLink;
    private string deploymentLink;

    private void Awake()
    {
        userId = AnalyticsSessionInfo.userId;
        sessionId = AnalyticsSessionInfo.sessionId;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Link Handles
        _linkHandler = new APILink();
        editorLink = _linkHandler.getEditorAPI();
        deploymentLink = _linkHandler.getDeploymentAPI();
        
        levelName = SceneManager.GetActiveScene().name;
        
        // avoid too many get Components
        controller = player.GetComponent<PlayerController>();
        bank = player.GetComponent<TimeBank>();

        // For session Analytics
        session = new AnalyticGameSession(sessionId, userId);
        playID = System.Guid.NewGuid().ToString();
        shotID = 0;
        damageID = 0;
    }
    
    public void setCurrentSection(string secName)
    {
        this.currentSection = secName;
    }
    
    public string getCurrentSection()
    {
        return this.currentSection;
    }

    public string getEditLink()
    {
        return editorLink;
    }
    
    public string getDeployLink()
    {
        return deploymentLink;
    }
    
    public int GetNumberDeaths()
    {
        return controller.getNumberDeaths();
    }
    
    public string GetPlayID()
    {
        return playID;
    }

    public long GetSessionID()
    {
        return this.sessionId;
    }

    // Update is called once per frame
    void Update()
    {
        rt += Time.deltaTime;
    }
    public void SendShootInfo(int x, int y, int z, int timeStored, int age, int health, string clickType, string interactionType, string objectInteracted)
    {
        shotID++;
        var info = new ShootMapping(x, y, z,timeStored, age, health, clickType, interactionType, objectInteracted, rt);
        StoreData(JsonConvert.SerializeObject(info), "GunDetail", shotID);
    }

    public void SendDamageInfo(bool died,string damagingObject,int prevHearts,int afterHears,int x,int y)
    {
        damageID++;
        var damageMap = new Damaged(died, damagingObject, prevHearts, afterHears, x, y, currentSection);
        StoreData(JsonConvert.SerializeObject(damageMap), "DamageDetails", damageID);
    }

    public void SendSessionInfo(bool finished)
    {
        session.DataUpdate(finished, levelName, this.rt, controller.getHP().GetHP(), bank.GetTimeStore(), controller.getNumberDeaths());
        StoreData(session.ToString(), "GameAnalytic");
    }
    
    private void StoreData(string json, string location)
    {
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{deploymentLink}/{location}/{sessionId.ToString()}_{playID}_{levelName}/.json", json);
        }
        else
        {
            RestClient.Put($"{editorLink}/{location}/{sessionId.ToString()}_{playID}_{levelName}/.json", json);
        }
    }
    
    private void StoreData(string json, string location, int id)
    {
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{deploymentLink}/{location}/{sessionId.ToString()}_{playID}_{levelName}/{id}/.json", json);
        }
        else
        {
            RestClient.Put($"{editorLink}/{location}/{sessionId.ToString()}_{playID}_{levelName}/{id}/.json", json);
        }
    }
}
