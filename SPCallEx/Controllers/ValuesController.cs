using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCallEx.Models;

namespace SPCallEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly RECRUITContext _context;

        public ValuesController(RECRUITContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IEnumerable<object>> PostSP([FromBody] ClassInput sData)
        {
            var returnObject = new List<dynamic>();

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "dbo.SP_Test1";
                cmd.CommandType = CommandType.StoredProcedure;
                //加入參數
                cmd.Parameters.Add(new SqlParameter("item", sData.bottom));
                cmd.Parameters.Add(new SqlParameter("field", sData.high));

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                var retObject = new List<dynamic>();
                using (var dataReader = await cmd.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        //定義ExpandoObject,代表無成員物件,意指可以再執行階段動態新增及刪除成員
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var iFiled = 0; iFiled < dataReader.FieldCount; iFiled++)
                        {
                            dataRow.Add(
                                //取得欄位名稱
                                dataReader.GetName(iFiled),
                                //如果欄位值為null,則回傳null
                                dataReader.IsDBNull(iFiled) ? null : dataReader[iFiled] 
                            );
                        }

                        //將取得的dataRow加入動態List中
                        retObject.Add((ExpandoObject)dataRow);
                    }
                }
                //返回sp results
                return retObject;
            }
        }
    }
}
