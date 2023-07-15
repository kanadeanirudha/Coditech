using Coditech.Common.API.Model;


namespace Coditech.Common.Helper.Utilities
{
    public static class TranslatorExtension
    {
        public static CoditechTranslator TranslatorInstance { get; set; }

        /// <summary>
        /// Translate Model to Entity
        /// </summary>
        /// <typeparam name="TEntity">Translate Model to TEntity</typeparam>
        /// <param name="modelBase">BaseModel is extended class</param>
        /// <returns></returns>
        public static TEntity FromModelToEntity<TEntity>(this BaseModel modelBase)
            => TranslatorInstance.Translate<TEntity>(modelBase);

        /// <summary>
        /// Transalate Entity to Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static TModel FromEntityToModel<TModel>(this Object entity)
            => TranslatorInstance.Translate<TModel>(entity);

        ///// <summary>
        ///// Translate Model to Entity
        ///// </summary>
        ///// <typeparam name="TEntity">TEntity is a destination model, having contraint TEntity is a ZnodeEntityBaseModel</typeparam>
        ///// <typeparam name="TModel">TModel is source model</typeparam>
        ///// <param name="Model">model is extended class</param>
        ///// <returns></returns>
        //public static TEntity ToEntity<TEntity, TModel>(this TModel model) where TEntity : ZnodeEntityBaseModel
        //    => TranslatorInstance.Translate<TEntity, TModel>(model);

        /// <summary>
        /// Translate Model collection to Model Entity
        /// </summary>
        /// <typeparam name="TEntity">TEntity is a destination model</typeparam>
        /// <param name="collection">collection is extended BaseModel class list</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToEntity<TEntity>(this IEnumerable<BaseModel> collection)
            => TranslatorInstance.Translate<TEntity>(collection);

        /// <summary>
        /// Translate Model collection to Model Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToEntity<TEntity, TModel>(this IEnumerable<TModel> collection)
            => TranslatorInstance.Translate<TEntity, TModel>(collection);


        /// <summary>
        /// Transalate Entity to Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static TModel ToModel<TModel, TEntity>(this TEntity entity)
            => TranslatorInstance.Translate<TModel, TEntity>(entity);

        /// <summary>
        /// Translate Collection Entity to Collection Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="EntityCollection"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> ToModel<TModel>(this IEnumerable<Object> entityCollection)
            => TranslatorInstance.Translate<TModel>(entityCollection);

        /// <summary>
        /// Translate Collection Entity to Collection Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="EntityCollection"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> ToModel<TModel, TEntity>(this IEnumerable<TEntity> entityCollection)
            => TranslatorInstance.Translate<TModel, TEntity>(entityCollection);

        /// <summary>
        /// Translate Filter Collection to Filter Data Collection
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static FilterDataCollection ToFilterDataCollections(this FilterCollection filters)
        {
            FilterDataCollection dataCollection = new FilterDataCollection();
            if (!Equals(filters, null))
            {
                dataCollection.AddRange(filters.ToModel<FilterDataTuple, FilterTuple>());
            }
            return dataCollection;
        }

        /// <summary>
        /// Source to destination mapping by keeping unmatched properties as it is in the destination. Only matched properties will be updated with the values available in the source type.
        /// </summary>
        /// <typeparam name="TSource">Source</typeparam>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        /// <returns>Destination</returns>
        public static TDestination ToModel<TSource, TDestination>(this TSource source, TDestination destination)
            => TranslatorInstance.Translate<TSource, TDestination>(source, destination);

        /// <summary>
        /// List type source to destination mapping by keeping unmatched properties as it is in the destination. Only matched properties will be updated with the values available in the source type.
        /// </summary>
        /// <typeparam name="TSource">Source</typeparam>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        /// <returns>IEnumerable<TDestination></returns>
        public static IEnumerable<TDestination> ToModel<TSource, TDestination>(this IEnumerable<TSource> source, IEnumerable<TDestination> destination)
            => TranslatorInstance.Translate<TSource, TDestination>(source, destination);

        /// <summary>
        /// Translate Model to ViewModel
        /// </summary>
        /// <typeparam name="TDTOModel">Translate Model to ViewModel</typeparam>
        /// <param name="modelBase">APIBaseModel is extended class</param>
        /// <returns></returns>
        public static TDTOModel ToViewModel<TDTOModel>(this BaseModel modelBase)
            => TranslatorInstance.Translate<TDTOModel>(modelBase);

        /// <summary>
        /// Translate Model to ViewModel
        /// </summary>
        /// <typeparam name="TDTOModel">TDTOModel is a destination model, having contraint TDTOModel is a BaseModel</typeparam>
        /// <typeparam name="TModel">TModel is source model</typeparam>
        /// <param name="Model">model is extended class</param>
        /// <returns></returns>
        public static TDTOModel ToViewModel<TDTOModel, TModel>(this TModel model) where TDTOModel : BaseModel
            => TranslatorInstance.Translate<TDTOModel, TModel>(model);

        /// <summary>
        /// Translate Model collection to View Model
        /// </summary>
        /// <typeparam name="TDTOModel">TDTOModel is a destination model</typeparam>
        /// <param name="collection">collection is extended APIBaseModel class list</param>
        /// <returns></returns>
        public static IEnumerable<TDTOModel> ToViewModel<TDTOModel>(this IEnumerable<BaseModel> collection)
            => TranslatorInstance.Translate<TDTOModel>(collection);

        /// <summary>
        /// Translate Model collection to View Model
        /// </summary>
        /// <typeparam name="TDTOModel"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<TDTOModel> ToViewModel<TDTOModel, TModel>(this IEnumerable<TModel> collection)
            => TranslatorInstance.Translate<TDTOModel, TModel>(collection);

    }
}
