namespace PROCommerce.Authentication.Domain.Entities.Base;

public abstract class BaseEntity
{
    private long? _id = null;

    public long? Id 
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeletionDate { get; set; }

    protected void SetIdentity(long id)
    {
        _id = id;
    }
}