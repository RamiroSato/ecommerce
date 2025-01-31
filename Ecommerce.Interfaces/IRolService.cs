using Ecommerce.DTO;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces;

public interface IRolService
{
    Task<IEnumerable<Rol>> GetRolesAsync();
    Task<Rol> GetRolByIdAsync(int id);
    Task<Rol> AddRolAsync(Rol rol);
    Task<Rol> UpdateRolAsync(RolToUpdateDTO rolToUpdate);
    Task DeleteRolAsync(int id);
    Task<Rol> GetRolByCognitoIdAsync(string cognitoId);

}
