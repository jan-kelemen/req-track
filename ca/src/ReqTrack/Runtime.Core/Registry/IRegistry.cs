using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.UseCases.Core.Factories;

namespace ReqTrack.Runtime.Core.Registry
{
    public interface IRegistry
    {

        /// <summary>
        /// Registers a factory for runtime.
        /// </summary>
        /// <typeparam name="T">Factory type.</typeparam>
        /// <param name="factoryObject">Factory.</param>
        /// <returns><c>true</c> if the factory was successfully registered.</returns>
        bool RegisterFactory<T>(T factoryObject) where T : class;
        
        /// <summary>
        /// Returns a factory of specified type.
        /// </summary>
        /// <typeparam name="T">Factory type.</typeparam>
        /// <returns>Registered factory.</returns>
        T GetFactory<T>() where T : class;
    }
}
