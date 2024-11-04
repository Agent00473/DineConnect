namespace DineConnect.OrderManagementService.Application.Interfaces.MapperFactories
{
    //Contratcs to Implment this and Register for DI

    /// <summary>
    /// Converts a request to an Entity
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRequestEntityFactory<TRequest, TEntity>
    {
        TEntity CreateEntity(TRequest request);
        IEnumerable<TEntity> CreateEntities(IEnumerable<TRequest> requests);

    }

    /// <summary>
    /// Converts an Entity to Response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityResponseFactory<in TEntity, out TResponse>
    {
        TResponse CreateResponse(TEntity entity);
        IEnumerable<TResponse> CreateResponses(IEnumerable<TEntity> entities);
    }
}
