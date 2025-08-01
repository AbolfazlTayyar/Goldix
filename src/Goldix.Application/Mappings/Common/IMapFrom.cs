﻿namespace Goldix.Application.Mappings.Common;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}