using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class User
    {
        [Key]
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public string Ten { get; set; }
        public string sdt { get; set; }
        public string Email { get; set; }
        public string Diachi { get; set; }

    }
}
