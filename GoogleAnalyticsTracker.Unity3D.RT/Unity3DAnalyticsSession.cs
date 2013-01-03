using UnityEngine;

namespace GoogleAnalyticsTracker
{
    public class Unity3DAnalyticsSession
        : AnalyticsSession, IAnalyticsSession
    {
        private const string StorageKeyUniqueId = "GoogleAnalytics.UniqueID";
        private const string StorageKeyFirstVisitTime = "GoogleAnalytics.FirstVisitTime";
        private const string StorageKeyPreviousVisitTime = "GoogleAnalytics.PrevVisitTime";
        private const string StorageKeySessionCount = "GoogleAnalytics.SessionCount";
		
#if UNITY3D
        protected override string GetUniqueVisitorId() 
		{
            if (!SystemPrefUtil.HasLocalSetting(StorageKeyUniqueId))
            {
                SystemPrefUtil.SetLocalSettingString(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return SystemPrefUtil.GetLocalSettingString(StorageKeyUniqueId);
		}
		 
		protected override int GetFirstVisitTime()
        {
            if (!SystemPrefUtil.HasLocalSetting(StorageKeyFirstVisitTime))
            {
                SystemPrefUtil.SetLocalSettingInt(StorageKeyFirstVisitTime, base.GetFirstVisitTime());
            }
            return SystemPrefUtil.GetLocalSettingInt(StorageKeyFirstVisitTime);
		}
		
        protected override int GetPreviousVisitTime()
        {
            if (!SystemPrefUtil.HasLocalSetting(StorageKeyPreviousVisitTime))
            {
                SystemPrefUtil.SetLocalSettingInt(StorageKeyPreviousVisitTime, base.GetPreviousVisitTime());
            }

            var previousVisitTime = SystemPrefUtil.GetLocalSettingInt(StorageKeyPreviousVisitTime);
            SystemPrefUtil.SetLocalSettingInt(StorageKeyPreviousVisitTime, GetCurrentVisitTime());
            return previousVisitTime;
		}
		
        protected override int GetSessionCount()
        {
            if (!SystemPrefUtil.HasLocalSetting(StorageKeySessionCount))
            {
                SystemPrefUtil.SetLocalSettingInt(StorageKeySessionCount, base.GetPreviousVisitTime());
            }
            var sessionCount = SystemPrefUtil.GetLocalSettingInt(StorageKeySessionCount);
            SystemPrefUtil.SetLocalSettingInt(StorageKeySessionCount, sessionCount++);
            return sessionCount;
		}
#else
        private readonly IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;


        protected override string GetUniqueVisitorId()
        {
            if (!_settings.Contains(StorageKeyUniqueId))
            {
                _settings.Add(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return (string)_settings[StorageKeyUniqueId];
        }

        protected override int GetFirstVisitTime()
        {
            if (!_settings.Contains(StorageKeyFirstVisitTime))
            {
                _settings.Add(StorageKeyFirstVisitTime, base.GetFirstVisitTime());
            }
            return (int)_settings[StorageKeyFirstVisitTime];
        }

        protected override int GetPreviousVisitTime()
        {
            if (!_settings.Contains(StorageKeyPreviousVisitTime))
            {
                _settings.Add(StorageKeyPreviousVisitTime, base.GetPreviousVisitTime());
            }

            var previousVisitTime = (int)_settings[StorageKeyPreviousVisitTime];
            _settings[StorageKeyPreviousVisitTime] = GetCurrentVisitTime();
            return previousVisitTime;
        }

        protected override int GetSessionCount()
        {
            if (!_settings.Contains(StorageKeySessionCount))
            {
                _settings.Add(StorageKeySessionCount, base.GetPreviousVisitTime());
            }
            var sessionCount = (int)_settings[StorageKeySessionCount];
            _settings[StorageKeySessionCount] = sessionCount++;
            return sessionCount;
        }
#endif
    }
}
