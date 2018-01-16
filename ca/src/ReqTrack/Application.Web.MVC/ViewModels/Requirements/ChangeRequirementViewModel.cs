﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Requirements
{
    public class ChangeRequirementViewModel : ViewModel
    {
        public ChangeRequirementViewModel() : this(null, null) { }

        public ChangeRequirementViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput] public string ProjectId { get; set; }

        [HiddenInput] public string RequirementId { get; set; }

        [HiddenInput]
        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [DisplayName("Title")]
        [StringLength(RequirementValidationHelper.MaximumTitleLength, ErrorMessage = "Title is too long.")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [DisplayName("Note")]
        [StringLength(RequirementValidationHelper.MaximumNoteLength, ErrorMessage = "Note is too long.")]
        public string Note { get; set; }

        [HiddenInput]
        public SelectListItem[] Types { get; set; }
    }
}
