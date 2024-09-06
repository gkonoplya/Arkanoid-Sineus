
using System;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
  public class SaveLoadService
  {
   public T Load<T>(T defaultValue = default)
   {
     try
     {
       return JsonUtility.FromJson<T>(ES3.LoadRawString(typeof(T).Name + ".es"));
     }
     catch (Exception e)
     {
       Debug.LogError(e);
       return defaultValue;
     }
   }

   public void Save<T>(T data) where T : class => 
      ES3.SaveRaw(JsonUtility.ToJson(data),typeof(T).Name + ".es");
  }
}