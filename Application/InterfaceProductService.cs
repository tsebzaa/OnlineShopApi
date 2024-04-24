using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Application
{
    public interface InterfaceProductService
    {
        List<Product> GetAll();

        Product GetProductById(int id);

        Product EditProduct(int id,Product product);

        Product CreateProduct(Product product);

        Product DeleteProduct(int id);

    }
}
