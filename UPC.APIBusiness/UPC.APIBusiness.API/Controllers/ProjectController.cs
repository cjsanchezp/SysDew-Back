using DBContext;
using DBEntity;
using DBEntity.Model;
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
    /// Este controlador expone los ecursos para el manejo de proyectos, para que aparezca 3 veces slash (/)
    /// </summary>
    [Produces("application/json")]
    [Route("api/project")]
    [ApiController]
    public class ProjectController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IProjectRepository __ProjectRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectRepository"></param>

        public ProjectController(IProjectRepository projectRepository)
        {
            __ProjectRepository = projectRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("listar")]
        public ActionResult GetProjects()
        {
            var ret = __ProjectRepository.GetProjects();
            return Json(ret);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("obtener")]
        public ActionResult GetProject(int id)
        {
            var ret = __ProjectRepository.GetProject(id);
            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityProject project)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            var userid = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
            var userdni = claims.Where(p => p.Type == "client_numero_documento").FirstOrDefault()?.Value;

            project.UsuarioCrea = int.Parse(userid);

            var ret = __ProjectRepository.Insert(project);
            return Json(ret);
        }
    }
}
