namespace TeamDataForum.Web.Models.BindingModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class PostBindingModel
    {
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextTextRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MinLength(NumericValues.PostTextMinValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMinValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PostTextMaxValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMaxValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Post")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}