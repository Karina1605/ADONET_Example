﻿using System;
using System.Collections.Generic;
using System.Text;
using DataBaseModels.DataBaseTables;

namespace DataBaseConnector.Interfaces
{
    interface IDataBaseRep
    {
        ICRUD<Category> Categories { get; }
        ICRUD<Color> Colors { get; }
        ICRUD<Product> Products { get; }
    }
}
