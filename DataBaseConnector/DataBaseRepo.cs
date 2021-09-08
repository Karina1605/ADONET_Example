using System;
using System.Collections.Generic;
using System.Text;
using DataBaseConnector.Implementation;
using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;

namespace DataBaseConnector
{
    class DataBaseRepo : IDataBaseRep
    {
        public ICRUD<Category> Categories { get; private set; }

        public ICRUD<Color> Colors { get; private set; }

        public ICRUD<Product> Products { get; private set; }

        public DataBaseRepo(string connectionString)
        {
            Categories = new CategoriesRepo(connectionString);
            Colors = new ColorsRepo(connectionString);
            Products = new ProductsRepo(connectionString);
        }
    }
}
