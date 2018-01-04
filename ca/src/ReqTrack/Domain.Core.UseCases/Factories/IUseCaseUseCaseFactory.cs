using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.DeleteUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;

namespace ReqTrack.Domain.Core.UseCases.Factories
{
    public interface IUseCaseUseCaseFactory
    {
        IUseCase<AddUseCaseRequest, AddUseCaseResponse> AddUseCase { get; }

        IUseCase<ChangeUseCaseInitialRequest, ChangeUseCaseRequest, ChangeUseCaseResponse> ChangeUseCase { get; }

        IUseCase<DeleteUseCaseRequest, DeleteUseCaseResponse> DeleteUseCase { get; }

        IUseCase<ViewUseCaseRequest, ViewUseCaseResponse> ViewUseCase { get; set; }
    }
}
