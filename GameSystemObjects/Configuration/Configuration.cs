﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystemObjects.Configuration
{
    public static class Configuration
    {


        public static CommonConfiguration databaseConnection;

    }

    public class CommonConfiguration
    {
        public string DatabaseConnection { get; set; }
    }
}