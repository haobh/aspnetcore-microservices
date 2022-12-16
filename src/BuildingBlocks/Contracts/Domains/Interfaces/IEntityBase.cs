using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains.Interfaces
{
    /// <summary>
    /// Định nghĩa interface chung cho cả hệ thống
    /// Khi thay đổi kiểu dữ liệu tên biến thì nó sẽ tự động thay đổi ở class domain
    /// Biến T nó nhận kiểu bất kỳ khi đưa vào
    /// </summary>
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
