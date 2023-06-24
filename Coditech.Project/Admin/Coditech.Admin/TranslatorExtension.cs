using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;

namespace Coditech.Admin
{
    public static class TranslatorExtension
    {
        public static Translator TranslatorInstance { get; set; }

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

        /// <summary>
        /// Transalate View Model to Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static TModel ToModel<TModel>(this BaseViewModel viewModel)
            => TranslatorInstance.Translate<TModel>(viewModel);

        /// <summary>
        /// Transalate View Model to Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TDTOModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static TModel ToModel<TModel, TDTOModel>(this TDTOModel viewModel)
            => TranslatorInstance.Translate<TModel, TDTOModel>(viewModel);

        /// <summary>
        /// Translate Collection View Model to Collection Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="EntityCollection"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> ToModel<TModel>(this IEnumerable<BaseViewModel> viewModelCollection)
            => TranslatorInstance.Translate<TModel>(viewModelCollection);

        /// <summary>
        /// Translate Collection View Model to Collection Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TDTOModel"></typeparam>
        /// <param name="EntityCollection"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> ToModel<TModel, TDTOModel>(this IEnumerable<TDTOModel> viewModelCollection)
            => TranslatorInstance.Translate<TModel, TDTOModel>(viewModelCollection);
    }
}
