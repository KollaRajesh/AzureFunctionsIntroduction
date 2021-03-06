﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsIntroduction
{
    public static class ConfigurationManagerHelper
    {
        public static string GetOrDefault(string key, string defaultValue = "")
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(key);
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[] GetOrDefault(string key, string[] defaultValue)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(key)?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) ?? defaultValue;
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int GetOrDefault(string key, int defaultValue)
        {
            try
            {
                return int.Parse(ConfigurationManager.AppSettings.Get(key));
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool GetOrDefault(string key, bool defaultValue)
        {
            try
            {
                return bool.Parse(ConfigurationManager.AppSettings.Get(key));
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
