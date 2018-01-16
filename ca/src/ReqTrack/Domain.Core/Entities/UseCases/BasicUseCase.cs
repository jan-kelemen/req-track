using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;

namespace ReqTrack.Domain.Core.Entities.UseCases
{
    public class BasicUseCase : Entity<BasicUseCase>
    {
        private string _title;

        public BasicUseCase(Identity id, string title) : base(id)
        {
            Title = title;
        }

        public string Title
        {
            get => _title;
            set
            {
                if (!UseCaseValidationHelper.IsTitleValid(value))
                {
                    throw new ValidationException("Title is invalid")
                    {
                        PropertyKey = nameof(Title),
                    };
                }

                _title = value;
            }
        }
    }
}
