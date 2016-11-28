namespace TeamDataForum.Web.Models.BindingModels.Threads
{
    using System.ComponentModel.DataAnnotations;
    using Forums;
    using Posts;
    using Resources;

    public class ThreadBindingModel
    {
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadTitleRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadTitleMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MinLength(NumericValues.ThreadTitleMinLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadTitleMinLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Thread title")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadNoForum),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public IdentifiableForumBindingModel Forum { get; set; }

        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadNoPost),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MinLength(NumericValues.PostTextMinValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMinValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PostTextMaxValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMaxValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Post text")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}