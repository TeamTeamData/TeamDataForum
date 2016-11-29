namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class TownUserBindingModel
    {
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorTownNameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PlaceNameMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorTownNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Town")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorCountryNameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PlaceNameMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorCountryNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}