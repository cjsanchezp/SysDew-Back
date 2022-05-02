using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IApartmentRepository
    {
        EntityBaseResponse GetApartments();
        EntityBaseResponse GetApartments(int idproyecto);
    }
}
