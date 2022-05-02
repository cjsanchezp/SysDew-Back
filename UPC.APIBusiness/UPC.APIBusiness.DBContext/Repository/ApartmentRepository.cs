using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using DBEntity;
using Dapper;

namespace DBContext
{
    public class ApartmentRepository : BaseRepository, IApartmentRepository
    {
        public EntityBaseResponse GetApartments()
        {
            var response = new EntityBaseResponse();

            try
            {
                using(var db = GetSqlConnection())
                {
                    var apartments = new List<EntityApartment>();
                    const string sql = "usp_Listar_Departamentos";

                    apartments = db.Query<EntityApartment>(
                        sql: sql,
                        commandType: CommandType.StoredProcedure
                        ).ToList();

                    if (apartments.Count > 0)
                    {
                        var imageRepo = new ImageRepository();
                        foreach (var apart in apartments)
                        {
                            apart.images = imageRepo.GetImages(apart.iddepartamento, "A");
                        }
                        response.issuccess = true;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = apartments;
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

        public EntityBaseResponse GetApartments(int idproyecto)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    var apartments = new List<EntityApartment>();
                    const string sql = "usp_Listar_Departamentos_X_Proyecto";
                    var p = new DynamicParameters();
                    p.Add(name: "@IDPROYECTO", value: idproyecto, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    apartments = db.Query<EntityApartment>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                        ).ToList();

                    if (apartments.Count > 0)
                    {
                        var imageRepo = new ImageRepository();
                        foreach (var apart in apartments)
                        {
                            apart.images = imageRepo.GetImages(apart.iddepartamento, "A");
                        }

                        response.issuccess = true;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = apartments;
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
