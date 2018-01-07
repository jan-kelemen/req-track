﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.UseCases
{
    public class ChangeUseCaseViewModel : ViewModel
    {
        public ChangeUseCaseViewModel() : this(null, null) { }

        public ChangeUseCaseViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string UseCaseId { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

        [HiddenInput]
        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [DisplayName("Title")]
        [StringLength(UseCaseValidationHelper.MaximumTitleLength, ErrorMessage = "Title is too long.")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [DisplayName("Steps")]
        public List<string> Steps { get; set; }

        [DisplayName("Note")]
        [StringLength(UseCaseValidationHelper.MaximumNoteLength, ErrorMessage = "Note is too long.")]
        public string Note { get; set; }
    }
}
