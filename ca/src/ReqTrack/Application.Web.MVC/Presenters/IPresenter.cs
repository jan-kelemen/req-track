﻿using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public interface IPresenter<T, out VM> : IUseCaseOutput<T> where T : ResponseModel
    {
        VM ViewModel { get; }

        T Response { get; }
    }
}
