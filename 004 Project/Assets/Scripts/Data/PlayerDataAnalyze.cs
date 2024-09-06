using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataAnalyze : MonoBehaviour
{
    public static PlayerDataAnalyze instance;
    public PlayerDataCollect pdc;
    public string playerType;
    private void Awake()
    {
        playerType = "High_dash";
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        if (pdc == null)
        {
            Debug.LogError("PlayerDataCollect instance is not assigned!");
            return;
        }

        // actionData 딕셔너리를 가져옵니다.
        Dictionary<string, int> actionData = pdc.actionData;

        // 플레이어 데이터를 분석합니다.
        AnalyzePlayerData(actionData);
    }

    // 플레이어 데이터를 분석하는 메서드
    public void AnalyzePlayerData(Dictionary<string, int> actionData)
    {
        // 총 액션 수 계산
        int totalActions = actionData["ParryAttempt"] + actionData["DashAttempt"] + actionData["DefenceAttempt"];

        // 로지스틱 함수 적용
        float parryRatio = LogisticFunction((float)actionData["ParryAttempt"] / totalActions);
        float dashRatio = LogisticFunction((float)actionData["DashAttempt"] / totalActions);
        float runRatio = LogisticFunction((float)actionData["DefenceAttempt"] / totalActions);

        // 비율 정규화
        float ratioSum = parryRatio + dashRatio + runRatio;
        parryRatio /= ratioSum;
        dashRatio /= ratioSum;
        runRatio /= ratioSum;

        // 플레이어 스타일 분류
        string playStyle = ClassifyPlayer(parryRatio, dashRatio, runRatio);

        // 결과 출력
        Debug.Log($"Parry Ratio = {parryRatio:F4}, Dodge Ratio = {dashRatio:F4}, Run Ratio = {runRatio:F4}, Play Style = {playStyle}");
    }

    // 로지스틱 함수
    float LogisticFunction(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    // 플레이어 스타일 분류
    public string ClassifyPlayer(float parryRatio, float dashRatio, float runRatio)
    {
        Dictionary<string, float> ratios = new Dictionary<string, float>
        {
            { "parry", parryRatio },
            { "dodge", dashRatio },
            { "run", runRatio }
        };

        
        float maxRatio = -1;

        foreach (var entry in ratios)
        {
            if (entry.Value > maxRatio)
            {
                maxRatio = entry.Value;
                playerType = entry.Key;
            }
        }

        switch (playerType)
        {
            case "parry":
                return maxRatio > 0.5f ? "High_parry" : maxRatio > 0.4f ? "parry" : "Balanced";
            case "dash":
                return maxRatio > 0.5f ? "High_dash" : maxRatio > 0.4f ? "dash" : "Balanced";
            case "run":
                return maxRatio > 0.5f ? "High_run" : maxRatio > 0.4f ? "run" : "Balanced";
            default:
                return "Balanced";
        }
    }
}