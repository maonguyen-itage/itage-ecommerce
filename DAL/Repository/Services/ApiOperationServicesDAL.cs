using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class ApiOperationServicesDAL : IApiOperationServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public ApiOperationServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public async Task<Apiconfiguration?> GetAPIConfiguration(string UrlName)
        {
            Apiconfiguration? result = new Apiconfiguration();
            using (var repo = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 *")
                        .From("APIConfigurations")
                        .Where("UrlName=@0", UrlName);
                    result = repo.Query<Apiconfiguration>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<string?> GetApiData(Dictionary<string, object>? requestParameters, Apiconfiguration? apiConfiguration)
        {
            string result = "";
            try
            {
                if (String.IsNullOrWhiteSpace(apiConfiguration.Ormtype) || apiConfiguration.Ormtype == "PetaPoco")
                {
                    using (var repo = _contextHelper.GetDataContextHelper())
                    {
                        result = repo.Fetch<string>(apiConfiguration.SqlQuery, requestParameters).FirstOrDefault();
                        await Task.FromResult(result);
                        return result;
                    }
                }
                else if (apiConfiguration.Ormtype == "Dapper")
                {
                    using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                    {
                        dbConnection.Open();
                        result = dbConnection.Query<string>(apiConfiguration.SqlQuery, requestParameters , commandType: CommandType.Text).FirstOrDefault();

                        dbConnection.Close();
                        await Task.FromResult(result);
                        return result;
                    }
                }
                else
                {
                    using (var repo = _contextHelper.GetDataContextHelper())
                    {
                        result = repo.Fetch<string>(apiConfiguration.SqlQuery, requestParameters).FirstOrDefault();
                        await Task.FromResult(result);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
