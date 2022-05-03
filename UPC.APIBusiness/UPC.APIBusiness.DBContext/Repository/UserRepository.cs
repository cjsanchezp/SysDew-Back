using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public EntityBaseResponse Insert(EntityUser user)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_InsertarUuario";

                    var p = new DynamicParameters();
                    p.Add(name: "@IDUSUARIO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@LOGINUSUARIO", value:user.loginusuario, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PASSWORDUSUARIO", value: user.passwordusuario, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@IDPERFIL", value: user.idperfil, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@NOMBRES", value: user.nombres, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@APELLIDOPATERNO", value: user.apellidopaterno, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@APELLIDOMATERNO", value: user.apellidomaterno, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@DOCUMENTOIDENTIDAD", value: user.documentoidentidad, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@USUARIOCREA", value: user.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityUser>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();

                    int idusuario = p.Get<int>("@IDUSUARIO");

                    if (idusuario > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = idusuario,
                            nombre = user.nombres
                        };
                    }
                    else
                    {
                        response.issuccess = false;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = null;
                    }

                }
            }
            catch (Exception ex)
            {

                response.issuccess = false;
                response.errorcode = "0001";
                response.errormessage = ex.Message;
                response.data = null;
            }

            return response;
        }

        public EntityBaseResponse Login(EntityLogin login)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    var userresponse = new EntityLoginResponse();
                    const string sql = "usp_user_login";

                    var p = new DynamicParameters();
                    p.Add(name: "@LOGINUSUARIO", value: login.usr, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PASSWORDUSUARIO", value: login.pwd, dbType: DbType.String, direction: ParameterDirection.Input);

                    userresponse = db.Query<EntityLoginResponse>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();

                    if (userresponse != null)
                    {
                        response.issuccess = true;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = userresponse;
                        
                    }
                    else
                    {
                        response.issuccess = false;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {

                response.issuccess = false;
                response.errorcode = "0001";
                response.errormessage = ex.Message;
                response.data = null;
            }

            return response;
        }
    }
}
