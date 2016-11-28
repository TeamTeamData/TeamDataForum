namespace TeamDataForum.Web.Models.BindingModels.Threads
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class EditThreadBindingModel
    {
        [Required]
        public int Id { get; set; }

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
    }
}