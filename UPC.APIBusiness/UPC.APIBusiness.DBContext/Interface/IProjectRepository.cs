using System;
using System.Collections.Generic;
using System.Text;
using DBEntity;
using DBEntity.Model;

namespace DBContext
{
    public interface IProjectRepository
    {
        EntityBaseResponse GetProjects();

        EntityBaseResponse GetProject(int id);

        EntityBaseResponse Insert(EntityProject project);
    }
}
