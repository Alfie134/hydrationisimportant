using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;



namespace Repositories.Interfaces.Base
{

    public interface IDeleteRepository<T>
    {
        // Returnerer true, hvis sletning lykkedes, ellers false


        // Med entitet der skal slettes
        bool Delete(T entity,  SqlConnection connection, SqlTransaction? transaction = null);

        // Eller med Id der skal slettes 
        bool DeleteById(int id , SqlConnection connection, SqlTransaction? transaction = null);
    }
}
