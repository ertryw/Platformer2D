public class EntityState
{
    public EntityObject EntityObject
    {
        get; set;
    }

    public RigidbodyController RigidbodyController
    {
        get; set;
    }

    public EntityState(EntityObject entityObject, RigidbodyController rigidbodyController)
    {
        EntityObject = entityObject;
        RigidbodyController = rigidbodyController;
    }
}

