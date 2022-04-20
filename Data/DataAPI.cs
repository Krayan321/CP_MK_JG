using System;

namespace Data
{
    public abstract class DataAPI
    {
        public static DataAPI CreateDataBase()
        {
            return new DataBase();
        }

    }

    public class DataBase : DataAPI
    {
        public DataBase()
        {

        }
    }
}
