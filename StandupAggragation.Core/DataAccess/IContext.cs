using System;

namespace StandupAggragation.Core.DataAccess
{
    public interface IContext : IDisposable
    {
        object Fetch(string command, object filter);
    }
}