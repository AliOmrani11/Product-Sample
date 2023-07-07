using Product.Application.Repositories.Accessory;
using Product.Application.Repositories.Product;
using Product.Application.Repositories.Properties;
using Product.Application.UnitofWorks;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories.Accessory;
using Product.Infrastructure.Repositories.Product;
using Product.Infrastructure.Repositories.Properties;

namespace Product.Infrastructure.UnitofWorks;

public class UnitofWork : IUnitofWork
{
    #region Constructor
    protected readonly ApplicationDbContext _db;
    public UnitofWork(ApplicationDbContext db)
    {
        _db = db;
    }

    #endregion


    #region Repositories

    private IProductGroupRepository _productGroupRepository;
    public IProductGroupRepository ProductGroupRepository
    {
        get
        {
            if (_productGroupRepository == null)
                _productGroupRepository = new ProductGroupRepository(_db);
            return _productGroupRepository;
        }
    }

    
    
    private IProductRepository _productRepository;
    public IProductRepository ProductRepository
    {
        get
        {
            if (_productRepository == null)
                _productRepository = new ProductRepository(_db);
            return _productRepository;
        }
    }

    
    
    private IProductPropertyRepository _productPropertyRepository;
    public IProductPropertyRepository ProductPropertyRepository
    {
        get
        {
            if (_productPropertyRepository == null)
                _productPropertyRepository = new ProductPropertyRepository(_db);
            return _productPropertyRepository;
        }
    }

    
    private IPropertyTypeRepository _propertyTypeRepository;
    public IPropertyTypeRepository PropertyTypeRepository
    {
        get
        {
            if (_propertyTypeRepository == null)
                _propertyTypeRepository = new PropertyTypeRepository(_db);
            return _propertyTypeRepository;
        }
    }


    private IPropertyValueRepository _propertyValueRepository;
    public IPropertyValueRepository PropertyValueRepository
    {
        get
        {
            if (_propertyValueRepository == null)
                _propertyValueRepository = new PropertyValueRepository(_db);
            return _propertyValueRepository;
        }
    }


    private IAccessoryRepository _accessoryRepository;
    public IAccessoryRepository AccessoryRepository
    {
        get
        {
            if (_accessoryRepository == null)
                _accessoryRepository = new AccessoryRepository(_db);
            return _accessoryRepository;
        }
    }

    #endregion


    #region Methods


    public int Save()
    {
        return _db.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
    #endregion


    #region Dispose
    private bool _disposed = false;


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitofWork()
    {
        Dispose(false);
    }

    #endregion
}
