using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IFindSqlAsync<TEntity>
     where TEntity : class
    {
        /// <summary>
        /// Filters a collection of entities using a predicate
        /// </summary>
        /// <param name="sql">SQL containing named parameter placeholders. For example: SELECT * FROM Customer WHERE Id = @Id</param>
        /// <param name="parameters">Named parameters</param>
        /// <param name="parameterPattern">Parameter Regex pattern, Defualts to @(\w+)</param>
        /// <returns>Filtered collection of entities</returns>
        Task<IEnumerable<TEntity>> FindAsync(
          string sql,
          IDictionary<string, object> parameters = null,
          IDbTransaction transaction = null,
          string parameterPattern = @"@(\w+)");
    }
}
