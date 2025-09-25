using AutoMapper;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
using System.Net;

namespace LibraryMS.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //convert string → DateOnly conversion
            CreateMap<string, DateOnly>()
                .ConvertUsing((string src) =>
                    string.IsNullOrEmpty(src) ? default(DateOnly) : DateOnly.Parse(src));

            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => ShouldMap(srcValue)));
        }

        // it is needed to map data during update any value to set DB value persist
        private bool ShouldMap(object sourceValue)
        {
            if (sourceValue == null)
                return false;

            // Skip empty/whitespace strings
            if (sourceValue is string str && string.IsNullOrWhiteSpace(str))
                return false;

            // Skip default value types (e.g., 0, DateTime.MinValue)
            var type = sourceValue.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                if (sourceValue.Equals(defaultValue))
                    return false;

                // Special handling for DateTime
                if (sourceValue is DateTime dt && dt == DateTime.MinValue)
                    return false;

                // Optional: handle DateOnly
                if (sourceValue is DateOnly d && d == DateOnly.MinValue)
                    return false;

                // Handle Guid.Empty
                if (sourceValue is Guid guid && guid == Guid.Empty)
                    return false;
            }

            return true;
        }
    }
}
