using Product.Application.Repositories.Accessory;
using Product.Application.Repositories.Product;
using Product.Application.Repositories.Properties;

namespace Product.Application.UnitofWorks;

public interface IUnitofWork
{
    public IProductGroupRepository ProductGroupRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IProductPropertyRepository ProductPropertyRepository { get; }
    public IPropertyTypeRepository PropertyTypeRepository { get; }
    public IPropertyValueRepository PropertyValueRepository { get; }
    public IAccessoryRepository AccessoryRepository { get; }


    Task<int> SaveAsync();
    int Save();
}
