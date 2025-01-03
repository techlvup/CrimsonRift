﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_Events_UnityEvent_intWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.Events.UnityEvent<int>), typeof(UnityEngine.Events.UnityEventBase), "UnityEvent_int");
		L.RegFunction("AddListener", AddListener);
		L.RegFunction("RemoveListener", RemoveListener);
		L.RegFunction("Invoke", Invoke);
		L.RegFunction("New", _CreateUnityEngine_Events_UnityEvent_int);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_Events_UnityEvent_int(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UnityEngine.Events.UnityEvent<int> obj = new UnityEngine.Events.UnityEvent<int>();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Events.UnityEvent<int>.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Events.UnityEvent<int> obj = (UnityEngine.Events.UnityEvent<int>)ToLua.CheckObject<UnityEngine.Events.UnityEvent<int>>(L, 1);
			UnityEngine.Events.UnityAction<int> arg0 = (UnityEngine.Events.UnityAction<int>)ToLua.CheckDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
			obj.AddListener(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Events.UnityEvent<int> obj = (UnityEngine.Events.UnityEvent<int>)ToLua.CheckObject<UnityEngine.Events.UnityEvent<int>>(L, 1);
			UnityEngine.Events.UnityAction<int> arg0 = (UnityEngine.Events.UnityAction<int>)ToLua.CheckDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
			obj.RemoveListener(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Invoke(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Events.UnityEvent<int> obj = (UnityEngine.Events.UnityEvent<int>)ToLua.CheckObject<UnityEngine.Events.UnityEvent<int>>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.Invoke(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
