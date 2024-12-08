using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Brands.Validators
{
    public record ValidateBrandName(string Name, Guid? Id = null!);
}
