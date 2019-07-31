using System;
using MySql.Data.MySqlClient;

namespace salary.dal.repository
{
    public static class MySqlDataReaderExtension
    {
        public static dto.Employee ReadEmployee(this MySqlDataReader dataReader)
        {
            if (dataReader.HasRows == false) return null;
            var mySqlUuid = dataReader[Employee.cId] as byte[];
            var result = new dto.Employee
            {
                Id = mySqlUuid.CreateGuidFromMySqlByteOrder(),
                Name = dataReader[Employee.cName].ToString(),
                Rate = Decimal.ToDouble(Decimal.Parse(dataReader[Salary.cRate].ToString())),
                Kind = dataReader[Salary.cKind].ToString()
            };

            return result;
        }
    }
}