using Domain.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface InterfaceTestRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);

        Product EditProduct(Product product,long id);
        Product CreateProduct(Product product);
        Product DeleteProduct(int id);

    }
}
