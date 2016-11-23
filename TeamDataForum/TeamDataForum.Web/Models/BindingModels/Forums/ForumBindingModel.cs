﻿namespace TeamDataForum.Web.Models.BindingModels.Forums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Resources;
    using Models.BindingModels.Users;

    public class ForumBindingModel
    {
        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumTitleRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumTitleMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Forum title")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumDescriptionRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumDescriptionMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Forum description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [MinLength(1)]
        public ModeratorBindingModel[] Moderators { get; set; }
    }
}