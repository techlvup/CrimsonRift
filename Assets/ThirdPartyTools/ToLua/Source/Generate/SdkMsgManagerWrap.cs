﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class SdkMsgManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SdkMsgManager), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Login", Login);
		L.RegFunction("ReceiveAndroidMessage", ReceiveAndroidMessage);
		L.RegFunction("LogoutAccount", LogoutAccount);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Login(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SdkMsgManager obj = (SdkMsgManager)ToLua.CheckObject<SdkMsgManager>(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.Login(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReceiveAndroidMessage(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SdkMsgManager obj = (SdkMsgManager)ToLua.CheckObject<SdkMsgManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.ReceiveAndroidMessage(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogoutAccount(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SdkMsgManager obj = (SdkMsgManager)ToLua.CheckObject<SdkMsgManager>(L, 1);
			obj.LogoutAccount();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

