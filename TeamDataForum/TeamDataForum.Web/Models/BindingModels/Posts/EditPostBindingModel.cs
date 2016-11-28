namespace TeamDataForum.Web.Models.BindingModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using Resources;
    using Threads;

    public class EditPostBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorNoThread),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public IdentifiableThreadBindingModel Thread { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextTextRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MinLength(NumericValues.PostTextMinValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMinValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PostTextMaxValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMaxValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Post text")]
        public string Text { get; set; }
    }
}