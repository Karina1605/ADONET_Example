using System;
using System.Collections.Generic;
using System.Text;
using DataBaseModels.DataBaseTables;

namespace DataBaseConnector.Interfaces
{
    public interface IDataBaseRep
    {
        ICRUD<Category> Categories { get; }
        ICRUD<Color> Colors { get; }
        ICRUD<Product> Products { get; }
    }
}
