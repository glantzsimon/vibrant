﻿using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IHealthQuestionnaireService : ICacheableService
    {
        HealthQuestionnaire GetHealthQuestionnaireForUser(int userId);
        HealthQuestionnaire GetHealthQuestionnaireForClient(int clientId);
        HealthQuestionnaire GetHealthQuestionnaireById(Guid externalId);
        void Save(HealthQuestionnaire model);
        List<Protocol> GetGeneticProfileMatchedProtocols(int clientId);
        Protocol GetAutoGeneratedProtocolFromGeneticProfile(int clientId);
        Protocol GetAutoGeneratedProtocolFromGeneticProfile(HealthQuestionnaire hq);
    }
}