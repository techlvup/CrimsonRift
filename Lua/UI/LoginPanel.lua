---@class LoginPanel
local LoginPanel = PrefabClass("LoginPanel")

local gameObject = nil

function LoginPanel.Awake(instance)
    gameObject = instance.gameObject

    LuaCallCS.SetActive(gameObject, "Text_Des", false)
    LuaCallCS.SetActive(gameObject, "Text_Progress", false)
    LuaCallCS.SetActive(gameObject, "Sli_Progress", false)

    LuaCallCS.PlayAnimation(gameObject, nil, "Play", WrapMode.Once, function()
        LuaCallCS.AddClickListener(gameObject, "Btn_Login", LoginPanel.Login)
    end)
end

function LoginPanel.Login()
    SdkMsgManager:Login(function(name)
        LuaCallCS.SetText(gameObject, "Text_Name", name)
    end)
end

return LoginPanel