﻿using StackExchange.Redis;
using System;

namespace WebApp01
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {

        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        /// <summary>
        /// 默认构造函数
        /// 注：使用默认配置，即localhost:6379,无密码
        /// </summary>
        public RedisCache(IDatabase db)
        {
            //databaseIndex
            //_redis = redis;
            _db = db;

        }



        private ConnectionMultiplexer _redisConnection { get; }

        private int _databaseIndex { get; }
        public bool ContainsKey(string key)
        {
            return _db.KeyExists(key);
        }

        public object GetCache(string key)
        {
            object value = null;
            var redisValue = _db.StringGet(key);
            if (!redisValue.HasValue)
                return null;
            ValueInfoEntry valueEntry = redisValue.ToString().ToObject<ValueInfoEntry>();
            if (valueEntry.TypeName == typeof(string).AssemblyQualifiedName)
                value = valueEntry.Value;
            else
                value = valueEntry.Value.ToObject(Type.GetType(valueEntry.TypeName));

            if (valueEntry.ExpireTime != null && valueEntry.ExpireType == ExpireType.Relative)
                SetKeyExpire(key, valueEntry.ExpireTime.Value);

            return value;
        }

        public TimeSpan? GetExpireTime(string key)
        {
            return _db.KeyTimeToLive(key);
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)GetCache(key);
        }

        public void SetKeyExpire(string key, TimeSpan expire)
        {
            _db.KeyExpire(key, expire);
        }

        public void RemoveCache(string key)
        {
            _db.KeyDelete(key);
        }

        public void SetCache(string key, object value)
        {
            _SetCache(key, value, null, null);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _SetCache(key, value, timeout, ExpireType.Absolute);
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _SetCache(key, value, timeout, expireType);
        }

        private void _SetCache(string key, object value, TimeSpan? timeout, ExpireType? expireType)
        {
            string jsonStr = string.Empty;
            if (value is string)
                jsonStr = value as string;
            else
                jsonStr = value.ToJson();

            ValueInfoEntry entry = new ValueInfoEntry
            {
                Value = jsonStr,
                TypeName = value.GetType().AssemblyQualifiedName,
                ExpireTime = timeout,
                ExpireType = expireType
            };

            string theValue = entry.ToJson();
            if (timeout == null)
                _db.StringSet(key, theValue);
            else
                _db.StringSet(key, theValue, timeout);
        }
    }
}
