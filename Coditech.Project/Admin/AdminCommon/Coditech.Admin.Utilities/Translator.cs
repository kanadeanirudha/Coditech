using AutoMapper;


namespace Coditech.Admin.Utilities
{
    public class Translator
    {

        protected readonly IMapper _mapper;

        public Translator(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        ///   Translate to destination type.
        /// </summary>
        /// <param id="source">Entity/Model to translate.</param>
        /// <returns>returns translated entity or model.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public TDest Translate<TDest>(object source) => _mapper.Map<TDest>(source);

        /// <summary>
        ///   Translate to list of destination type.
        /// </summary>
        /// <param id="source">Entity/Model to translate.</param>
        /// <param id="collection">List of Entity/Model to translate.</param>
        /// <returns>returns translated entity or model list.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public IEnumerable<TDest> Translate<TDest>(IEnumerable<object> collection) => _mapper.Map<IEnumerable<TDest>>(collection);

        /// <summary>
        /// Translate Source to Destination.
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns>TDest</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public TDest Translate<TDest, TSource>(TSource source) => _mapper.Map<TSource, TDest>(source);

        /// <summary>
        /// Collection Translate Source to Destination.
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <returns>IEnumerable<TDest></returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public IEnumerable<TDest> Translate<TDest, TSource>(IEnumerable<TSource> collection) => _mapper.Map<IEnumerable<TSource>, IEnumerable<TDest>>(collection);
    }
}
