using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using LuaInterface;
using System.Collections.Generic;
using TapSDK.Core;
using TapSDK.Login;
using System.Threading.Tasks;



public class SdkMsgManager : MonoBehaviour
{
    private AndroidJavaClass m_gameHelper = null;
    private const string tapTapId = "pkx0l15iusfahz2dbt";
    private const string tapTapToken = "BA8F5YiJNzKdTu7JU1WQTFdNc1FeoP3tolHrBVE3";
    private TapTapSdkOptions tapTapSdkOptions = null;
    private TapTapAccount tapTapAccount = null;
    private LuaFunction m_loginCallBack = null;



    private void Awake()
    {
        InitTapTapSdkOptions();

        if (DataUtilityManager.m_platform == "Android")
        {
            m_gameHelper = new AndroidJavaClass("com.VastStarryRiver.CrimsonRift.GameHelper");//编辑器环境下无法测试
        }
    }



    public void Login(LuaFunction loginCallBack)
    {
        m_loginCallBack = loginCallBack;

        if (DataUtilityManager.m_platform == "Android")
        {
            if (m_gameHelper == null)
            {
                return;
            }

            m_gameHelper.CallStatic("Login");
        }
        else
        {
            LoginTapTap();
        }
    }

    public void ReceiveAndroidMessage(string message)
    {
        JObject jsonData = JObject.Parse(message);

        string name = (string)jsonData["Name"];
        string param = (string)jsonData["Param"];

        if (name == "Login")
        {
            if(param == "TapTap")
            {
                LoginTapTap();
            }
        }
    }

    private void InitTapTapSdkOptions()
    {
        if (tapTapSdkOptions != null)
        {
            return;
        }

        tapTapSdkOptions = new TapTapSdkOptions
        {
            clientId = tapTapId,//ID，开发者后台获取

            clientToken = tapTapToken,//令牌，开发者后台获取

            region = TapTapRegionType.CN,// 地区，CN 为国内，Overseas 为海外

            preferredLanguage = TapTapLanguageType.zh_Hans,// 语言，默认为 Auto，默认情况下，国内为 zh_Hans，海外为 en

            enableLog = true,// 是否开启日志，Release 版本请设置为 false
        };

        TapTapSDK.Init(tapTapSdkOptions);

        // 当需要添加其他模块的初始化配置项，例如合规认证、成就等， 请使用如下 API
        TapTapSdkBaseOptions[] otherOptions = new TapTapSdkBaseOptions[]
        {

        };

        TapTapSDK.Init(tapTapSdkOptions, otherOptions);
    }

    private void LoginTapTap()
    {
        try
        {
            AsyncLoginAccount();
        }
        catch (TaskCanceledException)
        {
            Debug.Log("用户取消登录");
        }
        catch (Exception exception)
        {
            Debug.Log($"登录失败，出现异常：{exception}");
        }
    }

    private async void AsyncLoginAccount()
    {
        // 定义授权范围
        List<string> scopes = new List<string>() { TapTapLogin.TAP_LOGIN_SCOPE_PUBLIC_PROFILE };
        tapTapAccount = await TapTapLogin.Instance.LoginWithScopes(scopes.ToArray());
        m_loginCallBack?.Call(tapTapAccount.name);
    }

    public void LogoutAccount()
    {
        if (tapTapAccount == null)
        {
            return;
        }

        TapTapLogin.Instance.Logout();
    }
}