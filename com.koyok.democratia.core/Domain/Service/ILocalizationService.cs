using System;
using System.Collections.Generic;
using System.Text;

namespace com.koyok.democratia.Domain.Service
{
    public interface ILocalizationService
    {
        string GetString(string key);
        string GetString(string key, params object[] args);
    }
}
