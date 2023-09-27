using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IProtocolService
    {
        Protocol Find(int id);
        Protocol FindPrevious(int id);
        Protocol FindNext(int id);
        Protocol Find(Guid id);
        Protocol GetFullProtocol(Protocol protocolItem);
        Protocol Duplicate(int id);
        void DeleteChildRecords(int id);
        void AddDefaultSections(int id);

        List<Protocol> List(bool retrieveFullProtocol = false);
    }
}