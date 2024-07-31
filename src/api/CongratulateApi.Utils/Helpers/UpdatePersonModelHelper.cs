using CongratulateApi.Domain.Models.Person;

namespace CongratulateApi.Utils.Helpers;

public static class UpdatePersonModelHelper
{
    public static bool HasPropertiesForUpdating(PersonUpdateModel model)
    {
        return model.Name != null ||
               model.Surname != null ||
               model.DayOfBirth != null;
    }
}