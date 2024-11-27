using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Models
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string Taikhoan);
		
	}
}
