using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/apartment")]
    public class ApartmentController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IApartmentRepository __ApartmentRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apartmentRepository"></param>
        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            __ApartmentRepository = apartmentRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("listar")]
        public ActionResult GetApartments()
        {
            var ret = __ApartmentRepository.GetApartments();

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idproyecto"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("listarxproyecto")]
        public ActionResult GetApartments(int idproyecto)
        {
            var ret = __ApartmentRepository.GetApartments(idproyecto);
            return Json(ret);
        }
    }
}
