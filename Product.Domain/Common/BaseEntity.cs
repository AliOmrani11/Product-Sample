namespace Product.Domain.Common;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        InsertedDate = DateTime.Now;
    }
    public int Id { get; set; }
    public DateTime InsertedDate { get; set; }
}
