﻿using K9.WebApplication.Models;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface ILogService
    {
        List<LogItem> GetLogItems();
    }
}