using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text ScoreNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public int h_Score;
    public string playerName;

    
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        Debug.Log(h_Score + " at start");
        StartCoroutine(ShowData());
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            LoadData();
            ScoreNameText.text = "Best Score : " + playerName + ": " + h_Score;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveData();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        MenuManager.Instance.canvas.enabled = true;
    }

      [System.Serializable]
    class PlayerData
    {
        public string playerName;
        public int high_Score;
    }

    public void SaveData()
    {
        PlayerData mydata = new PlayerData();
        if (h_Score <= m_Points)
        {
            Debug.Log(h_Score + " Is IT less then" + h_Score);
            mydata.high_Score = m_Points;
            Debug.Log(h_Score + "After");
            mydata.playerName = MenuManager.Instance.nameField.text;
        }
        else
        {
            mydata.high_Score = h_Score; 
            mydata.playerName = playerName;
        }

        string json = JsonUtility.ToJson(mydata);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData mydata = JsonUtility.FromJson<PlayerData>(json);
            playerName = mydata.playerName;
            h_Score = mydata.high_Score;
        }
    }
    
    IEnumerator ShowData()
    {
        yield return new WaitForEndOfFrame();
        LoadData();
        ScoreNameText.text = "Best Score : " + playerName + ": " + h_Score;
    }
}
